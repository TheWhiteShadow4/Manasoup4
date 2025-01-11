using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject selecetdObject;
    public UnityEvent<GameObject> onSelect;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void selectNewObject(GameObject newObject){
        if(selecetdObject != newObject){
            selecetdObject = newObject;
            onSelect.Invoke(newObject);
        }
    }
}
