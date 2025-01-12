using TMPro;
using UnityEngine;

public class PointTracker : MonoBehaviour
{
    public TextMeshProUGUI armyUnitsValue;
    public TextMeshProUGUI enemyUnitsValue;
    public GameObject resultat;
    public GameObject sieg;
    public GameObject niederlage;

    void FixedUpdate()
    {
        int armyUnits = 0;
        int enemyUnits = 0;
        GameManager.Instance.allPois.ForEach(poi =>
        {
            if (poi.fraction == Fraction.Player)
            {
                armyUnits += poi.currentPoints;
            }
            else if (poi.fraction == Fraction.Enemy)
            {
                enemyUnits += poi.currentPoints;
            }
        });
        armyUnitsValue.text = armyUnits.ToString();
        enemyUnitsValue.text = enemyUnits.ToString();


        if (enemyUnits == 0)
        {
            Time.timeScale = 0;
            resultat.SetActive(true);
            sieg.SetActive(true);
        }

        if (armyUnits == 0)
        {
            Time.timeScale = 0;
            resultat.SetActive(true);
            niederlage.SetActive(true);
        }

    }
}
