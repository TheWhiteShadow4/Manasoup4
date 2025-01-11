using UnityEngine;
using UnityEngine.AI;

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

    void Update()
    {

        NavMeshHit hit;
        if (NavMesh.SamplePosition(agent.transform.position, out hit, 1.0f, NavMesh.AllAreas)){
            float areaCost = agent.GetAreaCost(hit.mask);
            if (areaCostLast != areaCost){
                areaCostLast = areaCost;
                Debug.Log("Current area cost: " + areaCost);    
            }     
        }


        if (targetObject != lastTargetObject)
        {
            goToTarget(targetObject);
        }
    }
}
