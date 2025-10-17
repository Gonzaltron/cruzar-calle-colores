using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;

public class Cammera : MonoBehaviour {


    public GameObject player; // drag player gameobject into this in inspector
    float Speed;
    public float SpeedNormal;
    public float SpeedFast;
    [SerializeField] Canvas canvas;
    float distanceZstart;
    public float smoothSpeed;
    public float sidewaisMovementDuration;
    public float sideMovement;
    void Start ()
    {
        canvas = FindObjectOfType<Canvas>();
        canvas.enabled = false;
        distanceZstart = player.transform.position.z - transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        float distanceZ = player.transform.position.z - transform.position.z;
        Vector3 newCameraPosition = transform.position + new Vector3(0, 0, Speed * Time.deltaTime);
        transform.position = newCameraPosition;



        if (distanceZ > 6)
        {
            DOTween.To(() => Speed, x => Speed = x, SpeedFast, smoothSpeed);
        }
        else
        {
            Speed = SpeedNormal;
        }

        if(distanceZ < 6 && distanceZ > 2)
        {
            DOTween.To(() => Speed, x => Speed = x, SpeedNormal, smoothSpeed);
        }

        if (distanceZ < 2)
        {
            canvas.enabled = true;
            Speed = 0;
            //animacion de muerte
            //quitarle el control al jugador
            //...
        }

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