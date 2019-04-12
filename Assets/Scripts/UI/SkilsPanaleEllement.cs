using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image))]
public class SkilsPanaleEllement : MonoBehaviour, IPointerClickHandler
{
    private Image _image;


    public System.Action Action;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }



    public Sprite Icon
    {
        get => _image.sprite;
        set => _image.sprite = value;
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        Action?.Invoke();
    }
}
