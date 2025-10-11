using UnityEngine;
using System.Collections;

public class Calamar : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float TimeOnSquid;
    bool isOnSquid = false;
    [SerializeField] bool dead = false;
    public Mesh mallaVivo;
    public Mesh mallaMuerto;
    Mesh malla;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        malla = mallaVivo;
        this.GetComponent<MeshFilter>().mesh = mallaVivo;
    }

    // Update is called once per frame
    void Update()
    {
        this.GetComponent<MeshFilter>().mesh = malla;
        transform.Translate(0, 0, speed * Time.deltaTime); //se mueve en z, invertir speed para que vaya a -z
        if (isOnSquid)
        {
            StartCoroutine(time());
        }

        if (dead)
        {
           malla = mallaMuerto;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isOnSquid = true;
            transform.parent = other.transform;
        }
    }

    void OnCollisionStay(Collision other)
    {
        if (other.gameObject.CompareTag("Player") && dead == true)
        {
            //acaba la partida (cuando se haga el menú)
            Debug.Log("Game Over");
        }
    }

    IEnumerator time()
    {
        yield return new WaitForSeconds(TimeOnSquid);
        dead = true;
    }
}