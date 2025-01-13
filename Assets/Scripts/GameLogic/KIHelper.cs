using System;
using UnityEngine;

public class KIHelper
{
    public static Fort FindLowestUnitFort(Func<Fort, bool> test)
    {
        Fort lowest = null;
        int minPoints = int.MaxValue;
        foreach (var fort in GameManager.Instance.allForts)
        {
            if (test(fort) && fort.currentPoints < minPoints)
            {
                lowest = fort;
                minPoints = fort.currentPoints;
            }
        }
        return lowest;
    }

    public static Fort FindLowestUnitFort(Fraction fraction)
    {
        Fort lowest = null;
        int minPoints = int.MaxValue;
        foreach (var fort in GameManager.Instance.allForts)
        {
            if (fort.fraction == fraction && fort.currentPoints < minPoints)
            {
                lowest = fort;
                minPoints = fort.currentPoints;
            }
        }
        return lowest;
    }

    public static Fort FindHigestUnitFort(Fraction fraction)
    {
        Fort lowest = null;
        int maxPoints = int.MinValue;
        foreach (var fort in GameManager.Instance.allForts)
        {
            if (fort.fraction == fraction && fort.currentPoints > maxPoints)
            {
                lowest = fort;
                maxPoints = fort.currentPoints;
            }
        }
        return lowest;
    }

    public static Fort FindClosestFort(Vector3 position, Func<Fort, bool> test)
    {
        Fort closest = null;
        float minSqrDist = float.MaxValue;
        foreach (var fort in GameManager.Instance.allForts)
        {
            float dist = Vector3.SqrMagnitude(position - fort.transform.position);
            if (test.Invoke(fort) && dist * dist < minSqrDist)
            {
                closest = fort;
                minSqrDist = dist * dist;
            }
        }
        return closest;
    }
}