using UnityEngine;

public class Casilla : MonoBehaviour
{
    public GameObject casilla;
    public Transform posicion;
   


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public Vector3 getPosition()
    {
        return posicion.position;
    }
}
