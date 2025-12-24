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
        if (jugador.posicionMax >= ultimaFila - filasIniciales) // Si la posicion maxima del jugador es mayor o igual que la ultima fila - las iniciales
        {
            GenerateMoreSuelo(); // Se llama a la funcion de generar mas suelo
        }
        EliminarSuelo(); // Sino se llama a la funcion de eliminar suelo
    }

    void NextSuelo(GameObject tipoSuelo, bool tieneObstaculo)
    {
        var nuevafila = Instantiate(tipoSuelo, posicion, Quaternion.identity, transform); // Se inicializa una nueva fila con estos parametros
        if (tipoSuelo == sueloNormal) // Si la fila es una fila normal
        {
            var fila = nuevafila.GetComponent<Fila>(); // Se guarda la fila creada
            int siguiente = currentSafeIndex; 
            if (siguiente < 0) // Si siguiente tiene un valor menor a 0
            {
                siguiente = ObtenerSafeIndexAnterior(); // Sigueinte toma el valor del indice seguro actual
            }
            StartCoroutine(SetupFila(fila, siguiente, tieneObstaculo));
        }
        posicion.z += 1; // La posicionz se sume en uno porque cada fila ocupa 1 z
        ultimaFila = posicion.z; // La posicion de la ultima fila es la de la posicion z
    }
    int ObtenerSafeIndexAnterior()
    {
        return currentSafeIndex; // Devuelve el indice seguro actual
    }

    IEnumerator SetupFila(Fila fila, int siguienteSafeIndex, bool tieneObstaculo)
    {
        while (fila.casillas== null || fila.casillas.Count == 0)
        {
            yield return null;
        }
        int cantidad = fila.casillas.Count; // La cantidad es la cantidad de casillas de las filas
        int elegida;
        if (siguienteSafeIndex >= 0 && siguienteSafeIndex < cantidad) // Si el indice seguro es mayor que 0 y menor a la cantidad de casillas
        {
            elegida = siguienteSafeIndex; // Casilla elegida para tener el indice seguro
        }
        else // Si no podemos usar ese indice seguro
        { 
            float posicionX = jugador.transform.position.x; // la posicionX es la posicion x del jugador
            float mejorDistancia = float.MaxValue; // Hacer que la mejorDistancia sea un un valor muy ato
            int mejorIndice = 0;
            for (int i = 0; i < cantidad; i++) // Se elegira la casilla mas cercana al jugador como casilla segura
            {
                float distanciaX = Mathf.Abs(fila.casillas[i].getPosition().x - posicionX);
                if (distanciaX < mejorDistancia)
                {
                    mejorDistancia = distanciaX;
                    mejorIndice = i;
                }
            }
            elegida = mejorIndice; //El indice seguro es el mejor indice
        }
        fila.safeIndex = elegida; // Asignar el indice seguro a la fila
        fila.tieneObstaculo = tieneObstaculo; // La fila tendra obstaculo
        if (tieneObstaculo) // Si tiene obstaculo
        {
            fila.generarObstaculos(); // Se genera osbtaculo
        }
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
        for (int i = transform.childCount - 1; i >= 0; i--) // For de todas las filas de más a menos para que no se alteren los indices al borrar
        {
            Transform filaTransform = transform.GetChild(i); // Se coge el transform de cada fila
            float distanciaJugador = Mathf.Abs(filaTransform.position.z - jugador.transform.position.z); // Calcula la distancia entre fila y jugador

            if (distanciaJugador > 20) // Si la distancia es mayor de 20 casillas
            {
                Destroy(filaTransform.gameObject); // Se destruye la fila
            }
        }
    }
}
