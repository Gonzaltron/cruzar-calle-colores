using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Colorin : MonoBehaviour
{
    public GameObject casilla;
    public GameObject player;

    void OnTriggerEnter(Collider player)
    {
        if (!player.CompareTag("Player"))
        {
            return;
        }

        cambio playerColor = player.GetComponent<cambio>();

        if (player.CompareTag("Player"))
        {
             if (player.gameObject.GetComponent<cambio>().colorChanged == false)
             {
                player.gameObject.GetComponent<Muerte>();
             }
        }
    }
}



