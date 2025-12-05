using UnityEngine;
using DG.Tweening;
using System.Collections;
using UnityEngine.SceneManagement;
using TMPro;

public class CanvasManager : MonoBehaviour
{
    public CanvasGroup canvasgMarcador;
    public CanvasGroup canvasgMuerte;
    public CanvasGroup canvasgMenuPrincipal;
    public Movimiento_jugador movimientoJugador;
    float ScoreMuerte;
    public TMP_Text scorefinal;
    public bool time;
    [SerializeField] Cammera camara;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        movimientoJugador.enabled = false;
        canvasgMenuPrincipal.interactable = true;
        canvasgMenuPrincipal.alpha = 1f;
        canvasgMarcador.alpha = 0f;
        canvasgMarcador.interactable = false;
        canvasgMuerte.alpha = 0f;
        canvasgMuerte.interactable = false;
        camara.activo = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CanvasMenuPrincpal()
    {
        movimientoJugador.enabled = false;
        SceneManager.LoadScene("SampleScene");
        canvasgMuerte.interactable = false;
        canvasgMuerte.DOFade(0f, 1f).From(1f);
        canvasgMenuPrincipal.DOFade(1f, 1f).From(0f);
        canvasgMenuPrincipal.interactable = true;
        camara.activo = false;
    }

    public void CanvasMarcador()
    {
        canvasgMarcador.interactable = true;
        canvasgMenuPrincipal.interactable = false;
        canvasgMenuPrincipal.DOFade(0f, 1f).From(1f);
        canvasgMarcador.DOFade(1f, 1f).From(0f);
        StartCoroutine(WaitAndResume());
        camara.activo = true;
    }

    public void CanvasMuerte()
    {
        canvasgMarcador.interactable = false;
        canvasgMarcador.DOFade(0f, 0.5f).From(1f);
        ScoreMuerte = movimientoJugador.highscore;
        scorefinal = canvasgMuerte.GetComponentInChildren<TMP_Text>();
        scorefinal.text = "Puntuación = " + ScoreMuerte.ToString();
        canvasgMuerte.DOFade(1f, 0.5f).From(0f);
        canvasgMuerte.interactable = true;
        movimientoJugador.enabled = false;
        camara.activo = false;
    }
    

    public void Salir()
    {
        Application.Quit();
    }  
    

    IEnumerator WaitAndResume()
    {
        yield return new WaitForSeconds(0.1f);
        camara.activo = true;
        movimientoJugador.enabled = true;
    }
}
