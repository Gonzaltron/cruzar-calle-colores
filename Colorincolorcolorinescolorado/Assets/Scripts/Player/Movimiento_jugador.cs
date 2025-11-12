using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimiento_jugador: MonoBehaviour
{

    private float moveSpeed;
    private Rigidbody rb2d;
    private Vector3 change;
  
    public Vector2 direction;
    public float posicionMax;

    bool isMoving = false;
   

    void Start()
    {
        posicionMax = transform.position.z; // Inicializa la posición máxima 
        rb2d = GetComponent<Rigidbody>();
    }

    void Update()
    {
        posicionMax = Mathf.Max(posicionMax, transform.position.z); // Actualiza la posición máxima alcanzada cogiendo el valor mayor entre los dos

        if (Input.GetKeyDown(KeyCode.A)) // Si se pulsa la A
        {
            direction = new Vector2(-1, 0); // La dirección es hacia la izquierda
            Movement(); // Se mueve
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            direction = new Vector2(1, 0);
            Movement();
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            direction = new Vector2(0, 1);
            Movement();
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            direction = new Vector2 (0, -1);
            Movement();
        }
    }

    void FixedUpdate()
    {
        if (!isMoving) // Si no se está moviendo
        {
            rb2d.MovePosition(rb2d.position + change * moveSpeed * Time.fixedDeltaTime); // Se mueve en la dirección indicada
            moveSpeed = 50; 
        }
        isMoving = (change.magnitude != 0); // Si la magnitud es 0 es que está quieto
    }

    void Movement()
    {   
        Collider[] hitColliders = Physics.OverlapBox(gameObject.transform.position + new Vector3(direction.x, -1, direction.y), Vector3.one*0.2f, Quaternion.identity);
        int i = 0;

        while (i < hitColliders.Length)
        {
            Casilla casilla = hitColliders[i].GetComponent<Casilla>();
            if (casilla != null)
            {
                Vector3 p = casilla.getPosition();
                transform.position = p;
                break;
            }
            i++;
        }
    }
}
