using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundScale : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
        
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        float _scaleX, _scaleY;
        float _backgroundImageHeight = _spriteRenderer.bounds.size.y;
        float _backgroundImageWidth = _spriteRenderer.bounds.size.x;
        float _screenWidth = Screen.width;
        float _screenHeight = Screen.height;

        _scaleY = Camera.main.orthographicSize * 2 / _backgroundImageHeight;
        _scaleX = (_screenWidth / _screenHeight) * _scaleY * (_backgroundImageHeight / _backgroundImageWidth);
        
        Vector3 Scale = transform.localScale;
        Scale.x = _scaleX;
        Scale.y = _scaleY;
        transform.localScale = Scale;

        
    }

}
