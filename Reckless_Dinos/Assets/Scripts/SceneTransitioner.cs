using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitioner : MonoBehaviour
{
    public Animator transition;
    public float transitionDuration;
    
  

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            LoadNextLevel();
           
            
        }
        
    }

    private void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        //Play Animation.
       // transition.SetTrigger("Start");

        //Stop.
        yield return new WaitForSeconds(transitionDuration);

        //Load Scene.
        SceneManager.LoadScene(levelIndex);
        

    }
}
