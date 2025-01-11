using UnityEngine;
using UnityEngine.InputSystem;


public class SelectableObject : MonoBehaviour
{
    private bool isSelected;
    private GameObject selectMarker;

    public bool IsSelected
    {
        get { return isSelected; }
    }

    void Start()
    {
        selectMarker = gameObject.transform.Find("SelectMarker").gameObject;
        MarkAsSelected(false);
    }

    void OnMouseOver()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame) Select(!Keyboard.current.shiftKey.isPressed);
        if (Mouse.current.rightButton.wasPressedThisFrame) Deselect();
    }

    internal void MarkAsSelected(bool selected)
    {
        //Debug.Log("select " + gameObject.name + " " + selected);

        isSelected = selected;
        if (selectMarker) selectMarker.SetActive(selected);
    }

    public void Select(bool exclusive)
    {
        if (isSelected) return;
        MarkAsSelected(true);
        GameManager.Instance.selectionChangedEvent.Select(this, exclusive);
    }

    public void Deselect()
    {
        if (!isSelected) return;
        MarkAsSelected(false);
        GameManager.Instance.selectionChangedEvent.Deselect(this);
    }
}
