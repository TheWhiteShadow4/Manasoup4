using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public bool pause = false;
    public GameObject Men�;
    public GameObject knopf;
    public GameObject optionen;
    public GameObject gamemanager;
    


    public void pausegedr�ckt()
    {     
        Time.timeScale = 0;
        Men�.SetActive(true);
        knopf.SetActive(false);
        gamemanager.SetActive(false);
    }

    public void weitermachen()
    {
        Time.timeScale = 1;
        Men�.SetActive(false);
        knopf.SetActive(true);
        gamemanager.SetActive(true);    
    }

    public void optionendr�cken()
    {
        optionen.SetActive(true);
        Men�.SetActive(false);
    }

    public void optionenverlassen()
    {
        optionen.SetActive(false);
        Men�.SetActive(true);
    }

    public void hauptmen�laden()
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
