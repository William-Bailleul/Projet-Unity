using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(SpriteRenderer))]
public class ColorEvent : MonoBehaviour
{
    public ColorManager.Colors Color;
    private bool _active = true;
    private Collider2D _collider;
    private SpriteRenderer _sprite;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
        _sprite = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        onColorChange(ColorManager.ActiveColor);
        ColorManager.OnChangeColor += onColorChange;
    }

    private void OnDestroy()
    {
        ColorManager.OnChangeColor -= onColorChange;
    }

    void onColorChange(ColorManager.Colors newColor) {
        if (_active)
        {
            if (Color == newColor) return;
            else
            {
                Deactivate();
            }
        } else
        {
            if (Color != newColor) return;
            Activate();
        }
    }

    private void Activate()
    {
        _active = true;
        _collider.enabled = true;
        _sprite.enabled = true;
    }

    private void Deactivate()
    {
        _active = false;
        _collider.enabled = false;
        _sprite.enabled = false;
    }
}
