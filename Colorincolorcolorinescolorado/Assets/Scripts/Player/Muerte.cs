using UnityEngine;
using DG.Tweening;

public class Muerte : MonoBehaviour
{
    public MeshRenderer jugador;
    public Movimiento_jugador movimientoJugador;
    public CanvasGroup canvasgMarcador;
    public CanvasGroup canvasgMuerte;
    public Cammera camara;
    [SerializeField] CanvasManager canvasManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        jugador = gameObject.GetComponent<MeshRenderer>();
        movimientoJugador = GetComponent<Movimiento_jugador>();
        canvasManager = this.gameObject.GetComponent<CanvasManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void muerteJugador()
    {
        Debug.Log("muerto");
        movimientoJugador.enabled = false;
        jugador.material.DOFade(0f, 1f);
        Cammera cam = camara.GetComponent<Cammera>();
        cam.SpeedNormal = 0;
        cam.SpeedFast = 0;
        canvasManager.CanvasMuerte();
    }
}
