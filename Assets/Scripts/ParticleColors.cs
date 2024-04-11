using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class ParticleColors : MonoBehaviour
{
    public ParticleSystem.MainModule _particle;
    public ColorDetection _colorDetection;
    // Start is called before the first frame update
    void Start()
    {
        _particle = GetComponent<ParticleSystem>().main;
    }

    // Update is called once per frame
    void Update()
    {
        if (_colorDetection.selectedColor == "blue")
        {
            _particle.startColor = new Color(0.4f, 1f, 1f);
        }
        if (_colorDetection.selectedColor == "red")
        {
            _particle.startColor = new Color(1f, 0.3349057f, 0.3554974f);
        }
        if (_colorDetection.selectedColor == "green")
        {
            _particle.startColor = new Color(1f, 1f, 0.4009434f);
        }
    }
}
