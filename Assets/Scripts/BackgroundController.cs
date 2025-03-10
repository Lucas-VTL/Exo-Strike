using UnityEngine;
using UnityEngine.UI;

public class BackgroundController : MonoBehaviour
{
    public RawImage image;
    private float _xOffset = 0.05f;
    private float _yOffset = 0.1f;
    
    void Update()
    {
        image.uvRect = new Rect(image.uvRect.position + new Vector2(_xOffset, _yOffset) * Time.deltaTime, image.uvRect.size);
    }
}
