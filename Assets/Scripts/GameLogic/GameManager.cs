using Events;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public SelectionEventChannelSO selectionChangedEvent;

    private Camera activeCamera;

    public Camera ActiveCamera
    {
        get
        {
            if (activeCamera == null)
            {
                activeCamera = Camera.main;
            }
            return activeCamera;
        }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
}
