using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MenuPageSwitcher : MonoBehaviour
{
    public GameObject[] pages;
    public GameObject backButton;

    public AudioSlider[] audioSliders;
    public GameObject boboToggleMarker;

    void Start()
    {
        foreach (var slider in audioSliders)
        {
            slider.ApplyPlayerPrefs();
        }
        ShowPage(0);
    }

    public void ShowPage(int index)
    {
        for (int i = 0; i < pages.Length; i++)
        {
            if (i == index)
            {
                pages[i].SetActive(true);
            }
            else
            {
                pages[i].SetActive(false);
            }
        }
        backButton.SetActive(index != 0);
    }

    public void LoadScene(int buildIndex)
    {
        SceneManager.LoadScene(buildIndex);
    }

    public void ToggleBoboMode()
    {
       ModeHandler.Instance.ToggleMode();
       boboToggleMarker.SetActive(ModeHandler.Instance.boboMode);
    }

    public void ExitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
