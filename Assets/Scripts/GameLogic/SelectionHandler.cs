using UnityEngine;
using System.Collections.Generic;

public class SelectionHandler : MonoBehaviour
{
    private readonly List<SelectableObject> selection = new List<SelectableObject>();

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
}
