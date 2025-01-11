using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PointGeneration : MonoBehaviour
{
    public int maxPoints = 100;
    public int pointPerSec = 1;
    public int pointInterval = 1;
    public int currentPoints = 0;
    public bool isCounting = false;

    public TextMeshProUGUI pointsText;

   
    private void Update()
    {
        if (isCounting == false && gameObject.CompareTag("Captured"))
        {
            StartCoroutine(count());
        }

        if (gameObject.CompareTag("Captured") == false)
        {
            isCounting = false;
        }

        if (currentPoints == 0)
        {
            pointsText.enabled = false;
        }
        else
        {
            pointsText.enabled = true;
        }

        pointsText.text = "" + currentPoints;
    }

    private IEnumerator count()
    {
        isCounting = true;

        while (currentPoints < maxPoints && gameObject.CompareTag("Captured"))
        {
           currentPoints += pointPerSec;
           yield return new WaitForSeconds(pointInterval);
        }      
    }
}
