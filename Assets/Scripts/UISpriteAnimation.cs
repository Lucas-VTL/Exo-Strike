using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISpriteAnimation : MonoBehaviour
{

    public Image image;

    public Sprite[] spritesArray;
    private float _speed = .02f;
    private int _indexSprite;
    private Coroutine _corotineAnim;
    private bool _isDone;

    void Update()
    {
        _isDone = false;
        StartCoroutine(Func_PlayAnimUI());
    }   
    
    IEnumerator Func_PlayAnimUI()
    {
        yield return new WaitForSeconds(_speed);
        if (_indexSprite >= spritesArray.Length)
        {
            _indexSprite = 0;
        }
        image.sprite = spritesArray[_indexSprite];
        _indexSprite += 1;
        if (_isDone == false)
            _corotineAnim = StartCoroutine(Func_PlayAnimUI());
    }
}