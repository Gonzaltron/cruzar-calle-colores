using UnityEngine;

public class Obstaculo : MonoBehaviour
{
    [SerializeField] public bool activado;
    public GameObject obstaculo;
    public Casilla ScriptCasilla;
    public GameObject casilla;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        activado = false;
        if (activado == false)
        {
            Activacion();
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    void Activacion()
    {
        int probability = Random.Range(0, 100);
        if (probability > 80)
        {
            obstaculo.SetActive(true);
            activado = true;     
            if (activado == true)
            {
                casilla.GetComponent<Casilla>().activado = true;
            }
        }
        else
        {
            obstaculo.SetActive(false);
        }
    }
}
