using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{


    public GameObject selecetdObject;
    public UnityEvent<GameObject> onSelect;


    public void selectNewObject(GameObject newObject){
        if(selecetdObject != newObject){
            selecetdObject = newObject;
            onSelect.Invoke(newObject);
        }
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
