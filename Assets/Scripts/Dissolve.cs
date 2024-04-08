using System.Collections;
using UnityEngine;

public class Dissolve : MonoBehaviour
{
    [SerializeField] private float _dissolveTime = 0.75f;

    public GameObject _gameObject;

    private SpriteRenderer[] _spriteRenderers;
    private Material[] _materials;
    private bool isVanished;

    private int _dissolveAmount = Shader.PropertyToID("_DissolveAmount");

    void Start()
    {
        isVanished = false;
        _spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        _materials = new Material[_spriteRenderers.Length];
        for (int i = 0; i < _spriteRenderers.Length; i++)
        {
            _materials[i] = _spriteRenderers[i].material;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if(isVanished == false)
            {
                StartCoroutine(Vanish());
                isVanished = true;
            }
            else
            {
                StartCoroutine(Appear());
                isVanished = false;
            } 
        }
    }

    private IEnumerator Vanish()
    {
        float elapsedTime = 0f;
        while (elapsedTime < _dissolveTime)
        {
            elapsedTime += Time.deltaTime;

            float lerpDissolve = Mathf.Lerp(0, 1.1f, (elapsedTime / _dissolveTime));

            for (int i = 0; i < _materials.Length; i++)
            {
                _materials[i].SetFloat(_dissolveAmount, lerpDissolve);
            }

            yield return null;
        }
    }

    private IEnumerator Appear()
    {
        float elapsedTime = 0f;
        while (elapsedTime < _dissolveTime)
        {
            elapsedTime += Time.deltaTime;

            float lerpDissolve = Mathf.Lerp(1.1f, 0f, (elapsedTime / _dissolveTime));

            for (int i = 0; i < _materials.Length; i++)
            {
                _materials[i].SetFloat(_dissolveAmount, lerpDissolve);
            }

            yield return null;
        }
    }
}
