using UnityEngine;
using UnityEngine.UI;

public class GraveController : MonoBehaviour
{
    public GameObject portal;
    public float maxProgressTime;
    
    private Slider _progressSlider;
    private Color _startSliderColor = new Color(0f / 255f, 255f / 255f, 72f / 255f);
    private Color _endSliderColor = new Color(255f / 255f, 20f / 255f, 0f / 255f);
    
    void Start()
    {
        Slider[] sliders = GetComponentsInChildren<Slider>();
        if (sliders.Length > 0)
        {
            _progressSlider = sliders[sliders.Length - 1];
        }
        
        _progressSlider.onValueChanged.AddListener(OnSliderChanged);
        _progressSlider.maxValue = maxProgressTime;
        _progressSlider.value = 0;
    }
    
    void Update()
    {
        _progressSlider.value += Time.deltaTime;

        if (_progressSlider.value >= maxProgressTime)   
        {
            var id = gameObject.GetComponent<MonsterController>().monsterID;
            var monster = Instantiate(portal.gameObject.GetComponent<PortalController>().monsters[id], transform.position, Quaternion.Euler(0f, 0f, 0f));
            monster.gameObject.GetComponent<MonsterController>().SetIsReviveMonster(true);
            Destroy(gameObject);
        }
    }
    
    void OnSliderChanged(float value)
    {
        var t = value / _progressSlider.maxValue;
        var newRed =  Mathf.Lerp(_endSliderColor.r, _startSliderColor.r, t);
        var newGreen =  Mathf.Lerp(_endSliderColor.g,_startSliderColor.g, t);
        var newBlue =  Mathf.Lerp(_endSliderColor.b, _startSliderColor.b, t);
        _progressSlider.fillRect.GetComponent<Image>().color = new Color(newRed, newGreen, newBlue, 1);
    }
}
