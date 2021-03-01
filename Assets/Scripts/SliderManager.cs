using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Lean.Transition;

public class SliderManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider sliderJugador;
    public Slider sliderOponente;
    private static int vidaJugador = 100;
    private static int vidaOponente = 100;
    public GameObject Mensaje;
    public GameObject BotonAtras;
    public GameObject BotonAdelante;
    public TMP_Text text;
    void Start()
    {
        vidaJugador = 100;
        vidaOponente = 100;
    }

    // Update is called once per frame
    void Update()
    {
        if(sliderJugador.value <= 0)
        {
            Mensaje.SetActive(true);
            BotonAdelante.SetActive(false);
            text.text = "Intentalo Denuevo";
        }

        if (sliderOponente.value <= 0)
        {
            Mensaje.SetActive(true);
            BotonAtras.SetActive(false);
            text.text = "Felicitaciones";
        }

    }

    public void DañoJugador(int daño)
    {
        vidaJugador -= daño; 
    	sliderJugador.value = vidaJugador;
    }

    public void DañoOponente(int daño)
    {   
        vidaOponente -= daño;
    	sliderOponente.value = vidaOponente;
    }
}
