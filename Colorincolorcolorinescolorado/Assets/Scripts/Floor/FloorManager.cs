using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class FloorManager : MonoBehaviour
{
    public GameObject sueloNormal;
    public GameObject sueloCalamar;
    public GameObject sueloTiburon;
    public GameObject sueloRojo;
    public GameObject sueloNegro;
    public Transform posicionInicial; // Posición donde se empiezan a crear las filas
    private Vector3 posicion; // Posición donde crear la siguiente fila
    public Movimiento_jugador jugador; // La referencia al script del jugador

    private float ultimaFila = 0f;  // La posición de la ultima fila creada
    public int filasIniciales = 10;
    private bool ultimaEsRoja = false;
    private bool ultimaEsNegra = false;
    private int currentSafeIndex = -1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        posicion = posicionInicial.position; // Posición donde las filas empiezan a crearse
        ultimaFila = posicion.z; // La última fila es la posición inicial
        for (int i = 0;  i < filasIniciales; i++) // Al inicio se crearán 10 filas
        {
            NextSuelo(sueloNormal, false);
        }
    }

    void Update()
    {
        if (jugador.posicionMax >= ultimaFila - filasIniciales / 2) GenerateMoreSuelo();
        }

    void NextSuelo(GameObject tipoSuelo, bool tieneObstaculo)
    {
        var nuevafila = Instantiate(tipoSuelo, posicion, Quaternion.identity, transform);
        if (tipoSuelo == sueloNormal)
        {
            var fila = nuevafila.GetComponent<Fila>();
            int incoming = currentSafeIndex;
            if (incoming < 0)
            {
                // try to find the most recent fila with a valid safeIndex so new row can align
                for (int ci = transform.childCount - 1; ci >= 0; ci--)
                {
                    var prev = transform.GetChild(ci).GetComponent<Fila>();
                    if (prev != null && prev.safeIndex >= 0)
                    {
                        incoming = prev.safeIndex;
                        break;
                    }
                }
            }
            StartCoroutine(SetupFila(fila, incoming, tieneObstaculo));
        }
        posicion.z += 1;
        ultimaFila = posicion.z;
    }

    IEnumerator SetupFila(Fila fila, int incomingSafeIndex, bool tieneObstaculo)
    {
        yield return new WaitUntil(() => fila.casillas != null && fila.casillas.Count > 0);
        int count = fila.casillas.Count;
        int chosen;
        if (incomingSafeIndex >= 0 && incomingSafeIndex < count)
        {
            chosen = incomingSafeIndex;
        }
        else
        {
            if (jugador != null)
            {
                float px = jugador.transform.position.x;
                float bestDist = float.MaxValue;
                int bestIdx = 0;
                for (int i = 0; i < count; i++)
                {
                    float dx = Mathf.Abs(fila.casillas[i].getPosition().x - px);
                    if (dx < bestDist)
                    {
                        bestDist = dx;
                        bestIdx = i;
                    }
                }
                chosen = bestIdx;
            }
            else
            {
                chosen = count / 2; 
            }
        }
            fila.safeIndex = chosen;
        fila.tieneObstaculo = tieneObstaculo;
        if (tieneObstaculo) fila.generarObstaculos();
        currentSafeIndex = Mathf.Clamp(chosen + Random.Range(-1, 2), 0, count - 1);
    }

    void GenerateMoreSuelo()
    {
        int probability = Random.Range(0, 100); // Crea un número aleatorio del 0 al 100
        if (probability <= 20) // Si el número es menor o igual a 30
        {
            NextSuelo(sueloNormal, true); // Creará un suelo normal
            ultimaEsRoja = false;
            ultimaEsNegra = false;
        }
        else if (probability <= 40)
        {
            NextSuelo(sueloTiburon, false); // Creará un suelo con tiburones
            ultimaEsRoja = false;
            ultimaEsNegra = false;
        }
        else if (probability <= 60)
        {
            NextSuelo(sueloCalamar, false); // Creará un suelo con calamares
            ultimaEsRoja = false;
            ultimaEsNegra = false;
        }
        else if (!ultimaEsRoja && probability <= 80)
        {
            NextSuelo(sueloNegro, false);
            ultimaEsNegra = true;
            ultimaEsRoja = false;
        }
        else if (!ultimaEsNegra && probability <= 100)
        {
            NextSuelo(sueloRojo, false);
            ultimaEsRoja = true;
            ultimaEsNegra = false;
        }
    }
    
    void EliminarSuelo()
    {
       
    }
}
