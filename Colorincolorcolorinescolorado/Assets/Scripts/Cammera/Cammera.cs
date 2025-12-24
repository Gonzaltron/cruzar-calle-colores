using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;

public class Cammera : MonoBehaviour
{
    [SerializeField] Canvas canvas;         //canvas
    public GameObject player;               //drag player gameobject into this in inspector
    float Speed;                            //velocidad de movimiento hacia delante
    public float SpeedNormal;               //Velocidad cuando el jugador est� cerca
    public float SpeedFast;                 //Velocidad cuando el jugador est� lejos
    float distanceZstart;                   //distancia inicial entre c�mara y jugador
    public float smoothSpeed;               //suavizado de cambio de velocidad
    public float sidewaisMovementDuration;  //duraci�n del movimiento lateral
    public float sideMovement;              //distancia del movimiento lateral
    private float distanceZ;
    public bool activo;
    bool unaVez;
    bool perdido = false;
    [SerializeField] Muerte muerte;
    void Start()
    {
        canvas = Object.FindFirstObjectByType<Canvas>();                       //busca el canvas en la escena
        distanceZstart = player.transform.position.z - transform.position.z;   //calcula la distancia inicial entre c�mara y jugador
        distanceZ = distanceZstart;
        unaVez = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(activo)
        {
            CameraPositionStart();  //Actualiza la posicion de la camara                      
            CameraMovement();       //Movimiento lateral de la c�mara con las flechas izquierda y derecha
        } 
        if(!unaVez)
        {
            CameraPositionStart();  //Actualiza la posicion de la camara                       
            CameraMovement(); 
            unaVez = true; 
        }  

        //si se cumplen las distancias, llama a las funciones correspondientes
        if(distanceZ < 6 && distanceZ > -2)
        {
            ReduceSpeed();
        }
        else if(distanceZ >= 6)
        {
            CameraSpeedFast();
        }
        else if(distanceZ <= -2 && !perdido)
        {
            perdido = true;
            StopCamera();
        }
    }
    private void CameraPositionStart()
    {
        distanceZ = player.transform.position.z - transform.position.z;                       //calcula la distancia entre c�mara y jugador
        Vector3 newCameraPosition = transform.position + new Vector3(0, 0, Speed * Time.deltaTime); //calcula la nueva posici�n de la c�mara
        transform.position = newCameraPosition;                                                     //actualiza la posicion de la camara
    }
    private void CameraSpeedFast()    //cambia la velocidad de la c�mara a SpeedFast
    {
        DOTween.To(() => Speed, x => Speed = x, SpeedFast, smoothSpeed); //cambia la velocidad a SpeedFast con suavizado
    }
    private void ReduceSpeed()        //cuando el jugador est� cerca, reduce la velocidad   
    {
        DOTween.To(() => Speed, x => Speed = x, SpeedNormal, smoothSpeed); //cambia la velocidad a SpeedNormal con suavizado
    }
    private void StopCamera()       //cuando el jugador esaa muy cerca, muestra el canvas y detiene la c�mara
    {
        Speed = 0;
        muerte.muerteJugador();
    }
    private void CameraMovement()       //movimiento lateral de la c�mara con las flechas izquierda y derecha
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))                                              //si se pulsa la flecha izquierda
        {
            transform.DOMoveX(transform.position.x - sideMovement, sidewaisMovementDuration); //mueve la c�mara a la izquierda, con duraci�n sidewaisMovementDuration
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))                                             //si se pulsa la flecha derecha
        {
            transform.DOMoveX(transform.position.x + sideMovement, sidewaisMovementDuration); //mueve la c�mara a la derecha, con duraci�n sidewaisMovementDuration
        }
    }
}