using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class PointTracker : MonoBehaviour
{
    public static PointTracker Instance;

    public TextMeshProUGUI armyUnitsValue;
    public TextMeshProUGUI enemyUnitsValue;
    public GameObject resultat;
    public GameObject sieg;
    public GameObject niederlage;

    public UnityAction OnPlayerChanged;
    public UnityAction OnEnemyChanged;

    [NonSerialized] public int playerForts = 0;
    [NonSerialized] public int enemyForts = 0;
    [NonSerialized] public int neutralForts = 0;

    [NonSerialized] public int playerUnits = -1;
    [NonSerialized] public int enemyUnits = -1;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance);
        }
        Instance = this;
    }

    void FixedUpdate()
    {
        int pForts = 0;
        int eForts = 0;
        int nForts = 0;
        int pUnits = 0;
        int eUnits = 0;
        GameManager.Instance.allForts.ForEach(poi =>
        {
            if (poi.fraction == Fraction.Player)
            {
                pForts++;
                pUnits += poi.currentPoints;
            }
            else if (poi.fraction == Fraction.Enemy)
            {
                eForts++;
                eUnits += poi.currentPoints;
            }
            else
            {
                nForts++;
            }
        });
        if (playerUnits != -1 && playerUnits != pUnits)
        {
            OnPlayerChanged?.Invoke();
        }
        if (enemyUnits != -1 && enemyUnits != eUnits)
        {
            OnEnemyChanged?.Invoke();
        }
        playerForts = pForts;
        enemyForts = eForts;
        neutralForts = nForts;
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
