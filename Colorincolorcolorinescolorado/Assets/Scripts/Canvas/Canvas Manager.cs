using UnityEngine;
using DG.Tweening;
using System.Collections;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Profiling;

public class CanvasManager : MonoBehaviour
{
    public CanvasGroup canvasgMarcador;
    public CanvasGroup canvasgMuerte;
    public CanvasGroup canvasgMenuPrincipal;
    [SerializeField] Canvas canvas;
    public Movimiento_jugador movimientoJugador;
    float ScoreMuerte;
    public TMP_Text scorefinal;
    public bool time;
    [SerializeField] Cammera camara;
    [SerializeField] TMP_Text record;
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
        record.text = "Record: " + PlayerPrefs.GetInt("Record", 0).ToString();
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
        record.text = "Record: " + PlayerPrefs.GetInt("Record", 0).ToString();
        canvasgMuerte.DOFade(0f, 1f).From(1f);
        canvasgMenuPrincipal.DOFade(1f, 1f).From(0f);
        canvasgMenuPrincipal.interactable = true;
        camara.activo = false;
    }

    public void CanvasMarcador()
    {
        canvas.enabled = true;
        canvasgMarcador.interactable = true;
        canvasgMenuPrincipal.interactable = false;
        canvasgMenuPrincipal.DOFade(0f, 1f).From(1f);
        canvasgMarcador.DOFade(1f, 1f).From(0f);
        canvasgMuerte.DOFade(0f, 1f).From(1f);
        canvasgMuerte.interactable = false;
        StartCoroutine(WaitAndResume());
        camara.activo = true;
    }

    public void CanvasMuerte()
    {
        canvasgMarcador.interactable = false;
        canvasgMarcador.DOFade(0f, 0.5f).From(1f);
        int puntuacion = Mathf.FloorToInt(movimientoJugador.score);
        int record = movimientoJugador.highscore;
        if (scorefinal != null)
        {
            scorefinal.text = "Score: " + puntuacion.ToString() + "\nHighscore: " + record.ToString();
        }
        canvasgMuerte.DOFade(1f, 0.5f).From(0f);
        PlayerPrefs.SetInt("Record", record);
        PlayerPrefs.Save();
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
