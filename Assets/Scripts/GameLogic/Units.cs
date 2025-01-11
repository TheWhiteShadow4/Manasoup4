using System;
using System.Collections;
using UnityEngine;

public class Units : MonoBehaviour
{
    public Fraction fraction = Fraction.Neutral;
    public int strength = 5;
    public float strengthIntervall = 0.25f;
    public PointGeneration target;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PointGeneration poi))
        {
            if (poi != target) return;

            Debug.Log("hit");
            StartCoroutine(UnitAbrechnung());           
        }
    }

    private IEnumerator UnitAbrechnung()
    {
        while (strength > 0 && target.currentPoints > 0)
        {
            target.currentPoints -= 1;
            strength -= 1;
            yield return new WaitForSeconds(strengthIntervall);
        }

        if (strength <= 0)
        {
            Destroy(gameObject);
            yield return null;
        }

        if (target.currentPoints <= 0)
        {
            target.CapturePoi(fraction, strength);
            Destroy(gameObject);
        }          
    }
}
