using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Colorin : MonoBehaviour
{
    public GameObject casilla;
    public GameObject player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider player)
    {
        if (!player.CompareTag("Player"))
        {
            return;
        }

        cambio playerColor = player.GetComponent<cambio>();

        if (player.CompareTag("Player"))
        {
            if (playerColor.colorChanged == false)
            {
                Debug.Log("es negro");
            }
            else if (playerColor.colorChanged == true)
            {
                Debug.Log("es rojo");
            }
        }
    }
}



