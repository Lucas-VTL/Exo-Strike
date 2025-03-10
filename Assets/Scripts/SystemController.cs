using System;
using System.Collections;
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

    IEnumerator LoadingScence(string sceneName)
    {
        transition.SetTrigger("isStartLoading");
        yield return new WaitForSeconds(_transitionTime);
        SceneManager.LoadScene(sceneName);
        transition.SetTrigger("isEndLoading");
        yield return new WaitForSeconds(_transitionTime);
    }   
}
