using Events;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public SelectionEventChannelSO selectionChangedEvent;

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
