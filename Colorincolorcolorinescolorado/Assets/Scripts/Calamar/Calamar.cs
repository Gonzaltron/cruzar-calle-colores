using UnityEngine;
using System.Collections;

public class Calamar : MonoBehaviour
{
    [SerializeField] float speed;                                   //variable de velocidad
    [SerializeField] float TimeOnSquid;                             //tiempo que pasa desde que el jugador sube al calamar, hasta que este muere
    bool isOnSquid = false;                                         //booleano para saber si el jugador est� sobre el calamar (e iniciar la corutina)
    [SerializeField] public bool dead = false;                      //booleano para saber si el calamar est� muerto o vivo
    public Mesh mallaVivo;                                          //malla del calamar vivo
    public Mesh mallaMuerto;                                        //mall del calamar muerto
    Mesh malla;                                                     //variabkle malla para programar mas comodo
    public Canvas canvas;                                           //variable del canvas
    bool a = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();  //encuentra el canvas en la escena y lo asigna a la variable
        malla = mallaVivo;                                          //asigna la mallaVivo a la variable malla
        this.GetComponent<MeshFilter>().mesh = mallaVivo;           //asigna la mallaVivo al MeshFilter (lo que hace que el calamar se vea de una forma u otra) del calamar
    }

    // Update is called once per frame
    void Update()
    {
        this.GetComponent<MeshFilter>().mesh = malla;               //asigna la malla (vivo o muerto) al MeshFilter del calamar
        transform.Translate(speed * Time.deltaTime, 0, 0);          //se mueve en z, invertir speed para que vaya a -z
        if (isOnSquid && !a)                                        //si el jugador est� sobre el calamar
        {
            a = true;
            StartCoroutine(time());                                 //llama a la corrutina
        }

        if (dead)                                                   //si el calamar est� muerto
        {
            malla = mallaMuerto;                                    //cambia la malla a la de muerto
        }
    }


    void OnCollisionEnter(Collision other)                          //cuando hay una colision
    {
        if (other.gameObject.CompareTag("Player"))                  //si el otro objeto tiene el tag player
        {
            isOnSquid = true;                                       //cambia el booleano a true
            other.transform.SetParent(this.transform);              //hace que el jugador sea hijo del calamar
        }
    }

    void OnCollisionStay(Collision other)                           //mientras haya una colision
    {
        if (other.gameObject.CompareTag("Player") && dead == true)  //si el otro objeto tiene el tag player y el calamar est� muerto
        {
            //el personaje muere
            canvas.enabled = true;                                  //muestra el canvas en pantalla
        }
    }

    void OnCollisionExit(Collision other)                           //cuando la colision termina
    {
        if (other.gameObject.CompareTag("Player"))                  //si el otro objeto tiene el tag player
        {
            other.transform.SetParent(null);                        //hace que el jugador deje de ser hijo del calamar
        }
    }

    IEnumerator time()                                              //corutina que espera un tiempo y luego mata al calamar
    {
        yield return new WaitForSeconds(TimeOnSquid);               //espera el tiempo asignado en TimeOnSquid
        dead = true;                                                //cambia el booleano a true (el calamar est� muerto)
    }
}