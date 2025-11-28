using UnityEngine;
using DG.Tweening;

public class CanvasManager : MonoBehaviour
{
    public CanvasGroup canvasgMarcador;
    public CanvasGroup canvasgMuerte;
    public CanvasGroup canvasgMenuPrincipal;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CanvasMenuPrincpal()
    {
        canvasgMuerte.interactable = false;
        canvasgMuerte.DOFade(0f, 1f).From(1f);
        canvasgMenuPrincipal.DOFade(1f, 1f).From(0f);
        canvasgMenuPrincipal.interactable = true;
    }

    public void CanvasMarcador()
    {
        canvasgMenuPrincipal.interactable = false;
        canvasgMenuPrincipal.DOFade(0f, 1f).From(1f);
        canvasgMarcador.DOFade(1f, 1f).From(0f);
        canvasgMarcador.interactable = true;
    }

    public void CanvasMuerte()
    {
        canvasgMarcador.interactable = false;
        canvasgMarcador.DOFade(0f, 1f).From(1f);
        canvasgMuerte.DOFade(1f, 1f).From(0f);
        canvasgMuerte.interactable = true;
    }
}
