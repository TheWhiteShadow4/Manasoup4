using Events;
using UnityEngine;
using System.Collections.Generic;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public SelectionEventChannelSO selectionChangedEvent;
    public RaidEventChannelSO raidEvent;
    public GameObject hud;

    public GameObject world;
    public Units unitPrefab;

    private Camera activeCamera;
    [NonSerialized] public List<PointGeneration> allPois;

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
        allPois = new List<PointGeneration>();
    }

    public void RegisterPoi(PointGeneration newPoi)
    {
        allPois.Add(newPoi);
    }

    public void UnregisterPoi(PointGeneration poi)
    {
        allPois.Remove(poi);
    }
  
    public void StartRaid(GameObject sourceObject, PointGeneration targetObject, int unitCount)
    {
        Units newUnit = Instantiate(unitPrefab, sourceObject.transform.position, Quaternion.identity, world.transform);
        newUnit.target = targetObject;
        newUnit.transform.GetComponent<UnitMover>().targetObject = targetObject.gameObject;
        Debug.Log("Raiding "+targetObject.name + " mit " + unitCount + " Units");
    }
}
