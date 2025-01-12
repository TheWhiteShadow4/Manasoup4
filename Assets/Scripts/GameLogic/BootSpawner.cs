using UnityEngine;

public class BootSpawner : MonoBehaviour
{
    public bool active = true;
    public int playerUnitsForTrigger = 100;
    public PointTracker tracker;

    public GameObject boot;
    public float bootSpeed = 2;
    public GameObject bootPoiPrefab;

    private bool bootHasStarted;

    private void Start()
    {
        tracker.OnPlayerChanged += OnPlayerChanged;
    }

    private void OnPlayerChanged()
    {
        if (active && tracker.playerUnits >= playerUnitsForTrigger)
        {
            active = false;
            tracker.OnPlayerChanged -= OnPlayerChanged;
            bootHasStarted = true;
        }
    }

    private void FixedUpdate()
    {
        if (!bootHasStarted || !boot) return;

        boot.transform.position += Vector3.right * bootSpeed * Time.fixedDeltaTime;
        if (Vector3.Distance(boot.transform.position, transform.position) < 1)
        {
            Destroy(boot);
            Instantiate(bootPoiPrefab, transform.position, Quaternion.identity);
            enabled = false;
        }
    }
}