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
    public float SpeedNormal;               //Velocidad cuando el jugador está cerca
    public float SpeedFast;                 //Velocidad cuando el jugador está lejos
    float distanceZstart;                   //distancia inicial entre cámara y jugador
    public float smoothSpeed;               //suavizado de cambio de velocidad
    public float sidewaisMovementDuration;  //duración del movimiento lateral
    public float sideMovement;              //distancia del movimiento lateral
    private float distanceZ;
    void Start()
    {
        canvas = FindObjectOfType<Canvas>();                                   //busca el canvas en la escena
        canvas.enabled = false;                                                //desactiva el canvas al inicio
        distanceZstart = player.transform.position.z - transform.position.z;   //calcula la distancia inicial entre cámara y jugador
    }

    // Update is called once per frame
    void Update()
    {
        CameraPositionStart();  //Actualiza la posicion de la camara
        CameraSpeed();          //Cambia la velocidad de la camara según el jugador                       
        ReduceSpeed();          //Cuando el jugador está cerca, reduce la velocidad
        StopCamera();           //Cuando el jugador está muy cerca, muestra el canvas y detiene la cámara
        CameraMovement();       //Movimiento lateral de la cámara con las flechas izquierda y derecha
    }
    private void CameraPositionStart()
    {
        float distanceZ = player.transform.position.z - transform.position.z;                       //calcula la distancia entre cámara y jugador
        Vector3 newCameraPosition = transform.position + new Vector3(0, 0, Speed * Time.deltaTime); //calcula la nueva posición de la cámara
        transform.position = newCameraPosition;                                                     //actualiza la posicion de la camara
    }
    private void CameraSpeed()        //cambia la velocidad de la cámara según la distancia al jugador
    {
        if (distanceZ < 6)
        {
            DOTween.To(() => Speed, x => Speed = x, SpeedNormal, smoothSpeed);  //cambia la velocidad a SpeedFast con suavizado
        }
        else
        {
            Speed = SpeedNormal;
        }
    }
    private void ReduceSpeed()        //cuando el jugador está cerca, reduce la velocidad   
    {
        if (distanceZ < 6 && distanceZ > 2)
        {
            DOTween.To(() => Speed, x => Speed = x, SpeedNormal, smoothSpeed); //cambia la velocidad a SpeedNormal con suavizado
        }
    }
    private void StopCamera()       //cuando el jugador esaa muy cerca, muestra el canvas y detiene la cámara
    {
        if (distanceZ < 2)
        {
            canvas.enabled = true;
            Speed = 0;
            //animación de muerte
            //quitarle el control al jugador
            //...
        }
    }
    private void CameraMovement()       //movimiento lateral de la cámara con las flechas izquierda y derecha
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))                                              //si se pulsa la flecha izquierda
        {
            transform.DOMoveX(transform.position.x - sideMovement, sidewaisMovementDuration); //mueve la cámara a la izquierda, con duración sidewaisMovementDuration
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))                                             //si se pulsa la flecha derecha
        {
            transform.DOMoveX(transform.position.x + sideMovement, sidewaisMovementDuration); //mueve la cámara a la derecha, con duración sidewaisMovementDuration
        }
    }
}