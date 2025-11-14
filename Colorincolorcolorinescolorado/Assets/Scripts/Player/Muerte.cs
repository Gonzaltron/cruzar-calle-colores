using UnityEngine;
using DG.Tweening;

public class Muerte : MonoBehaviour
{
    public MeshRenderer jugador;
    public Movimiento_jugador movimientoJugador;
    public Canvas canvasMuerte;
    public Cammera camara;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        jugador = gameObject.GetComponent<MeshRenderer>();
        movimientoJugador = GetComponent<Movimiento_jugador>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void muerteJugador()
    {
        movimientoJugador.enabled = false;
        jugador.material.DOFade(0f, 1f);
        canvasMuerte.enabled = true;
        Cammera cam = camara.GetComponent<Cammera>();
        cam.SpeedNormal = 0;
        cam.SpeedFast = 0;
    }
}
