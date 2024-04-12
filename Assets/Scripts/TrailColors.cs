using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class TrailColors : MonoBehaviour
{
    private TrailRenderer _trailRenderer;
    public ColorDetection _colorDetection;
    // Start is called before the first frame update
    void Start()
    {
        _trailRenderer = GetComponent<TrailRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_colorDetection.selectedColor == "blue")
        {
            _trailRenderer.startColor = new Color(0.4f, 1f, 1f);
        }
        if (_colorDetection.selectedColor == "red")
        {
            _trailRenderer.startColor = new Color(1f, 0.3349057f, 0.3554974f);
        }
        if (_colorDetection.selectedColor == "green")
        {
            _trailRenderer.startColor = new Color(1f, 1f, 0.4009434f);
        }
    }
}
