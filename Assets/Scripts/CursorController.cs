using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CursorController : MonoBehaviour
{
    public GameObject player;
    public Texture2D fireCursorTexture;
    public Texture2D prohibitCussorTexture;
    public Slider reloadCursor;
    
    private Vector2 _cursorHotSpot;
    private CursorMode _cursorMode = CursorMode.ForceSoftware;
    private bool _isReloading = false;
    private RectTransform _sliderRectTransform;
    private float _reloadTime;
    private float _reloadTimer;
    
    private Color _startSliderColor = new Color(255f / 255f, 20f / 255f, 0f / 255f);
    private Color _endSliderColor = new Color(0f / 255f, 255f / 255f, 72f / 255f);

    private void OnEnable()
    {
        _cursorHotSpot = new Vector2(fireCursorTexture.width / 2, fireCursorTexture.height / 2);
        _sliderRectTransform = reloadCursor.GetComponent<RectTransform>();

        if (player)
        {
            _reloadTime = player.gameObject.GetComponent<PlayerController>().GetReloadTime();
        
            player.gameObject.GetComponent<PlayerController>().OnShootAngleChange += CursorUIControll;
            player.gameObject.GetComponent<PlayerController>().OnReload += CursorReloadControl;   
        }
        
        reloadCursor.gameObject.SetActive(false);
        reloadCursor.maxValue = _reloadTime;
        reloadCursor.onValueChanged.AddListener(OnSliderChanged);
    }

    private void OnDisable()
    {
        if (player)
        {
            player.gameObject.GetComponent<PlayerController>().OnShootAngleChange -= CursorUIControll;
            player.gameObject.GetComponent<PlayerController>().OnReload -= CursorReloadControl;
        }
    }

    private void CursorUIControll(bool canFire)
    {
        if (!_isReloading)
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

    private void CursorReloadControl(bool isReload)
    {
        if (isReload)
        {
            reloadCursor.gameObject.SetActive(true);
            Cursor.visible = false;
            _reloadTimer = _reloadTime;
        }
        else
        {
            reloadCursor.gameObject.SetActive(false);
            Cursor.visible = true;
        }
        
        _isReloading = isReload;
    }

    private void Update()
    {
        if (_isReloading)
        {
            if (_reloadTimer > 0)
            {
                _reloadTimer -= Time.deltaTime;
                reloadCursor.value = _reloadTimer;
                
                Vector2 mousePosition = Input.mousePosition;
                Vector2 localPoint;
                RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    _sliderRectTransform.parent.GetComponent<RectTransform>(), 
                    mousePosition, 
                    null, 
                    out localPoint
                );
                _sliderRectTransform.localPosition = localPoint; 
            }
        }
    }
    
    private void OnSliderChanged(float value)
    {
        var t = value / reloadCursor.maxValue;
        var newRed =  Mathf.Lerp(_endSliderColor.r, _startSliderColor.r, t);
        var newGreen =  Mathf.Lerp(_endSliderColor.g,_startSliderColor.g, t);
        var newBlue =  Mathf.Lerp(_endSliderColor.b, _startSliderColor.b, t);
        reloadCursor.fillRect.GetComponent<Image>().color = new Color(newRed, newGreen, newBlue, 1);
    }
    
    void OnDestroy()
    {
        reloadCursor.onValueChanged.RemoveAllListeners();
    }
}
