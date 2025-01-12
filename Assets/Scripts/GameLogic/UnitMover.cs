using UnityEngine;
using UnityEngine.AI;
using NavMeshPlus.Components;
using UnityEngine.VFX;


public class UnitMover : MonoBehaviour
{
    public GameObject targetObject;
    public VisualEffect effect;
    public float effectUnitSpeed = 8;
    public float effectLookAhead = 2;
    public int opticalUnitMuliplier = 1;
    private GameObject lastTargetObject;
    private NavMeshAgent agent;
    private Units units;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        units = GetComponent<Units>();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        effect.SetInt("Units", units.strength*opticalUnitMuliplier);
        effect.SetInt("LeftUnits", units.strength*opticalUnitMuliplier);
        effect.SetFloat("Speed", effectUnitSpeed);
    }

    void goToTarget(GameObject targetObject)
    {
        agent.SetDestination(targetObject.transform.position);
    }

    // Update is called once per frame
    float areaCostLast = 0;


    float getMaxAreaCost()
    {
        float areaCost = 0;
        Collider2D[] colliders = Physics2D.OverlapPointAll(transform.position);
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject != gameObject)
            {
                NavMeshModifier navMeshModifier = collider.gameObject.GetComponent<NavMeshModifier>();
                if (navMeshModifier != null)
                {
                    float aC = agent.GetAreaCost(navMeshModifier.area);
                    if (aC > areaCost)
                    {
                        areaCost = aC;
                    }
                }
            }
        }
        return areaCost;
    }
    void FixedUpdate()
    {
        float areaCost = getMaxAreaCost();
        if (areaCostLast != areaCost)
        {
            areaCostLast = areaCost;
            Debug.Log("Current area cost: " + areaCost);
        }
        agent.speed = 10 - areaCost * 2;

        Vector3 vec = (targetObject.transform.position - transform.position);
        float dist = vec.magnitude;
        Vector3 direction = vec.normalized;

        effect.SetVector3("Attractor", transform.position + direction * Mathf.Min(effectLookAhead, dist));
        effect.SetInt("LeftUnits", units.strength*opticalUnitMuliplier);

        //if (agent.velocity != Vector3.zero) particleEfect.transform.rotation = Quaternion.LookRotation(agent.velocity);
    }

    void Update()
    {
        if (targetObject != lastTargetObject)
        {
            goToTarget(targetObject);
        }
    }
}
