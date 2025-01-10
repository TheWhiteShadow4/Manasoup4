using UnityEngine;

public class MenuPageSwitcher : MonoBehaviour
{
    public GameObject[] pages;
    public GameObject backButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ShowPage(0);
    }

    // Update is called once per frame
    void Update()
    {
        
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

    public void ExitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
