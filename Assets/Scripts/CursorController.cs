using Unity.VisualScripting;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    private GameObject _player;
    public Texture2D fireCursorTexture;
    public Texture2D prohibitCussorTexture;
    private Vector2 _cursorHotSpot;
    private CursorMode _cursorMode = CursorMode.ForceSoftware;

    private void OnEnable()
    {
        _player = GameObject.Find("Body");
        _cursorHotSpot = new Vector2(fireCursorTexture.width / 2, fireCursorTexture.height / 2);

        _player.gameObject.GetComponent<PlayerController>().OnShootAngleChange += CursorUIControll;
    }

    private void OnDisable()
    {
        _player.gameObject.GetComponent<PlayerController>().OnShootAngleChange -= CursorUIControll;
    }

    private void CursorUIControll(bool canFire)
    {
        if (canFire)
        {
            Cursor.SetCursor(fireCursorTexture, _cursorHotSpot, _cursorMode);   
        }
        else
        {
            Cursor.SetCursor(prohibitCussorTexture, _cursorHotSpot, _cursorMode);
        }
    }
}
