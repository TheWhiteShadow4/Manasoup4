using UnityEngine;
using System.Collections;

public class EnemyAI_RangeAndStrength : MonoBehaviour
{
    public float aiActionTimeMin = 2f;
    public float aiActionTimeCurrent = 8f;
    public float aiActionTimeScale = 0.5f;

    void Start()
    {
        StartCoroutine(aiAction());
        StartCoroutine(scaleTimer());
    }

    IEnumerator scaleTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(10);
            aiActionTimeCurrent -= aiActionTimeScale;
            if (aiActionTimeCurrent < aiActionTimeMin)
            {
                aiActionTimeCurrent = aiActionTimeMin;
                break;
            }
        }
    }


    IEnumerator aiAction()
    {
        while (true)
        {
            yield return new WaitForSeconds(aiActionTimeCurrent);

            if (PointTracker.Instance.enemyForts > 0 && PointTracker.Instance.playerForts > 0)
            {
                Fort lowestFort = KIHelper.FindLowestUnitFort((Fort fort) => fort.fraction != Fraction.Enemy);

                Fort sourceEnemyPoint = KIHelper.FindClosestFort(lowestFort.transform.position,
                    (Fort fort) => fort.fraction == Fraction.Enemy && fort.currentPoints > lowestFort.currentPoints * 2);

                if (sourceEnemyPoint == null)
                {
                    sourceEnemyPoint = KIHelper.FindHigestUnitFort(Fraction.Enemy);
                }
                sourceEnemyPoint.currentPoints -= sourceEnemyPoint.currentPoints / 2;
                GameManager.Instance.StartRaid(sourceEnemyPoint, lowestFort, sourceEnemyPoint.currentPoints);
            }
        }
    }
}
