using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


public class cambio : MonoBehaviour
{
    private Renderer rendererObject;


    public Color defaultColor;

    public Color newColor;

    public bool colorChanged = false;
    void Start()
    {
        rendererObject = GetComponent<Renderer>();

        rendererObject.material.color = defaultColor;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!colorChanged)
            {
                ChangeColorMethod(newColor);

            }
            else
            {
                ChangeColorMethod(defaultColor);
            }


        }
    }

    public void ChangeColorMethod(Color colorToChange)
    {
        colorChanged = !colorChanged;
        rendererObject.material.color = colorToChange;
    }
}

