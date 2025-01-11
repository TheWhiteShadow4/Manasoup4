using System.Collections;
using UnityEngine;

public class Units : MonoBehaviour
{
    public Fraction fraction = Fraction.Neutral;
    public int Strength = 5;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("hit");

        PointGeneration HitObject = collision.gameObject.GetComponent<PointGeneration>();

        if (HitObject.fraction != fraction)
        {
            HitObject.currentPoints -= 1;
            Strength -= 1;
        }
    }

    private void Update()
    {
        if (Strength <= 0)
        {
            Destroy(gameObject);
        }
    }

}
