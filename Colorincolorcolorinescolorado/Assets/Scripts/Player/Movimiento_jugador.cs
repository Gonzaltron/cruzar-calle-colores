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
    public float score = 0;
    public int highscore;
    public string CanvasMarcador;
    public TMP_Text textoMarcador;
    private cambio playercambiocolor;
    
   

    void Start()
    {
        posicionMax = transform.position.z; // Inicializa la posici�n m�xima 
        rb2d = GetComponent<Rigidbody>();
        muerte = GetComponent<Muerte>();
        textoMarcador = GameObject.Find("CanvasMarcador").GetComponentInChildren<TMP_Text>();
        highscore = PlayerPrefs.GetInt("Highscore", 0);
        int initialScoreInt = Mathf.FloorToInt(score);
        initialScoreInt = Mathf.Max(0, initialScoreInt);
        textoMarcador.text = "Score: " + initialScoreInt.ToString();
        playercambiocolor = GetComponent<cambio>();
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
            Movement();
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            direction = new Vector2 (0, -1);
            // Prevent score from going below 0
            score = Mathf.Max(0f, score - 1f);
            int scoreIntAfter = Mathf.FloorToInt(score);
            textoMarcador.text = "Score: " + Mathf.Max(0, scoreIntAfter).ToString() + "  Highscore: " + highscore.ToString();
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
            bool canMove = false;
            Casilla casilla = hitColliders[i].GetComponent<Casilla>();
            if (casilla != null)
            {
                if (playercambiocolor.currentColor == casilla.color && casilla.color != 0)
                {
                    canMove = true;
                }
                else if (playercambiocolor.currentColor != casilla.color && casilla.color != 0)
                {
                    muerte.muerteJugador();
                    break;
                }
                if (casilla.tieneObstaculo == false)
                {
                    canMove = true;
                }

                if (canMove)
                {
                    Vector3 p = casilla.getPosition();
                    float previousZ = transform.position.z;
                    transform.position = p;
                    // If we moved forward (z increased), update score based on z
                    if (transform.position.z > previousZ)
                    {
                        score = Mathf.Max(0f, transform.position.z - 6.979999f);
                        int scoreInt = Mathf.FloorToInt(score);
                        if (scoreInt > highscore)
                        {
                            highscore = scoreInt;
                            PlayerPrefs.SetInt("Highscore", highscore);
                            PlayerPrefs.Save();
                        }
                        textoMarcador.text = "Score: " + scoreInt.ToString();
                    }
                    break;
                }
            }
            i++;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Tiburon") || (collision.gameObject.CompareTag("Calamar") && collision.gameObject.GetComponent<Calamar>().dead))
        {
            dead = true;
            muerte.muerteJugador();
        }
    }


}
