using System;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.Cinemachine;
using Unity.Cinemachine.Editor;

public class SystemController : MonoBehaviour
{
    public Animator transition;
    private float _transitionTime = 1f;
    public MonsterParameter[] monsterParameters;
    public ProjectileParameter[] projectileParameters;
    public GameObject cinemachineCamera;
    public GameObject demonProjectileEffect;
    public GameObject altarEffect;
    
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
        
        foreach (var parameter in monsterParameters)
        {
            parameter.ResetData();
        }

        foreach (var parameter in projectileParameters)
        {
            parameter.ResetData();
        }

        if (cinemachineCamera != null)
        {
            cinemachineCamera.gameObject.GetComponent<CinemachineCamera>().Lens.OrthographicSize = 9;
            cinemachineCamera.gameObject.GetComponent<CinemachineConfiner2D>().InvalidateLensCache();   
        }

        if (demonProjectileEffect != null)
        {
            demonProjectileEffect.transform.localScale = new Vector3(1, 1, 1);   
        }

        if (altarEffect != null)
        {
            altarEffect.transform.localScale = new Vector3(1, 1, 1);   
        }
        
        if (sceneName.Length > 0)
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            if (Application.isEditor)
            {
                //  EditorApplication.isPlaying = false;  
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
