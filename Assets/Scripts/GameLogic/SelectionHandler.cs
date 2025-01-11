using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class SelectionHandler : MonoBehaviour
{
    public float minDragDist = 5;
    public LayerMask selectionMask;

    private readonly List<SelectableObject> selection = new List<SelectableObject>();
    private Vector2 dragStart;
    private bool isDragging;

    private void OnEnable()
    {
        GameManager.Instance.selectionChangedEvent.OnSelected += OnSelected;
        GameManager.Instance.selectionChangedEvent.OnDeselected += OnDeselected;
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
            foreach (var sel in selection)
            {
                if (sel)
                {
                    sel.MarkAsSelected(false);
                }
            }
            selection.Clear();
        }
        selection.Add(obj);
    }

    void OnDeselected(SelectableObject obj)
    {
        selection.Remove(obj);
    }

    void Update()
    {
        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            foreach (var sel in selection)
            {
                sel.Deselect();
            }
        }

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            dragStart = Mouse.current.position.ReadValue();
            Debug.Log("Drag start: " + dragStart);
        }

        if (!Mouse.current.leftButton.isPressed)
        {
            Vector2 dragEnd = Mouse.current.position.ReadValue();
            if (Vector2.Distance(dragStart, dragEnd) >= minDragDist)
            {
                isDragging = true;
            }
        }

        if (Mouse.current.leftButton.wasReleasedThisFrame && isDragging)
        {
            Vector2 dragEnd = Mouse.current.position.ReadValue();
            Debug.Log("Drag end: " + dragEnd);

            Vector2 size = dragEnd - dragStart;
            Vector2 point = dragStart + size / 2;

            Collider2D[] colliders = Physics2D.OverlapBoxAll(point, size, 0, selectionMask);
            if (colliders.Length > 0)
            {
                foreach (var col in colliders)
                {
                    if (col.TryGetComponent(out SelectableObject obj))
                    {
                        obj.Select(true);
                    }
                }
            }
        }
    }
}
