using UnityEngine;
using DG.Tweening;
using System.Collections;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Profiling;

public class CanvasManager : MonoBehaviour
{
    //variables de la clase
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
    [SerializeField] private GameObject botonResume;
    [SerializeField] private GameObject botonPausa;
    [SerializeField] private GameObject botonReturn;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // al iniciar el juego, desactiva los canvas de marcador y muerte, y activa el de menu principal
        movimientoJugador.enabled = false;
        botonPausa.SetActive(false);
        botonResume.SetActive(false);
        botonReturn.SetActive(false);
        canvasgMenuPrincipal.interactable = true;
        canvasgMenuPrincipal.alpha = 1f;
        canvasgMarcador.alpha = 0f;
        canvasgMarcador.interactable = false;
        canvasgMuerte.alpha = 0f;
        canvasgMuerte.interactable = false;
        camara.activo = false;
        int saved = PlayerPrefs.GetInt("Record", 0); //obtiene el record guardado 
        if (record != null) // si es valido
        {
            // muestra el record en el canvas de menu principal
            if (saved > 0)
            {
                record.text = "Record: " + saved.ToString();
                record.gameObject.SetActive(true);
            }
            else
            {
                record.gameObject.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CanvasMenuPrincpal()
    {
        //cuando se carga el menu principal, desactiva el canvas de muerte y activa el de menu principal
        movimientoJugador.enabled = false;
        int saved = PlayerPrefs.GetInt("Record", 0);
        if (record != null)
        {
            if (saved > 0)
            {
                record.text = "Record: " + saved.ToString();
                record.gameObject.SetActive(true);
            }
            else
            {
                record.gameObject.SetActive(false);
            }
        }
        SceneManager.LoadScene("SampleScene"); //recarga la escena
        canvasgMuerte.interactable = false;
        canvasgMuerte.DOFade(0f, 1f).From(1f);  //hace que desaoparezca el canvas de muerte
        canvasgMenuPrincipal.DOFade(1f, 1f).From(0f); //hace que aparezca el canvas de menu principal
        canvasgMenuPrincipal.interactable = true; //hace que se pueda interactauar con el canvas deñ menu principal
        camara.activo = false;
    }

//cuando se da a jugar
    public void CanvasMarcador()
    {
        canvas.enabled = true; // se asegura de que el canvas este activo
        canvasgMarcador.interactable = true;
        if (botonPausa != null) 
        {
            botonPausa.SetActive(true); 
        }
        if (botonResume != null)
        {
            botonResume.SetActive(false);
        }
        canvasgMenuPrincipal.interactable = false;
        //hacen que aparezcan y desaparezcan los canvas correspondientes
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
        //muestra en pantalla el recor y la puntuación de la partida
        if (scorefinal != null)
        {
            scorefinal.text = "Score: " + puntuacion.ToString() + "\nHighscore: " + record.ToString();
        }
        canvasgMuerte.DOFade(1f, 0.5f).From(0f);

        //guarfda el record si se ha superado
        PlayerPrefs.SetInt("Record", record);
        PlayerPrefs.Save();
        canvasgMuerte.interactable = true;
        movimientoJugador.enabled = false;
        camara.activo = false;
    }
    
    public void Pausa()
    {
        Time.timeScale = 0f; // El tiempo se para
        botonPausa.SetActive(false); // Desaparece el boton de pausa
        botonResume.SetActive(true); // Aparece el boton de reanudar
        botonReturn.SetActive(true); // Aparece el boton de volver
        movimientoJugador.enabled = false; // El jugador no se puede mover
    }

    public void Resume()
    {
        //reanuda el juego y oculta los botones correspondientes
        Time.timeScale = 1f;
        botonPausa.SetActive(true);
        botonResume.SetActive(false);
        botonReturn.SetActive(false);
        movimientoJugador.enabled = true;
    }

    public void Returns()
    {
        //recarga la escena desde el principio
        botonPausa.SetActive(false);
        botonResume.SetActive(false);
        botonReturn.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene("SampleScene"); // Reinicia la escena de juego
    }

    IEnumerator WaitAndResume()
    {
        //espera un momento antes de reanudar el movimiento del jugador y la camara
        yield return new WaitForSeconds(0.1f);
        camara.activo = true;
        movimientoJugador.enabled = true;
    }
}
