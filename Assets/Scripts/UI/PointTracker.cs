using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class PointTracker : MonoBehaviour
{
    public TextMeshProUGUI armyUnitsValue;
    public TextMeshProUGUI enemyUnitsValue;
    public GameObject resultat;
    public GameObject sieg;
    public GameObject niederlage;

    public UnityAction OnPlayerChanged;
    public UnityAction OnEnemyChanged;

    [NonSerialized] public int playerUnits = -1;
    [NonSerialized] public int enemyUnits = -1;

    void FixedUpdate()
    {
        int pUnits = 0;
        int eUnits = 0;
        GameManager.Instance.allPois.ForEach(poi =>
        {
            if (poi.fraction == Fraction.Player)
            {
                pUnits += poi.currentPoints;
            }
            else if (poi.fraction == Fraction.Enemy)
            {
                eUnits += poi.currentPoints;
            }
        });
        if (playerUnits != -1 && playerUnits != pUnits)
        {
            OnPlayerChanged?.Invoke();
        }
        if (enemyUnits != -1 && enemyUnits != eUnits)
        {
            OnPlayerChanged?.Invoke();
        }
        playerUnits = pUnits;
        enemyUnits = eUnits;
        armyUnitsValue.text = playerUnits.ToString();
        enemyUnitsValue.text = enemyUnits.ToString();


        if (enemyUnits == 0)
        {
            Time.timeScale = 0;
            resultat.SetActive(true);
            sieg.SetActive(true);
            enabled = false;
        }

        if (playerUnits == 0)
        {
            Time.timeScale = 0;
            resultat.SetActive(true);
            niederlage.SetActive(true);
            enabled = false;
        }
    }
}
