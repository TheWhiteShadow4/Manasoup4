using TMPro;
using UnityEngine;

public class PointTracker : MonoBehaviour
{
    public TextMeshProUGUI armyUnitsValue;
    public TextMeshProUGUI enemyUnitsValue;

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
    }
}
