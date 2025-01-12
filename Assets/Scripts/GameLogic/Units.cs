using UnityEngine;

public class Units : MonoBehaviour
{
    public Fraction fraction = Fraction.Neutral;
    public int strength = 5;
    public float tickIntervall = 0.25f;
    public PointGeneration target;
    public AudioClip spawnSound;

    private bool hasGoalReached = false;
    private bool isInterrupted = false;
    private float lastTick;

    private void Start()
    {
        AudioManager.Instance.PlaySound(spawnSound);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PointGeneration poi) && poi == target)
        {
            hasGoalReached = true;
            OnPOIAttackStep();
            lastTick = Time.fixedTime;
        }
        else if (collision.TryGetComponent(out Units enemies))
        {
            isInterrupted = true;
            OnUnitInteruptStep(enemies);
            lastTick = Time.fixedTime;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Units enemies))
        {
            if (Time.fixedTime >= lastTick + tickIntervall)
            {
                OnUnitInteruptStep(enemies);
                lastTick += tickIntervall;
            }
        }
    }

    private void FixedUpdate()
    {
        if (hasGoalReached && Time.fixedTime >= lastTick + tickIntervall)
        {
            OnPOIAttackStep();
            lastTick += tickIntervall;
        }
        if (strength <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnPOIAttackStep()
    {
        if (strength > 0)
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
    }


    private void OnUnitInteruptStep(Units enemies)
    {
        if (fraction != target.fraction && strength > 0 && enemies.strength > 0)
        {
            enemies.strength -= 1;
            strength -= 1;
        }
        else
        {
            // Merge?
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
