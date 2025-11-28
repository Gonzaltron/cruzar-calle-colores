using UnityEngine;

public class Casilla : MonoBehaviour
{
    public GameObject casilla;
    public Transform posicion;
    [SerializeField] public int color;
    [SerializeField] public bool activado;
    public GameObject obstaculo;
    public bool tieneObstaculo = false;
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    public void ActivarObstaculo()
    {
        tieneObstaculo = true;
        obstaculo.SetActive(true);
    }
    public Vector3 getPosition()
    {
        return posicion.position;
    }
}

