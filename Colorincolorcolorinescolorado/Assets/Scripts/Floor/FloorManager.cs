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
        if (jugador.posicionMax >= ultimaFila - filasIniciales/2) // Si el jugador está a 5 casillas de la última fila 
        {
            GenerateMoreSuelo(); // Se crean más filas 
        }
        else if (jugador.posicionMax >= ultimaFila + filasIniciales)
        {
            EliminarSuelo(); 
        }
    }

    void NextSuelo(GameObject tipoSuelo, bool tieneObstaculo)
    {
        GameObject nuevafila = Instantiate(tipoSuelo, posicion, Quaternion.identity, transform); // Se crea una fila de x tipo, el quaternion.identity es porque hay que tenerlo
        if (tipoSuelo == sueloNormal)
        {
            Fila scriptFila = nuevafila.GetComponent<Fila>();
            scriptFila.tieneObstaculo = tieneObstaculo;
        }
      
        posicion.z += 1; // La posición z aumenta en 1 para tener la posición de la siguiente fila
        ultimaFila = posicion.z; // Actualiza la posición de la ultima fila 
    }

    void GenerateMoreSuelo()
    {
        int probability = Random.Range(0, 100); // Crea un número aleatorio del 0 al 100
        if (probability <= 20) // Si el número es menor o igual a 30
        {
            NextSuelo(sueloNormal, true); // Creará un suelo normal
        }
        else if (probability <= 40)
        {
            NextSuelo(sueloTiburon, false); // Creará un suelo con tiburones
        }
        else if (probability <= 60)
        {
            NextSuelo(sueloCalamar, false); // Creará un suelo con calamares
        }
        else if (probability <= 80)
        {
            NextSuelo(sueloNegro, false);
        }
        else
        {
            NextSuelo(sueloRojo, false);
        }
    }
    
    void EliminarSuelo()
    {
       
    }
}
