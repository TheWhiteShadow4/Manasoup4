using System;
using System.Collections;
using UnityEngine;

public class Units : MonoBehaviour
{
    public Fraction fraction = Fraction.Neutral;
    public int strength = 5;
    public float strengthIntervall = 0.5f;
    [NonSerialized] public PointGeneration target;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PointGeneration poi))
        {
            if (poi != target) return;

            Debug.Log("hit");

            if (target.fraction != fraction)
            {
                StartCoroutine(UnitAbrechnung());
            }
            else
            {
                target.currentPoints += strength;
                strength = 0;
            }
        }
    }

    private void Update()
    {
        if (strength <= 0)
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator UnitAbrechnung()
    {
        Debug.Log("jop");
        
        while (strength > 0)
        {
            // FIXME: Was wenn das Fort schon eingenommen wurde?

            target.currentPoints -= 1;
            strength -= 1;
            yield return new WaitForSeconds(strengthIntervall);
        }

    }
}
