using UnityEngine;
using UnityEngine.InputSystem;

[SelectionBase]
public class SelectableObject : MonoBehaviour
{
    public static int PlayerLayer = 6;
    public static int EnemyLayer = 7;
    public static int NeutralLayer = 8;

    public Fraction fraction = Fraction.Neutral;

    protected bool isSelected;
    protected GameObject selectMarker;

    public bool IsSelected
    {
        get { return isSelected; }
    }

    void Start()
    {
        selectMarker = gameObject.transform.Find("SelectMarker").gameObject;
        MarkAsSelected(false);
    }

    /*private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            var collider = Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()), selectionMask);
            if (collider != null && fraction == Fraction.Player)
            {
                Select(true);
            }
        }
        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            var collider = Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()), selectionMask);
            if (collider != null)
            {
                Attack();
            }
        }
        //if (Mouse.current.rightButton.wasPressedThisFrame) Deselect();
    }*/

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

    public virtual void Attack() {}
}
