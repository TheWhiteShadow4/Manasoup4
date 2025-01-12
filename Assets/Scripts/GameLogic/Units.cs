using UnityEngine;

public class Units : MonoBehaviour
{
    public Fraction fraction = Fraction.Neutral;
    public int strength = 5;
    public float tickIntervall = 0.25f;
    public PointGeneration target;

    private float lastTick;

    Collider2D targetCollider = null;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnCollisionTick(collision);
        lastTick = Time.fixedTime;
        if (collision.TryGetComponent(out PointGeneration poi) && poi == target){
            targetCollider = collision;
        }
    }


    void FixedUpdate(){
        if (targetCollider){
            if (Time.fixedTime >= lastTick + tickIntervall){
                OnCollisionTick(targetCollider);
                lastTick += tickIntervall;
            }
        }
    }

    private void OnCollisionTick(Collider2D collision)
    {
        if (collision.TryGetComponent(out PointGeneration poi) && poi == target)
        {
            if (fraction == target.fraction)
            {
                target.currentPoints += strength;
                strength = 0;
            }
            else
            {
                if (target.currentPoints <= 0)
                {
                    target.CapturePoi(fraction, strength);
                    strength = 0;
                }
                else
                {
                    target.currentPoints -= 1;
                    strength -= 1;
                }
            }
        }
        else if (collision.TryGetComponent(out Units enemies))
        {
            if (fraction != target.fraction && enemies.strength > 0)
            {
                enemies.strength -= 1;
                strength -= 1;
            }
            else
            {
                // Merge?
            }
        }
        if (strength <= 0)
        {
            Destroy(gameObject);
        }
    }

    /*private IEnumerator UnitAbrechnung()
    {
        while (strength > 0)
        {
            if (fraction == target.fraction)
            {
                target.currentPoints += strength;
                yield return null;
            }
            else
            {
                if (target.currentPoints <= 0)
                {
                    target.CapturePoi(fraction, strength);
                }
                else
                {
                    target.currentPoints -= 1;
                    strength -= 1;
                    yield return new WaitForSeconds(tickIntervall);
                }
            }
        }
        Destroy(gameObject);        
    }*/
}
