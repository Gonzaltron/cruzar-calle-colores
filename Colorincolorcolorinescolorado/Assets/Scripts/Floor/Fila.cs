using UnityEngine;
using System.Collections.Generic;
public class Fila : MonoBehaviour
{
    public List<Casilla> casillas = new List<Casilla>();
    public bool tieneObstaculo = false;
    public int maxObstaculos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (tieneObstaculo)
        {
            int cont = 0;
            for (int i = 0; i < casillas.Count; i++)
            {
                if (Random.Range(0,2) == 1)
                {
                    casillas[i].ActivarObstaculo();
                    cont++;
                    if (cont == maxObstaculos)
                    {
                        break;
                    }
                }
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
