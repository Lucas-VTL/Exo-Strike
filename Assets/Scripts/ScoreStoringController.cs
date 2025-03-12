using UnityEngine;

public class ScoreStoringController : MonoBehaviour
{
    public GameObject player;
    private int _totalScore;
    
    private void OnEnable()
    {
        if (player)
        {
            player.gameObject.GetComponent<PlayerController>().OnDead += TotalScore;   
        }
    }

    private void OnDisable()
    {
        if (player)
        {
            player.gameObject.GetComponent<PlayerController>().OnDead -= TotalScore;
        }
    }

    void TotalScore(int score)
    {
        _totalScore = score;
    }

    public int GetTotalScore()
    {
        return _totalScore;
    }
}
