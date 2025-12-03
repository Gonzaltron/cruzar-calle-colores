using UnityEngine;
using DG.Tweening;
using System.Collections;
using UnityEngine.SceneManagement;
using TMPro;
using Unity.VisualScripting;

public class CanvasManager : MonoBehaviour
{
    public CanvasGroup canvasgMarcador;
    public CanvasGroup canvasgMuerte;
    public CanvasGroup canvasgMenuPrincipal;
    public Movimiento_jugador movimientoJugador;
    float ScoreMuerte;
    public TMP_Text scorefinal;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        movimientoJugador = GetComponent<Movimiento_jugador>();
        canvasgMenuPrincipal.interactable = true;
        canvasgMenuPrincipal.alpha = 1f;
        canvasgMarcador.alpha = 0f;
        canvasgMarcador.interactable = false;
        canvasgMuerte.alpha = 0f;
        canvasgMuerte.interactable = false;
        Time.timeScale = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CanvasMenuPrincpal()
    {
        SceneManager.LoadScene("SampleScene");
        canvasgMuerte.interactable = false;
        canvasgMuerte.DOFade(0f, 1f).From(1f);
        canvasgMenuPrincipal.DOFade(1f, 1f).From(0f);
        canvasgMenuPrincipal.interactable = true;
        movimientoJugador.enabled = false;
    }

    public void CanvasMarcador()
    {
        canvasgMenuPrincipal.interactable = false;
        canvasgMenuPrincipal.DOFade(0f, 1f).From(1f);
        canvasgMarcador.DOFade(1f, 1f).From(0f);
        canvasgMarcador.interactable = true;
        Time.timeScale = 1f;
        movimientoJugador.enabled = true;
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
        StartCoroutine(WaitAndPause());
    }
    

    public void Salir()
    {
        Application.Quit();
    }  
    
    IEnumerator WaitAndPause()
    {
        yield return new WaitForSeconds(1f);
        Time.timeScale = 0f;
    }
}
