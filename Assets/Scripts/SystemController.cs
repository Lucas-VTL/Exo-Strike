using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SystemController : MonoBehaviour
{
    public Animator transition;
    private float _transitionTime = 1f;
    
    public void RestartGame()
    {
        StartCoroutine(LoadingScence("Main Scene"));
    }

    IEnumerator LoadingScence(string sceneName)
    {
        transition.SetTrigger("isStartLoading");
        yield return new WaitForSeconds(_transitionTime);
        SceneManager.LoadSceneAsync(sceneName);
        transition.SetTrigger("isEndLoading");
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(LoadingScence("Main Scene"));
        }
    }
}
