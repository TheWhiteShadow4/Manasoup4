using Events;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public SelectionEventChannelSO selectionChangedEvent;
    public RaidEventChannelSO raidEvent;
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


  
    public void StartRaid(GameObject sourceObject, GameObject targetObject, int unitCount)
    {
        GameObject newUnit = Instantiate(unitPrefab, sourceObject.transform.position, Quaternion.identity);
        newUnit.transform.SetParent(world.transform);
        newUnit.transform.GetComponent<UnitMover>().targetObject = targetObject;
        Debug.Log("Raiding "+targetObject.name);
    }
    

}
