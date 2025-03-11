using Unity.VisualScripting;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    public Texture2D fireCursorTexture;
    public Texture2D prohibitCussorTexture;
    private Vector2 _cursorHotSpot;
    private CursorMode _cursorMode = CursorMode.ForceSoftware;
    private GameObject _player;
    private bool _canFire;
    
    void Start()
    {
        _player = GameObject.Find("Body");
        _cursorHotSpot = new Vector2(fireCursorTexture.width / 2, fireCursorTexture.height / 2);
    }

    void Update()
    {
        _canFire = _player.gameObject.GetComponent<PlayerController>().GetCanFire();
        
        if (_canFire)
        {
            Cursor.SetCursor(fireCursorTexture, _cursorHotSpot, _cursorMode);
        }
        else
        {
            Cursor.SetCursor(prohibitCussorTexture, _cursorHotSpot, _cursorMode);
        }
    }
}
