using UnityEngine;

public class CursorController : MonoBehaviour
{
    public Texture2D cursorTexture;
    private Vector2 _cursorHotSpot = Vector2.zero;
    private CursorMode _cursorMode = CursorMode.ForceSoftware;
    
    void Start()
    {
        Cursor.SetCursor(cursorTexture, _cursorHotSpot, _cursorMode);
    }
}
