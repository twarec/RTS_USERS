using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image))]
public class SkilsPanaleEllement : MonoBehaviour, IPointerClickHandler
{
    private bool _isEnable;
    private Color _baseColor;

    public bool IsEnable
    {
        get => _isEnable;
        set
        {
            _isEnable = value;
            if (!_isEnable)
            {
                _image.color = _baseColor * .5f;
            }
            else
            {
                _image.color = _baseColor * 2;
            }
        }
    }


    private Image _image;


    public System.Action Action;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _baseColor = _image.color;


        IsEnable = true;
    }



    public Sprite Icon
    {
        get => _image.sprite;
        set => _image.sprite = value;
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        if (_isEnable)
        {
            Action?.Invoke();
        }
    }
}
