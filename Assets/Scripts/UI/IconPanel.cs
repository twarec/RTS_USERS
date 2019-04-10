using System;
using RTS;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("RTS/UI/IconPanel")]
public class IconPanel : MonoBehaviour
{
    public Sprite DefaultTexture;

    [SerializeField]
    private Image icon;

    private Sprite _texture;
    public Sprite Texture
    {
        get => _texture;
        set
        {
            _texture = value;
            icon.sprite = value == null ? DefaultTexture : _texture;
        }
    }

    private void Awake()
    {
        Texture = null;
        SelectionView.SelectAction += SelectIcon;
    }

    private void SelectIcon(System.Collections.Generic.List<ISelectebleObject> objs)
    {
        if(objs.Count != 0)
        {
            Texture = objs[0].Icon;
        }
        else
        {
            Texture = null;
        }
    }
}

