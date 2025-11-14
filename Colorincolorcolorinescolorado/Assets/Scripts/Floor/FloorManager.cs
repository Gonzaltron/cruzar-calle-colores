using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class FloorManager : MonoBehaviour
{
    public GameObject sueloNormal;
    public GameObject sueloCalamar;
    public GameObject sueloTiburon;
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
            NextSuelo(sueloNormal);
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
   
    void NextSuelo(GameObject tipoSuelo)
    {
        Instantiate(tipoSuelo, posicion, Quaternion.identity, transform); // Se crea una fila de x tipo, el quaternion.identity es porque hay que tenerlo
        posicion.z += 1; // La posición z aumenta en 1 para tener la posición de la siguiente fila
        ultimaFila = posicion.z; // Actualiza la posición de la ultima fila 
    }

    void GenerateMoreSuelo()
    {
        int probability = Random.Range(0, 100); // Crea un número aleatorio del 0 al 100
        if (probability <= 30) // Si el número es menor o igual a 30
        {
            NextSuelo(sueloNormal); // Creará un suelo normal
        }
        else if (probability <= 65)
        {
            NextSuelo(sueloTiburon); // Creará un suelo con tiburones
        }
        else
        {
            NextSuelo(sueloCalamar); // Creará un suelo con calamares
        }
    }
    
    void EliminarSuelo()
    {
       
    }
}
