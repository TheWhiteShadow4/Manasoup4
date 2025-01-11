using UnityEngine;
using UnityEngine.AI;
using NavMeshPlus.Components;


public class UnitMover : MonoBehaviour
{


    public GameObject targetObject;
    GameObject lastTargetObject;
    NavMeshAgent agent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    void goToTarget(GameObject targetObject){
        agent.SetDestination(targetObject.transform.position);
    }

    // Update is called once per frame
    float areaCostLast = 0;


    float getMaxAreaCost(){
        float areaCost = 0;
        Collider2D[] colliders = Physics2D.OverlapPointAll(transform.position);
        foreach (Collider2D collider in colliders){
            if (collider.gameObject != gameObject) {
                NavMeshModifier navMeshModifier = collider.gameObject.GetComponent<NavMeshModifier>();
                if (navMeshModifier != null){
                    float aC = agent.GetAreaCost(navMeshModifier.area);
                    if (aC > areaCost){
                        areaCost = aC;
                    }
                }
            }
        }
        return areaCost;
    }
    void FixedUpdate(){
        float areaCost = getMaxAreaCost();
        if (areaCostLast != areaCost){
            areaCostLast = areaCost;
            Debug.Log("Current area cost: "+areaCost);    
        }
        agent.speed = 10-areaCost*2;
    }

    void Update()
    {
        if (targetObject != lastTargetObject)
        {
            goToTarget(targetObject);
        }
    }
}
