using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Movimiento_jugador: MonoBehaviour
{

    private float moveSpeed;
    private Rigidbody rb2d;
    private Vector3 change;
  
    public Vector2 direction;
    public float posicionMax;

    bool isMoving = false;
    public bool dead = false;
    public Muerte muerte;
    public Canvas Canvasmarcador;
    public int score = 0;
    public int highscore;
    public string CanvasMarcador;
    public TMP_Text textoMarcador;
   

    void Start()
    {
        posicionMax = transform.position.z; // Inicializa la posici�n m�xima 
        rb2d = GetComponent<Rigidbody>();
        muerte = GetComponent<Muerte>();
        textoMarcador = GameObject.Find("CanvasMarcador").GetComponentInChildren<TMP_Text>();
        textoMarcador.text = "Score: " + score.ToString();
    }

    void Update()
    {
        posicionMax = Mathf.Max(posicionMax, transform.position.z); // Actualiza la posici�n m�xima alcanzada cogiendo el valor mayor entre los dos

        if (Input.GetKeyDown(KeyCode.A)) // Si se pulsa la A
        {
            direction = new Vector2(-1, 0); // La direcci�n es hacia la izquierda
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
            score += 1;
            if (score > highscore)
            {
                highscore = score;
                textoMarcador.text = "Score: " + score.ToString();
            }
            Movement();
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            direction = new Vector2 (0, -1);
            score -= 1;
            Movement();
        }
    }

    void FixedUpdate()
    {
        if (!isMoving) // Si no se est� moviendo
        {
            rb2d.MovePosition(rb2d.position + change * moveSpeed * Time.fixedDeltaTime); // Se mueve en la direcci�n indicada
            moveSpeed = 50; 
        }
        isMoving = (change.magnitude != 0); // Si la magnitud es 0 es que est� quieto
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

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obastaculo") || collision.gameObject.CompareTag("Tiburon") || (collision.gameObject.CompareTag("Calamar") && collision.gameObject.GetComponent<Calamar>().dead))
        {
            dead = true;
            muerte.muerteJugador();
        }
    }
}
