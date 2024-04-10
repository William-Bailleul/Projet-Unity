using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorDetection : MonoBehaviour
{

    public string selectedColor = "none";
    GameObject[] blueObjects;
    GameObject[] greenObjects;
    GameObject[] redObjects;



    void ColorSwitch()
    {

        if (selectedColor == "blue")
        {
            foreach (GameObject obj in blueObjects)
            {
                obj.SetActive(true);
            }

        }
        else
        {
            foreach (GameObject obj in blueObjects)
            {
                obj.SetActive(false);
            }
        }

        if (selectedColor == "green")
        {
            foreach (GameObject obj in greenObjects)
            {
                obj.SetActive(true);
            }

        }
        else
        {
            foreach (GameObject obj in greenObjects)
            {
                obj.SetActive(false);
            }
        }

        if (selectedColor == "red")
        {
            foreach (GameObject obj in redObjects)
            {
                obj.SetActive(true);
            }

        }
        else
        {
            foreach (GameObject obj in redObjects)
            {
                obj.SetActive(false);
            }
        }
    }

    void KeyDetect()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            selectedColor = "red";
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            selectedColor = "green";
        }
        if (Input.GetKeyDown(KeyCode.B)) {
            selectedColor = "blue";
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        blueObjects = GameObject.FindGameObjectsWithTag("Blue");
        greenObjects = GameObject.FindGameObjectsWithTag("Green");
        redObjects = GameObject.FindGameObjectsWithTag("Red");
    }

    // Update is called once per frame
    void Update()
    {

        
        KeyDetect();
        ColorSwitch();

    }
}
