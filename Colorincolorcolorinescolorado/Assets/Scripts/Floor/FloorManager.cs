using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class FloorManager : MonoBehaviour
{
    public GameObject sueloNormal;
    public GameObject sueloCalamar;
    public GameObject sueloTiburon;
    public Transform posicionInicial;
    private Vector3 posicion;

    void Awake()
    {

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0;  i < 10; i++) // Al inicio se crearán 10 filas
        {
            Instantiate(sueloNormal, posicion,Quaternion.identity, transform); // Se crea una fila de tipo normal, el transform es para que sea hijo del transform
            posicion.z += 1; // Se creará la siguiente fila, la contigua
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W)) // Si avanzas una casillas
        {
            GenerateMoreSuelo();
        }
    }

    void NextSuelo(GameObject tipoSuelo)
    {
        Instantiate(tipoSuelo, posicion, Quaternion.identity, transform); // Se crea una fila de x tipo, el quaternion.identity es porque hay que tenerlo
        posicion.z += 1; 
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
}
