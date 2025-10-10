using UnityEngine;
using System.Collections;

public class Calamar : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float TimeOnSquid;
    bool isOnSquid = false;
    [SerializeField] bool dead = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, 0, speed * Time.deltaTime); //se mueve en z, invertir speed para que vaya a -z
        if (isOnSquid)
        {
            StartCoroutine(time());
        }

        if (dead)
        {
            //cambia de color
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

    IEnumerator time()
    {
        yield return new WaitForSeconds(TimeOnSquid);
        dead = true;
    }
}