using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public bool pause = false;
    public GameObject Menü;
    public GameObject knopf;
    public GameObject optionen;
    public GameObject gamemanager;
    


    public void pausegedrückt()
    {     
        Time.timeScale = 0;
        Menü.SetActive(true);
        knopf.SetActive(false);
        gamemanager.SetActive(false);
    }

    public void weitermachen()
    {
        Time.timeScale = 1;
        Menü.SetActive(false);
        knopf.SetActive(true);
        gamemanager.SetActive(true);    
    }

    public void optionendrücken()
    {
        optionen.SetActive(true);
        Menü.SetActive(false);
    }

    public void optionenverlassen()
    {
        optionen.SetActive(false);
        Menü.SetActive(true);
    }

    public void hauptmenüladen()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("TitleScene");
    }

    public void Neustart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("GamaSceneL1");
    }

    public void tutorialbeendet()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("GamaSceneL1");
    }
}
