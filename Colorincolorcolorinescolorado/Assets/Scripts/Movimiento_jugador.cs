using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimiento_jugador: MonoBehaviour
{

    private float moveSpeed;
    private Rigidbody rb2d;
    private Vector3 change;
    bool isXMoving;
    bool isYMoving;


    void Start()
    {
        rb2d = GetComponent<Rigidbody>();
    }

    void Update()
    {
        change.x = Input.GetAxisRaw("Horizontal"); // Se puede mover ahora a los lados
        change.z = Input.GetAxisRaw("Vertical"); // Se puede mover ahora hacia delante
  
    }

    bool isMoving = false;


    void FixedUpdate()
    {
        // En caso de no estar moviéndose, actualizar el movimiento para que se mueva

        if (!isMoving) 
        {
            rb2d.MovePosition(rb2d.position + change * moveSpeed * Time.fixedDeltaTime);

            moveSpeed = 50;
         
        }

        // Si la magnitud es 0 es que estamos quieto
        isMoving = (change.magnitude != 0);
    }
}
