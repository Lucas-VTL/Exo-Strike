using System.Collections;
using TMPro;
using UnityEngine;

public class ScoreUIDisplay : MonoBehaviour
{
    public GameObject totalScoreStorage;
    private int _score = 0;
    
    void Start()    
    {
        _score = totalScoreStorage.GetComponent<ScoreStoringController>().GetTotalScore();
        StartCoroutine(DisplayScore());
    }
    
    IEnumerator DisplayScore()
    {
        float elapsedTime = 0f;
        TextMeshProUGUI textGUI = gameObject.GetComponent<TextMeshProUGUI>();
        
        while (elapsedTime < 1f)
        {
            float currentValue = Mathf.Lerp(0, _score, elapsedTime / 1f);
            textGUI.text = Mathf.RoundToInt(currentValue).ToString("N0").Replace(",", ".");;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        
        textGUI.text = Mathf.RoundToInt(_score).ToString("N0").Replace(",", ".");
    }
}
