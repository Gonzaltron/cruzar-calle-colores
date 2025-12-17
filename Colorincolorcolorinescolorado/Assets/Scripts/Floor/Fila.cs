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

        yield return new WaitUntil(() => casillas != null && casillas.Count > 0);

    }

    void Start()
    {
    }

    void Update()
    {
    }

    public void generarObstaculos()
    {
        List<int> candidates = new List<int>(casillas.Count);
        int allowedObstaculos = Mathf.Clamp(maxObstaculos, 0, Mathf.Max(0, casillas.Count - 1));

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

        if (allowedObstaculos <= 0)
        {
            return;
        }

        for (int i = 0; i < casillas.Count; i++)
        {
            if (i == safeIndex)
            {
                continue;
            }
            candidates.Add(i);
        }

        for (int i = candidates.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            int tmp = candidates[i];
            candidates[i] = candidates[j];
            candidates[j] = tmp;
        }

        int maxAvailable = Mathf.Min(allowedObstaculos, candidates.Count);
        int numObstaculos = Random.Range(0, maxAvailable + 1); 
        for (int k = 0; k < numObstaculos; k++)
        {
            casillas[candidates[k]].ActivarObstaculo();
        }
    }
}
