using UnityEngine;

public class ShadowController : MonoBehaviour   
{
    public GameObject player;
    private PlayerController _playerController;
    private float _yOffset = -1.4f;

    void Start()
    {
        _playerController = player.GetComponent<PlayerController>();
    }
    
    void Update()
    {
        transform.position = player.transform.position + new Vector3(0, _yOffset, 0);

        if (_playerController)
        {
            int playerSpeed = _playerController.GetPlayerSpeed();
            if (playerSpeed == 0)
            {
                transform.localScale = new Vector3(0.5f, 0.6f, 0);
            }
            else if (playerSpeed == 4)
            {
                transform.localScale = new Vector3(0.5f, 0.8f, 0);
            }
            else
            {
                transform.localScale = new Vector3(0.5f, 1f, 0);
            }
        }
    }
}
