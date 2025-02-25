using UnityEngine;
using UnityEngine.SceneManagement;

public class SystemController : MonoBehaviour
{
    public void RestartGame()
    {
        SceneManager.LoadScene("Main Scence");
    }
}
