using System;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SystemController : MonoBehaviour
{
    public Animator transition;
    private float _transitionTime = 1f;
    
    public void PlayGame()
    {
        StartCoroutine(LoadingScence("Main Scene"));
    }

    public void BackToMenu()
    {
        StartCoroutine(LoadingScence("Menu"));
    }

    public void QuitGame()
    {
        StartCoroutine(LoadingScence(""));
    }

    IEnumerator LoadingScence(string sceneName)
    {
        transition.SetTrigger("isStartLoading");
        yield return new WaitForSeconds(_transitionTime);
        if (sceneName.Length > 0)
        {
            SceneManager.LoadScene(sceneName);   
        }
        else
        {
            if (Application.isEditor)
            {
                //EditorApplication.isPlaying = false;  
            }
            else
            {
                Application.Quit();     
            }   
        }
        transition.SetTrigger("isEndLoading");
        yield return new WaitForSeconds(_transitionTime);
    }   
}
