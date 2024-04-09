using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dissolve : MonoBehaviour
{
    [SerializeField] private float _dissolveTime = 0.75f;
    private int _dissolveAmount = Shader.PropertyToID("_DissolveAmount");

    private bool isVanished;

    private MaterialPropertyBlock _propertyBlock;

    private void Start()
    {
        ColorManager.OnChangeColor += OnColorChange;
        _propertyBlock = new MaterialPropertyBlock();
    }

    public void OnColorChange(ColorManager.Colors newColor)
    {

        if (this != null && gameObject != null && gameObject.activeSelf)
        {
            if (isVanished)
            {
                StartCoroutine(Appear());
            }
            else
            {
                StartCoroutine(Vanish());
            }
        }

    }


    public IEnumerator Vanish()
    {
        isVanished = true;
        float elapsedTime = 0f;
        while (elapsedTime < _dissolveTime)
        {
            elapsedTime += Time.deltaTime;

            float lerpDissolve = Mathf.Lerp(0, 1.1f, (elapsedTime / _dissolveTime));
            SetDissolveAmount(lerpDissolve, ColorManager.GetPreviousColor());

            yield return null;
        }
    }

    public IEnumerator Appear()
    {
        isVanished = false;
        float elapsedTime = 0f;
        while (elapsedTime < _dissolveTime)
        {
            elapsedTime += Time.deltaTime;

            float lerpDissolve = Mathf.Lerp(1.1f, 0f, (elapsedTime / _dissolveTime));
            SetDissolveAmount(lerpDissolve, ColorManager.GetActiveColor());

            yield return null;
        }
    }

    public void SetDissolveAmount(float amount, ColorManager.Colors color)
    {
        GameObject[] objectsToDissolve = GetObjectsWithColor(color);
        foreach (GameObject obj in objectsToDissolve)
        {
            SpriteRenderer[] renderers = obj.GetComponentsInChildren<SpriteRenderer>();
            foreach (SpriteRenderer renderer in renderers)
            {
                renderer.material.SetFloat(_dissolveAmount, amount);
            }
        }
    }


    private GameObject[] GetObjectsWithColor(ColorManager.Colors color)
    {
        ColorEvent[] colorEvents = FindObjectsOfType<ColorEvent>();

        // Filtrer les objets par couleur
        List<GameObject> objectsWithColor = new List<GameObject>();
        foreach (ColorEvent colorEvent in colorEvents)
        {
            if (colorEvent.Color == color && colorEvent.gameObject.activeSelf)
            {
                objectsWithColor.Add(colorEvent.gameObject);
            }
        }

        return objectsWithColor.ToArray();
    }

    public bool IsDestroyed()
    {
        return this == null;
    }
}
