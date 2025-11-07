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
    public Vector2 direction;

    void Start()
    {
        rb2d = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            direction = new Vector2(-1, 0);
            Movement();
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            direction = new Vector2(0, 1);
            Movement();
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            direction = new Vector2(1, 0);
            Movement();
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            direction = new Vector2 (0, -1);
            Movement();
        }
  
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

    void Movement()
    {

        Collider[] hitColliders = Physics.OverlapBox(gameObject.transform.position + new Vector3(direction.x, -1, direction.y), Vector3.one*0.2f, Quaternion.identity);
        int i = 0;

        while (i < hitColliders.Length)
        {
            i++;
        }
    }
}
