using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class FloorManager : MonoBehaviour
{
    public GameObject sueloNormal;
    public GameObject sueloCalamar;
    public GameObject sueloTiburon;
    public GameObject sueloTiburon2;
    public GameObject sueloTiburon3;
    public GameObject sueloRojo;
    public GameObject sueloNegro;
    public Transform posicionInicial; // Posici�n donde se empiezan a crear las filas
    private Vector3 posicion; // Posici�n donde crear la siguiente fila
    public Movimiento_jugador jugador; // La referencia al script del jugador

    private float ultimaFila = 0f;  // La posici�n de la ultima fila creada
    public int filasIniciales = 30;
    private bool ultimaEsRoja = false;
    private bool ultimaEsNegra = false;
    private int currentSafeIndex = -1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        posicion = posicionInicial.position; // Posici�n donde las filas empiezan a crearse
        ultimaFila = posicion.z; // La �ltima fila es la posici�n inicial
        for (int i = 0;  i < filasIniciales; i++) // Al inicio se crear�n 10 filas
        {
            NextSuelo(sueloNormal, false);
        }
    }

    void Update()
    {
        if (jugador.posicionMax >= ultimaFila - filasIniciales)
        {
            GenerateMoreSuelo();
        }
        EliminarSuelo();
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
        int probability = Random.Range(0, 100); // Crea un n�mero aleatorio del 0 al 100
        if (probability <= 30) // Si el n�mero es menor o igual a 30
        {
            NextSuelo(sueloNormal, true); // Crear� un suelo normal
            ultimaEsRoja = false;
            ultimaEsNegra = false;
        }
        else if (probability <= 37)
        {
            NextSuelo(sueloTiburon, false); // Crear� un suelo con tiburones
            ultimaEsRoja = false;
            ultimaEsNegra = false;
        }
        else if (probability <= 43)
        {
            NextSuelo(sueloTiburon2, false); // Crear� un suelo con tiburones
            ultimaEsRoja = false;
            ultimaEsNegra = false;
        }
        else if (probability <= 50)
        {
            NextSuelo(sueloTiburon3, false); // Crear� un suelo con tiburones
            ultimaEsRoja = false;
            ultimaEsNegra = false;
        }
        else if (probability <= 65)
        {
            NextSuelo(sueloCalamar, false); // Crear� un suelo con calamares
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
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            Transform filaTransform = transform.GetChild(i);
            float distanciaJugador = Mathf.Abs(filaTransform.position.z - jugador.transform.position.z);

            if (distanciaJugador > 20)
            {
                Destroy(filaTransform.gameObject);
            }
        }
    }
}
