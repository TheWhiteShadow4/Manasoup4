using System.Collections;
using UnityEngine;

public class Units : MonoBehaviour
{
    public Fraction fraction = Fraction.Neutral;
    public int Strength = 5;
    public float StrengthIntervall = 0.5f;
    private PointGeneration Target;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("hit");

        Target = collision.gameObject.GetComponent<PointGeneration>();

        if (Target.fraction != fraction)
        {
            StartCoroutine(UnitAbrechnung());
        }
        else
        {
            Target.currentPoints += Strength;
            Strength = 0;
        }

    }

    private void Update()
    {
        if (Strength <= 0)
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator UnitAbrechnung()
    {
        Debug.Log("jop");
        
        while (Strength > 0)
        {
            Target.currentPoints -= 1;
            Strength -= 1;
            yield return new WaitForSeconds(StrengthIntervall);
        }

    }
}
