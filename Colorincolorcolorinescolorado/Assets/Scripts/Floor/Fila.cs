using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Fila : MonoBehaviour
{
    public List<Casilla> casillas = new List<Casilla>();
    public bool tieneObstaculo = false;
    public int maxObstaculos;
    private bool obstaculosGenerados = false;
    public int safeIndex = -1;
    
  
    public IEnumerator InitializeAndGenerate(int safeIndex, bool tieneObstaculo)
    {
        this.safeIndex = safeIndex;
        this.tieneObstaculo = tieneObstaculo;

        while (casillas == null || casillas.Count == 0)
        {
            yield return null;
        }
    }

    public void generarObstaculos()
    {
        List<int> posiblesObstaculos = new List<int>(casillas.Count);
        int obstaculosPermitidos = Mathf.Clamp(maxObstaculos, 0, Mathf.Max(0, casillas.Count - 1));

        if (obstaculosGenerados)
        {
            return;
        }
        obstaculosGenerados = true;

        if (!tieneObstaculo)
        {
            return;
        }
        if (casillas == null || casillas.Count == 0)
        {
            return;
        }

        if (obstaculosPermitidos <= 0)
        {
            return;
        }

        for (int i = 0; i < casillas.Count; i++)
        {
            if (i == safeIndex)
            {
                continue;
            }
            posiblesObstaculos.Add(i);
        }

        for (int i = posiblesObstaculos.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            int tmp = posiblesObstaculos[i];
            posiblesObstaculos[i] = posiblesObstaculos[j];
            posiblesObstaculos[j] = tmp;
        }

        int maxAvailable = Mathf.Min(obstaculosPermitidos, posiblesObstaculos.Count);
        int numObstaculos = Random.Range(0, maxAvailable + 1); 
        for (int i = 0; i < numObstaculos; i++)
        {
            casillas[posiblesObstaculos[i]].ActivarObstaculo();
        }
    }
}
