using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool SharedInstance;
    public List<GameObject> pooledObjects;
    public GameObject objectToPool;
    public int amountToPool;
    
    void Awake() // Se ejecuta incluso antes que el start
    {
        SharedInstance = this;  // Solo puede haber un ObjectPool y se asigna a sí mismo como ese único ObjectPool
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pooledObjects = new List<GameObject> (); 
        GameObject tmp;

        for (int i = 0; i < amountToPool; i++) 
        {
            tmp = Instantiate(objectToPool); // Crea el clon temporal
            tmp.SetActive(false); // Antes de meterlo en la lista, desactiva el clon temporal
            pooledObjects.Add(tmp); // Añade el clon temporal a la lista
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject GetPooledObject() // Función paara poder utilizar el loop con otros objetos
    {
        for (int i = 0; i < amountToPool; i++)
        {
            if (!pooledObjects[i].activeInHierarchy) // Si el objeto de la lista no está activo
            {
                return pooledObjects[i]; // Devolver el objeto, activado
            }
        }
        return null; 
    }
}
