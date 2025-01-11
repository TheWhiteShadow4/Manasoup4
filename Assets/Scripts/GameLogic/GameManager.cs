using Events;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public SelectionEventChannelSO selectionChangedEvent;
    public RaidEventChannelSO raidEvent;
    public GameObject hud;

    public GameObject world;
    public Units unitPrefab;

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


  
    public void StartRaid(GameObject sourceObject, PointGeneration targetObject, int unitCount)
    {
        Units newUnit = Instantiate(unitPrefab, sourceObject.transform.position, Quaternion.identity, world.transform);
        newUnit.target = targetObject;
        newUnit.transform.GetComponent<UnitMover>().targetObject = targetObject.gameObject;
        Debug.Log("Raiding "+targetObject.name + " mit " + unitCount + " Units");
    }
    

}
