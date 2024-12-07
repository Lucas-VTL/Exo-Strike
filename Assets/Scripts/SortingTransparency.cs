using UnityEngine;
using UnityEngine.Tilemaps;

public class SortingTransparency : MonoBehaviour
{
    public GameObject player;
    public Tilemap tilemap;
    private SpriteRenderer _playerRenderer;
    private SpriteRenderer[] _tileRenderers;
    
    private void Start()
    {
        _playerRenderer = player.GetComponent<SpriteRenderer>();
        _tileRenderers = tilemap.GetComponentsInChildren<SpriteRenderer>();
    }
    
    private void Update()
    {
        var playerYPos = player.transform.position.y;
        _playerRenderer.sortingOrder = Mathf.FloorToInt(playerYPos * 100);

        foreach (SpriteRenderer tile in _tileRenderers)
        {
            float tileYPos = tile.transform.position.y;
            tile.sortingOrder = Mathf.FloorToInt(tileYPos * 100);
        }
    }
}
