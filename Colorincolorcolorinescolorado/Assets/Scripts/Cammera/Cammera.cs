using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cammera : MonoBehaviour {


    public GameObject player; // drag player gameobject into this in inspector
    float Speed;
    public float SpeedNormal;
    public float SpeedFast;
    [SerializeField] Canvas canvas;
    public bool isSmoothFollow = true;
    float distanceZstart;
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
            Speed = SpeedFast;
        }
        else
        {
            Speed = SpeedNormal;
        }

        if (distanceZ < 2)
        {
            canvas.enabled = true;
            Speed = 0;
            //animacion de muerte
            //quitarle el control al jugador
            //...
        }
    }
}