using Events;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public SelectionEventChannelSO selectionChangedEvent;
    public GameObject hud;

    public GameObject world;
    public GameObject unitPrefab;

    private Camera activeCamera;

    public Camera ActiveCamera
    {
        get
        {
            if (activeCamera == null)
            {
                activeCamera = Camera.main;
            }
            return activeCamera;
        }
    }

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


  
    void startRaid(GameObject sourceObject, GameObject targetObject){
        GameObject newUnit = Instantiate(unitPrefab, sourceObject.transform.position, Quaternion.identity);
        newUnit.transform.SetParent(world.transform);
        newUnit.transform.GetComponent<UnitMover>().targetObject = targetObject;
        Debug.Log("Raiding "+targetObject.name);
    }
    

}
