using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OverlapBox : MonoBehaviour
{
    public LayerMask m_layerMask;

    void FixeUpdate()
    {
        Colisiones();
    }
    
    void Colisiones()
    {
        Collider[] hitColliders = Physics.OverlapBox(gameObject.transform.position, transform.localScale / 2, Quaternion.identity, m_layerMask); // Se guarda los colliders de alrededor
        int i = 0;

        while (i < hitColliders.Length) // Recorre todos los colliders detectados
        {
            i++;
        }
    }

    void OnDrawGizmos() // Crea un cuadrado rojo para saber la posicion del jellyfish
    {
        Gizmos.color = Color.red;
        if (Application.isPlaying)
        {
            Gizmos.DrawWireCube(transform.position, transform.localScale);
        }
    }

}
