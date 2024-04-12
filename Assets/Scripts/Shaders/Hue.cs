using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Hue : MonoBehaviour
{
    private int _hueValue = Shader.PropertyToID("_Hue");

    public void ChangeHue(ColorManager.Colors newColor)
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if(this != null)
        {
            switch(newColor)
            {
                case ColorManager.Colors.None:
                    spriteRenderer.material.SetFloat(_hueValue, 0);
                    break;
                case ColorManager.Colors.Red:
                    spriteRenderer.material.SetFloat(_hueValue, 198);
                    break;
                case ColorManager.Colors.Yellow:
                    spriteRenderer.material.SetFloat(_hueValue, 250);
                    break;
                case ColorManager.Colors.Blue:
                    spriteRenderer.material.SetFloat(_hueValue, 60);
                    break;
            }
        }
    }
}
