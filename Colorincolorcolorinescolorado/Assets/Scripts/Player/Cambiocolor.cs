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

        rendererObject.material.color = defaultColor;   //Se inicializa el color al color principal
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))        //Seleccionar la tecla E para cambiar el color
        {
            if (!colorChanged)      
            {
                ChangeColorMethod(newColor);        //Cambiar el color inicial al segundo color si el inicial est� puesto

            }
            else
            {
                ChangeColorMethod(defaultColor);    //Si el color inicial no est� seleccionado entonces significa que est� el secundario puesto por lo tanto se cambia al inicial 
            }
        }
    }

 
    public void ChangeColorMethod(Color colorToChange)
    {
        colorChanged = !colorChanged;
        rendererObject.material.color = colorToChange;
    }
}

