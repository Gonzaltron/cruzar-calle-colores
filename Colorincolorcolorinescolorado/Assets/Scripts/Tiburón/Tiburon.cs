using UnityEngine;
using System.Collections;
using Unity.VisualScripting.ReorderableList.Element_Adder_Menu;

public class Tiburón : MonoBehaviour
{
    public Canvas canvas;
    [SerializeField] float speed; //variable de velocidad
    [SerializeField] Mesh Tibu1;
    //[SerializeField] Mesh Tibu2;
    //[SerializeField] Mesh Tibu3;
    [SerializeField] Mesh Tibu4;
    //[SerializeField] Mesh Tibu5;
    //[SerializeField] Mesh Tibu6;
    [SerializeField] Mesh Tibu7;
    [SerializeField] float animationWaitTime; //tiempo de espera entre cada frame de la animaci�

    MeshFilter Meshfilter;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>(); //Busca el canvas en la escena, y lo asigna a l avariable
        canvas.enabled = false; //se asegura de que el canvas no se muestre en pantalla al inicio
        Meshfilter = GetComponent<MeshFilter>();
        StartCoroutine(Animation());
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
            //acaba la partida (cuando se haga el men�)
            canvas.enabled = true; //muestrta el canvas en pantalla
        }
    }
    
    IEnumerator Animation()
    {
        while (true)
        {
            Meshfilter.mesh = Tibu1;
            yield return new WaitForSeconds(animationWaitTime);
            Meshfilter.mesh = Tibu4;
            yield return new WaitForSeconds(animationWaitTime);
            Meshfilter.mesh = Tibu1;
            yield return new WaitForSeconds(animationWaitTime);
            Meshfilter.mesh = Tibu7;
            yield return new WaitForSeconds(animationWaitTime);
        }
       
    }
}