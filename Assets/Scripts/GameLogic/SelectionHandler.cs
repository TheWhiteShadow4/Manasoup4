using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SelectionHandler : MonoBehaviour
{
    public float minDragDist = 5;
    public LayerMask selectionMask;
    public LayerMask enemySelectionMask;
    public SpriteRenderer selectionRect;

    private readonly List<SelectableObject> selection = new List<SelectableObject>();
    private Vector2 dragStart;
    private bool isDragging;

    private void OnEnable()
    {
        GameManager.Instance.selectionChangedEvent.OnSelected += OnSelected;
        GameManager.Instance.selectionChangedEvent.OnDeselected += OnDeselected;
        GameManager.Instance.raidEvent.OnEventRaised += OnRaidStarted;
        selectionRect.enabled = false;
    }

    private void OnDisable()
    {
        GameManager.Instance.selectionChangedEvent.OnSelected -= OnSelected;
        GameManager.Instance.selectionChangedEvent.OnDeselected -= OnDeselected;
    }

    void OnSelected(SelectableObject obj, bool exclusive)
    {
        if (exclusive)
        {
            DeselectAll();
        }
        selection.Add(obj);
    }

    void OnDeselected(SelectableObject obj)
    {
        selection.Remove(obj);
    }

    void OnRaidStarted(PointGeneration target)
    {
        foreach (var sel in selection)
        {
            if (sel && sel.TryGetComponent(out PointGeneration poi))
            {
                int unitCount = poi.currentPoints / 2;
                if (unitCount > 0 && poi.CanStartRaid())
                {
                    poi.currentPoints -= unitCount;
                    GameManager.Instance.StartRaid(poi, target, unitCount);
                    poi.SetRaidCooldown();
                }
            }
        }
    }

    void Update()
    {
        SelectableObject selectable = null;
        Collider2D collider;
        Vector3 mousePosition = GameManager.Instance.ActiveCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            collider = Physics2D.OverlapPoint(mousePosition, selectionMask);
            if (collider != null && collider.TryGetComponent(out selectable))
            {
                selectable.Select(true);
            }
            dragStart = mousePosition;
            dragStart = dragStart.Floor();
        }
        else if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            collider = Physics2D.OverlapPoint(mousePosition, selectionMask | enemySelectionMask);
            if (collider != null && collider.TryGetComponent(out selectable))
            {
                selectable.Attack();
            }
            else
            {
                DeselectAll();
            }
        }

        if (Mouse.current.leftButton.isPressed)
        {
            Vector2 dragEnd = mousePosition;
            dragEnd = dragEnd.Floor();
            if (Vector2.Distance(dragStart, dragEnd) >= minDragDist)
            {
                isDragging = true;
                selectionRect.enabled = true;
                //Debug.Log("Drag Aktiv von " + dragEnd + " bis " + dragEnd);
            }

            if (isDragging)
            {
                Vector2 size = dragEnd - dragStart;
                Vector2 point = dragStart + size / 2;
                selectionRect.transform.position = point;
                selectionRect.size = size.Abs();
            }
        }

        if (Mouse.current.leftButton.wasReleasedThisFrame && isDragging)
        {
            DeselectAll();
            isDragging = false;
            selectionRect.enabled = false;
            Vector2 dragEnd = mousePosition;
            dragEnd = dragEnd.Floor();

            Vector2 start = dragStart.MinComponents(dragEnd);
            Vector2 end = dragStart.MaxComponents(dragEnd);

            Vector2 size = end - start;
            Vector2 point = start + size / 2;

            //Debug.Log("Drag size: " + size);
            //Debug.Log("Drag point: " + point);

            Collider2D[] colliders = Physics2D.OverlapBoxAll(point, size, 0, selectionMask);
            if (colliders.Length > 0)
            {
                foreach (var col in colliders)
                {
                    if (col.TryGetComponent(out SelectableObject obj))
                    {
                        obj.Select(false);
                    }
                }
            }
        }
    }

    private void DeselectAll()
    {
        foreach (var sel in selection)
        {
            if (sel) 
                sel.MarkAsSelected(false);
        }
        selection.Clear();
    }
}
