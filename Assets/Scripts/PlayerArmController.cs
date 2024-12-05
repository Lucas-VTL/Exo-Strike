using System;
using UnityEngine;

public class PlayerArmController : MonoBehaviour
{
    public GameObject player;
    
    private Camera _mainCamera;
    private float _yOffset = 0.6f;
    
    void Start()
    {
        _mainCamera = Camera.main;
    }
    
    void Update()
    {
        var mousePos = Input.mousePosition;
        mousePos = _mainCamera.ScreenToWorldPoint(mousePos);
        mousePos.z = 0;
        
        Vector3 direction = (mousePos - transform.position).normalized;
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        angle = Mathf.Repeat(angle, 360f);

        if (angle <= 60 || angle >= 300)
        {
            transform.localScale = new Vector3(1, 1, 1);
            player.transform.localScale = new Vector3(1, 1, 1);
            
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
        else if (angle >= 120 && angle <= 240)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            player.transform.localScale = new Vector3(-1, 1, 1);
            
            transform.rotation = Quaternion.Euler(0, 0, angle + 180);
        }
        
        transform.position = player.transform.position + new Vector3(0, _yOffset, 0);
    }
}