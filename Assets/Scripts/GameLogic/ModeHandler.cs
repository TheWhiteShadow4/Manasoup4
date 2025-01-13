using Difficulty;
using UnityEngine;

public class ModeHandler : MonoBehaviour
{
    public static ModeHandler Instance;

    public Level difficulty = Level.Mittel;
    public bool boboMode;

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
    }
}
