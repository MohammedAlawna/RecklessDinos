using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsManager : MonoBehaviour
{

    public GameObject _pausedMenu = null;
    public GameObject infoLayout = null;
   

    //Movement Method.


   public void MoveToLeft()
    {

    }

   public void MoveToRight()
    {

    }
    

    public void PlayButton()
    {
        StartCoroutine(PlaySFXWithDelay("LevelSelector"));
       // SceneManager.LoadScene("LevelSelector");
    }

    public void QuitButton() {
        Application.Quit();
    }

    public void MainMenuButton(){
        StartCoroutine(PlaySFXWithDelay("MainMenu"));
       // SceneManager.LoadScene("MainMenu");

    }

    public void SettingsButton() { }

    public void InfoButton()
    {
        ProcessClickSFX();
        if(!infoLayout.activeInHierarchy)
        {
            infoLayout.SetActive(true);
        }
        else
        {
            infoLayout.SetActive(false);
        }
        
    }

    public void FacebookButton()
    {
        Application.OpenURL("http://fb.com/MuhammadAlawneh99");
    }

    public void YoutubeButton()
    {
        Application.OpenURL("https://www.youtube.com/channel/UCsZDglwqL4AkmflfaSmdGow");
    }

    void ProcessClickSFX()
    {
        AudioManager.i.PlaySound(AudioManager.i.gameSFX[1]);
    }

   // public void ShoppingButton() { }


   /* public void MuteButton()
    {

    }

    public void UnMuteButton()
    {

    }*/
    IEnumerator PlaySFXWithDelay(string sceneName)
    {
        ProcessClickSFX();
        yield return new WaitForSeconds(0.25f);
        SceneManager.LoadScene(sceneName);
    }

    public void ShowPausedMenu()
    {
        _pausedMenu.SetActive(true);
        GameManager._singletonVar._gamePaused = true;

    }

    public void ResumeGame()
    {
        _pausedMenu.SetActive(false);
        GameManager._singletonVar._gamePaused = false;
    }
}
