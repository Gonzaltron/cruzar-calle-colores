using UnityEngine;
using System.Collections;


public class Tiburón : MonoBehaviour
{
    public Canvas canvas;
    [SerializeField] float speed; //variable de velocidad
    [SerializeField] Mesh Tibu1;
    [SerializeField] Mesh Tibu2;
    [SerializeField] Mesh Tibu3;
    [SerializeField] Mesh Tibu4;
    [SerializeField] Mesh Tibu5;
    [SerializeField] Mesh Tibu6;
    [SerializeField] Mesh Tibu7;
    public float animationWaitTime; //tiempo de espera entre cada frame de la animaci�
    public Vector3 posicionInicial;
    public GameObject tiburon;
 

    MeshFilter Meshfilter;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        posicionInicial = gameObject.transform.position;
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>(); //Busca el canvas en la escena, y lo asigna a l avariable
        Meshfilter = GetComponent<MeshFilter>();
        StartCoroutine(Animation());
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(-0.04f, 0, speed * Time.deltaTime); //se mueve en z, invertir speed para que vaya a -z
    }

 
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("trigger"))
        {
            transform.position = posicionInicial;
        }

        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.SetParent(this.transform); //hace que el jugador sea hijo del tiburon
            //acaba la partida (cuando se haga el men�)
            canvas.enabled = true; //muestrta el canvas en pantalla
        }
    }
    

    IEnumerator Animation()
    {
        while (true)
        {
            Meshfilter.mesh = Tibu1; //asigna la malla Tibu1 al Meshfilter
            yield return new WaitForSeconds(animationWaitTime); //espera
            //Meshfilter.mesh = Tibu2;
            //yield return new WaitForSeconds(animationWaitTime);
            //Meshfilter.mesh = Tibu3;
            //yield return new WaitForSeconds(animationWaitTime);
            Meshfilter.mesh = Tibu4; //asigna la malla Tibu4 al Meshfilter
            yield return new WaitForSeconds(animationWaitTime); //espera
           // Meshfilter.mesh = Tibu3;
           // yield return new WaitForSeconds(animationWaitTime);
           // Meshfilter.mesh = Tibu2;
           // yield return new WaitForSeconds(animationWaitTime);
            Meshfilter.mesh = Tibu1; //asigna la malla Tibu1 al Meshfilter
            yield return new WaitForSeconds(animationWaitTime); //espera
           //Meshfilter.mesh = Tibu5;
           //yield return new WaitForSeconds(animationWaitTime);
           //Meshfilter.mesh = Tibu6;
           //yield return new WaitForSeconds(animationWaitTime);
            Meshfilter.mesh = Tibu7; //asigna la malla Tibu7 al Meshfilter
            yield return new WaitForSeconds(animationWaitTime); //espera
           //Meshfilter.mesh = Tibu6;
           //yield return new WaitForSeconds(animationWaitTime);
           //Meshfilter.mesh = Tibu5;
           //yield return new WaitForSeconds(animationWaitTime);
        }
       
    }
}