using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;

public class Cammera : MonoBehaviour {


    public GameObject player; // drag player gameobject into this in inspector
    float Speed; //velocidad de movimiento hacia delante
    public float SpeedNormal;  //Velocidad cuando el jugador esta cerca
    public float SpeedFast; //Velocidad cuando el jugador esta lejos
    [SerializeField] Canvas canvas; //canvas
    float distanceZstart; //distancia inicial entre camara y jugador
    public float smoothSpeed; //suavizado de cambio de velocidad
    public float sidewaisMovementDuration; //duracion del movimiento lateral
    public float sideMovement; //distancia del movimiento lateral
    void Start ()
    {
        canvas = FindObjectOfType<Canvas>(); //busca el canvas en la escena
        canvas.enabled = false; //desactiva el canvas al inicio
        distanceZstart = player.transform.position.z - transform.position.z; //calcula la distancia inicial entre camara y jugador
    }

    // Update is called once per frame
    void Update()
    {
        float distanceZ = player.transform.position.z - transform.position.z; //calcula la distancia entre camara y jugador
        Vector3 newCameraPosition = transform.position + new Vector3(0, 0, Speed * Time.deltaTime); //calcula la nueva posicion de la camara
        transform.position = newCameraPosition; //actualiza la posicion de la camara


        //cambia la velocidad de la camara segun la distancia al jugador
        if (distanceZ > 6)
        {
            DOTween.To(() => Speed, x => Speed = x, SpeedFast, smoothSpeed);
        }
        else
        {
            Speed = SpeedNormal;
        }

        //cuando el jugador esta cerca, reduce la velocidad
        if (distanceZ < 6 && distanceZ > 2)
        {
            DOTween.To(() => Speed, x => Speed = x, SpeedNormal, smoothSpeed);
        }

        //cuando el jugador esta muy cerca, muestra el canvas y detiene la camara
        if (distanceZ < 2)
        {
            canvas.enabled = true;
            Speed = 0;
            //animacion de muerte
            //quitarle el control al jugador
            //...
        }

        //movimiento lateral de la camara con las flechas izquierda y derecha
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.DOMoveX(transform.position.x -sideMovement, sidewaisMovementDuration);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.DOMoveX(transform.position.x +sideMovement, sidewaisMovementDuration);
        }
    }
}