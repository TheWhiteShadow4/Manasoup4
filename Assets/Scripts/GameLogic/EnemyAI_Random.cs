using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyAI_Random : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    List <Fort> pointGenerations = new List<Fort>();
    List<Fort> pointGenerationsPlayer = new List<Fort>();
    List<Fort> pointGenerationsEnemy = new List<Fort>();

    public float aiActionTime = 10f;

    GameManager gameManager;

    
    void Start(){
        gameManager = GameManager.Instance;
        pointGenerations = new List<Fort>(FindObjectsByType<Fort>(FindObjectsSortMode.None));

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
                Fort randomEnemyPoi = pointGenerationsEnemy[randomIndex];
                int randomIndex2 = Random.Range(0, pointGenerationsPlayer.Count);
                Fort randomPlayerPoi = pointGenerationsPlayer[randomIndex2];
                int usedRaidSize = randomEnemyPoi.currentPoints / 2;
                randomEnemyPoi.currentPoints -= usedRaidSize;
                gameManager.StartRaid(randomEnemyPoi, randomPlayerPoi, usedRaidSize);
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }


}
