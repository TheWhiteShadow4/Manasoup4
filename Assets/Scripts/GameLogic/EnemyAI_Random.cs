using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyAI_Random : MonoBehaviour
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
                int randomIndex = Random.Range(0, pointGenerationsEnemy.Count);
                PointGeneration randomEnemyPoi = pointGenerationsEnemy[randomIndex];
                int randomIndex2 = Random.Range(0, pointGenerationsPlayer.Count);
                PointGeneration randomPlayerPoi = pointGenerationsPlayer[randomIndex2];
                randomEnemyPoi.currentPoints -= randomEnemyPoi.currentPoints/2;
                gameManager.StartRaid(randomEnemyPoi.gameObject, randomPlayerPoi, randomEnemyPoi.currentPoints/2);
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }


}
