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
     
    }

    // Update is called once per frame
    void Update()
    {
        generarObstaculos(this);
    }

    public void generarObstaculos(Fila fila)
    {
        if (tieneObstaculo)
        {
            if (maxObstaculos <= 0)
            {
                return;
            }
            int cont = 0;
            int maxContObstaculos = 2;
            for (int i = 0; i < casillas.Count; i++)
            {
                bool[] lastFila = new bool[casillas.Count];
                for (int j=0; j < lastFila.Length; j++)
                {
                    lastFila[j] = fila.casillas[i];
                    if (lastFila[j] == tieneObstaculo)
                    {
                        maxContObstaculos--;
                        if (maxContObstaculos == 0)
                        {
                            fila.casillas[i].tieneObstaculo = true;
                        }
                        cont++;
                    }
                }

                if (maxContObstaculos > 0 && Random.Range(0, 2) == 1)
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
}
