using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyAI_RangeAndStrength : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    List <PointGeneration> pointGenerations = new List<PointGeneration>();
    List<PointGeneration> pointGenerationsPlayer = new List<PointGeneration>();
    List<PointGeneration> pointGenerationsEnemy = new List<PointGeneration>();

    public float aiActionTime = 10f;

    GameManager gameManager;

    
    void Start(){
        gameManager = GameManager.Instance;
        pointGenerations = new List<PointGeneration>(FindObjectsByType<PointGeneration>(FindObjectsSortMode.None));

        StartCoroutine(aiAction());
    }

    void updateFactionsLists(){
        pointGenerationsPlayer.Clear();
        pointGenerationsEnemy.Clear();
        foreach (var point in pointGenerations){
            if (point.fraction == Fraction.Player || point.fraction == Fraction.Neutral){
                pointGenerationsPlayer.Add(point);
            }
            else if (point.fraction == Fraction.Enemy){
                pointGenerationsEnemy.Add(point);
            }
        }
    }


    IEnumerator aiAction(){
        while (true){
            yield return new WaitForSeconds(aiActionTime);
            updateFactionsLists();
            if (pointGenerationsEnemy.Count > 0 && pointGenerationsPlayer.Count > 0){
                //get pointGenerationsPlayer with the lowest currentPoints
                PointGeneration targetPlayerPoint = pointGenerationsPlayer[0];
                foreach (var point in pointGenerationsPlayer){
                    if (point.currentPoints < targetPlayerPoint.currentPoints){
                        targetPlayerPoint = point;
                    }
                }
                //get pointGenerationsEnemy which is closest to targetPlayerPoint and has more currentPoints than targetPlayerPoint/2
                PointGeneration sourceEnemyPoint = null;
                float closestDistance = float.MaxValue;
                foreach (var point in pointGenerationsEnemy){
                    float distance = Vector3.Distance(point.gameObject.transform.position, targetPlayerPoint.gameObject.transform.position);
                    if (distance < closestDistance && point.currentPoints/2 > targetPlayerPoint.currentPoints){
                        closestDistance = distance;
                        sourceEnemyPoint = point;
                    }
                }
                if (sourceEnemyPoint == null){
                    sourceEnemyPoint = pointGenerationsEnemy[0];
                    foreach (var point in pointGenerationsEnemy){
                        if (point.currentPoints > sourceEnemyPoint.currentPoints){
                            sourceEnemyPoint = point;
                        }
                    }
                }
                sourceEnemyPoint.currentPoints -= sourceEnemyPoint.currentPoints/2;
                gameManager.StartRaid(sourceEnemyPoint, targetPlayerPoint, sourceEnemyPoint.currentPoints);
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }


}
