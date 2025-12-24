using UnityEngine;

public class Casilla : MonoBehaviour
{
    public GameObject casilla;
    public Transform posicion;
    [SerializeField] public int color;
    [SerializeField] public bool activado;
    public GameObject obstaculo;
    public bool tieneObstaculo = false;

    public void ActivarObstaculo()
    {
        tieneObstaculo = true; 
        obstaculo.SetActive(true); // hace que el obstaculo aparezca y funcione
    }
    public Vector3 getPosition()
    {
        return posicion.position; // devuelve la posicion del obstaculo
    }
}

