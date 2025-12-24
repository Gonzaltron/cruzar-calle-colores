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
        if (!player.CompareTag("Player")) // Si toca aglo que no tenga el tag player
        {
            return;
        }

        cambio playerColor = player.GetComponent<cambio>(); // Obtener el componente del color del jugador

        if (player.CompareTag("Player")) // Si toca algo que tenga el tag player
        {
            { 
                player.gameObject.GetComponent<Muerte>(); // Se muere
            }
        }
    }
}



