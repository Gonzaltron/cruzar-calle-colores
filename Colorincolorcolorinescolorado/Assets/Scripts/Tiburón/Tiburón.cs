using UnityEngine;

public class Tiburón : MonoBehaviour
{
    public Canvas canvas;
    [SerializeField] float speed; //variable de velocidad
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>(); //Busca el canvas en la escena, y lo asigna a l avariable
        canvas.enabled = false; //se asegura de que el canvas no se muestre en pantalla al inicio
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, 0, speed * Time.deltaTime); //se mueve en z, invertir speed para que vaya a -z
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.SetParent(this.transform); //hace que el jugador sea hijo del tiburon
            //acaba la partida (cuando se haga el menú)
            canvas.enabled = true; //muestrta el canvas en pantalla
        }
    }
}
