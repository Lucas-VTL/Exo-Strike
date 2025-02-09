using UnityEngine;

public class CursorController : MonoBehaviour
{
    public Texture2D cursorTexture;
    private Vector2 _cursorHotSpot;
    private CursorMode _cursorMode = CursorMode.ForceSoftware;
    
    void Start()
    {
        _cursorHotSpot = new Vector2(cursorTexture.width / 2, cursorTexture.height / 2);
        Cursor.SetCursor(cursorTexture, _cursorHotSpot, _cursorMode);
    }
}
