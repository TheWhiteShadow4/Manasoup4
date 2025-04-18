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

    public int unitCountMultiplier = 10;

    public GameObject world;
    public Units playerUnitPrefab;
    public Units enemyUnitPrefab;
    public Units boboUnitPrefab;

    private Camera activeCamera;
    [NonSerialized] public List<Fort> allForts;

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
        allForts = new List<Fort>();
    }

    public void RegisterPoi(Fort newPoi)
    {
        allForts.Add(newPoi);
    }

    public void UnregisterPoi(Fort poi)
    {
        allForts.Remove(poi);
    }
  
    public void StartRaid(Fort sourceObject, Fort targetObject, int unitCount)
    {
        if (sourceObject == targetObject) return;

        Units prefab = null;
        switch (sourceObject.fraction)
        {
            case Fraction.Player: prefab = ModeHandler.Instance.boboMode ? boboUnitPrefab : playerUnitPrefab; break;
            case Fraction.Enemy: prefab = enemyUnitPrefab; break;
            default: Debug.LogError("Kann Raid nicht von Neutral starten! " + sourceObject.name); return;
        }

        Units newUnit = Instantiate(prefab, sourceObject.transform.position, Quaternion.identity, world.transform);
        /*var ps = newUnit.GetComponent<UnitMover>().particleEfect.GetComponent<ParticleSystem>();
        var psMain = ps.main;
        psMain.maxParticles = unitCount * unitCountMultiplier;
        var emission = ps.emission;
        emission.rateOverTime = unitCount * unitCountMultiplier;*/
        newUnit.fraction = sourceObject.fraction;
        newUnit.target = targetObject;
        newUnit.strength = unitCount;
        newUnit.transform.GetComponent<UnitMover>().targetObject = targetObject.gameObject;
        //newUnit.Init();
        Debug.Log("Raiding "+targetObject.name + " mit " + unitCount + " Units");
    }
}
