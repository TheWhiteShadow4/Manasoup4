using UnityEngine;

public class ModeHandler : MonoBehaviour
{
    public static ModeHandler Instance;

    public bool boboMode;
    public GameObject toggleMarker;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
        }
    }

    public void ToggleMode()
    {

       boboMode = !boboMode;
       if (toggleMarker) toggleMarker.SetActive(boboMode);

    }
}
