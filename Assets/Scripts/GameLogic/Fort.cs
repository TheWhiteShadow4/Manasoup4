using UnityEngine;
using UnityEngine.UI;

[SelectionBase]
public class Fort : SelectableObject
{
    public int maxPoints = 100;
    public float pointInterval = 1f;
    public int currentPoints = 0;
    public float raidCooldown = 3f;
    public bool isCounting = false;
    private float lastTick;

    public AudioClip playerCaptureSound;
    public AudioClip enemyCaptureSound;
    public GameObject playerCaptureEffect;
    public GameObject enemyCaptureEffect;

    public SpriteRenderer spriteRenderer;
    public PointMarker pointMarkerPrefab;
    public Image cooldownPrefab;

    private PointMarker pointMarker;
    private Image cooldownImage;
    private float cooldownTimer;

    private void Start()
    {
        selectMarker = gameObject.transform.Find("SelectMarker").gameObject;
        MarkAsSelected(false);
        if (pointMarker == null)
        {
            pointMarker = Instantiate(pointMarkerPrefab, GameManager.Instance.hud.transform);
            pointMarker.transform.position = GameManager.Instance.ActiveCamera.WorldToScreenPoint(transform.position);
        }
        if (cooldownImage == null)
        {
            cooldownImage = Instantiate(cooldownPrefab, GameManager.Instance.hud.transform);
            cooldownImage.transform.position = GameManager.Instance.ActiveCamera.WorldToScreenPoint(transform.position);
            cooldownImage.enabled = false;
        }

        isCounting = fraction != Fraction.Neutral;
        lastTick = Time.fixedTime;
        MarkPoiForFraction();
    }

    private void OnEnable()
    {
        GameManager.Instance.RegisterPoi(this);
    }

    private void OnDisable()
    {
        GameManager.Instance.UnregisterPoi(this);
    }

    public override void Attack()
    {
        GameManager.Instance.raidEvent.RaiseEvent(this);
    }

    public bool CanStartRaid()
    {
        return cooldownTimer <= 0;
    }

    public void SetRaidCooldown()
    {
        cooldownTimer = raidCooldown;
        cooldownImage.enabled = true;
    }

    public void CapturePoi(Fraction newFraction, int unitCount)
    {
        fraction = newFraction;
        if (newFraction == Fraction.Player)
        {
            AudioManager.Instance.PlaySound(playerCaptureSound);
            GameObject effect = Instantiate(playerCaptureEffect, transform.position, Quaternion.identity);
            Destroy(effect, 2f);
        }
        else if (newFraction == Fraction.Enemy)
        {
            AudioManager.Instance.PlaySound(enemyCaptureSound);
            GameObject effect = Instantiate(enemyCaptureEffect, transform.position, Quaternion.identity);
            Destroy(effect, 2f);
        }
        cooldownTimer = 0;
        cooldownImage.enabled = true;
        currentPoints = unitCount;
        isCounting = newFraction != Fraction.Neutral;
        lastTick = Time.fixedTime;
        MarkPoiForFraction();
    }

    private void FixedUpdate()
    {
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.fixedDeltaTime;
            cooldownImage.enabled = cooldownTimer > 0;
            cooldownImage.fillAmount = cooldownTimer / raidCooldown;
        }
        else
        {
            cooldownImage.enabled = false;
        }

        if (isCounting && Time.fixedTime > lastTick + pointInterval)
        {
            currentPoints = Mathf.Min(currentPoints + 1, maxPoints);
            lastTick += pointInterval;
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
