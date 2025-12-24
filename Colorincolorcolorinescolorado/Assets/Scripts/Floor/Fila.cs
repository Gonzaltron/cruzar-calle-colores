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
    
  
    public IEnumerator Inicializar(int safeIndex, bool tieneObstaculo)
    {
        this.safeIndex = safeIndex; // Inciialiar el safeINdex
        this.tieneObstaculo = tieneObstaculo; // Inicializar el bool t ieneObstaculo

        while (casillas == null || casillas.Count == 0) // Mientras las casillas sean null ono haya
        {
            yield return null;
        }
    }

    public void generarObstaculos()
    {
        List<int> posiblesObstaculos = new List<int>(casillas.Count); // Lista de los obstaculos, hay uno por cada casilla
        int obstaculosPermitidos = Mathf.Clamp(maxObstaculos, 0, Mathf.Max(0, casillas.Count - 1)); // Limita la cantidad de obstaculos que se permiten

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
            posiblesObstaculos.Add(i); // Añadir a la lista de posibles obstaculos
        }

        for (int i = posiblesObstaculos.Count - 1; i > 0; i--) // for para hacer que los posibles obstaculos que se vayan a generar sean aleatorios
        {
            int j = Random.Range(0, i + 1);
            int tmp = posiblesObstaculos[i];
            posiblesObstaculos[i] = posiblesObstaculos[j];
            posiblesObstaculos[j] = tmp;
        }

        int maxAvailable = Mathf.Min(obstaculosPermitidos, posiblesObstaculos.Count); // Pone un rango de la cantidad de obstaculos que se pueden generar
        int numObstaculos = Random.Range(0, maxAvailable + 1);  //Numeor aleatorio 
        for (int i = 0; i < numObstaculos; i++)
        {
            casillas[posiblesObstaculos[i]].ActivarObstaculo(); // Se generan dependiendo de la cantidad de obstaculos que se haya indicado
        }
    }
}
