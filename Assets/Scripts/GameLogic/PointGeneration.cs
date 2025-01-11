using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PointGeneration : MonoBehaviour
{
    public static int PlayerLayer = 6;
    public static int EnemyLayer = 7;
    public static int NeutralLayer = 8;

    public Fraction fraction = Fraction.Neutral;
    public int maxPoints = 100;
    public int pointPerSec = 1;
    public float pointInterval = 1f;
    public int currentPoints = 0;
    public bool isCounting = false;
    private float lastTick;

    public SpriteRenderer spriteRenderer;
    public PointMarker pointMarkerPrefab;

    private PointMarker pointMarker;

    private void Start()
    {
        if (pointMarker == null)
        {
            pointMarker = Instantiate(pointMarkerPrefab, GameManager.Instance.hud.transform);
            pointMarker.transform.position = GameManager.Instance.ActiveCamera.WorldToScreenPoint(transform.position);
        }

        CapturePoi(fraction);
    }

    void CapturePoi(Fraction newFraction)
    {
        fraction = newFraction;
        isCounting = newFraction != Fraction.Neutral;
        lastTick = Time.fixedTime;
        MarkPoiForFraction();
    }

    private void FixedUpdate()
    {
        
        if (isCounting && currentPoints < maxPoints && Time.fixedTime > lastTick + pointInterval)
        {
            Debug.Log(lastTick);
            currentPoints = Mathf.Min(currentPoints + pointPerSec, maxPoints);
            lastTick = Time.fixedTime;
        }

        

        if (!pointMarker) return;
        if (currentPoints == 0)
        {
            pointMarker.enabled = false;
        }
        else
        {
            pointMarker.enabled = true;
        }

        pointMarker.UpdateText(currentPoints.ToString());
    }

    private void MarkPoiForFraction()
    {
        switch (fraction)
        {
            case Fraction.Neutral:
                spriteRenderer.color = Color.grey;
                gameObject.layer = NeutralLayer;
                break;
            case Fraction.Player:
                spriteRenderer.color = Color.green;
                gameObject.layer = PlayerLayer;
                break;
            case Fraction.Enemy:
                spriteRenderer.color = Color.red;
                gameObject.layer = EnemyLayer;
                break;
        }
    }
}
