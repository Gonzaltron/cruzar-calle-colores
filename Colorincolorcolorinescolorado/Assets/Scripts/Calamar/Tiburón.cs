using UnityEngine;

public class Tiburón : MonoBehaviour
{
    [SerializeField] float speed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

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
            //acaba la partida (cuando se haga el menú)
            Debug.Log("Game Over");
        }
    }
}
