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
    private Dissolve _dissolve;
    private ColorManager.Colors _previousColor;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
        _sprite = GetComponent<SpriteRenderer>();
        _dissolve = GetComponent<Dissolve>();
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
                if(_dissolve != null)
                {
                    StartCoroutine(FadeOutAndDisable());
                }
            }
        }
        else
        {
            if (Color != newColor) return;
            Activate();
        }
            //bool shouldBeActive = (Color == newColor);

            //if (_dissolve != null && _dissolve.gameObject.activeSelf)
            //{
            //    _dissolve.OnColorChange(newColor);
            //}

            //if (shouldBeActive)
            //{
            //    Activate();
            //}
            //else
            //{
            //    if (_dissolve != null && Color == ColorManager.PreviousColor)
            //    {
            //        StartCoroutine(FadeOutAndDisable());
            //    }
            //}

            //_previousColor = ColorManager.PreviousColor;

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

    private IEnumerator FadeOutAndDisable()
    {
        if (_dissolve != null)
        {
            yield return StartCoroutine(_dissolve.Vanish());
        }
        Deactivate();
    }

    private void onDestroy()
    {
        if (_dissolve != null)
        {
            Destroy(_dissolve.gameObject);
        }
    }
}
