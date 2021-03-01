using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SendScore : MonoBehaviour
{
    // Start is called before the first frame update
    Animator Anim;
    GameObject TextoPuntajeMov;
    public static int NewScore;
    void Start()
    {
        StartAnimationScore(NewScore);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void ScenePuntaje(int MultiplicadorPuntaje)
    {
        NewScore = MultiplicadorPuntaje * System.Convert.ToInt32(TimerControl.GetLeftTime());
        
        
    }

    public void ScenePuntajeActividad1(int MultiplicadorPuntaje)
    {
        NewScore = MultiplicadorPuntaje;


    }

    public void StartAnimationScore(int puntajeGanado)
    {
        TextoPuntajeMov = GameObject.Find("PuntajeMoviendose");
        Anim = TextoPuntajeMov.GetComponent<Animator>();
        TextoPuntajeMov.GetComponent<TMPro.TextMeshProUGUI>().text = "+"+puntajeGanado;
        Anim.Play("Animacion Puntaje");
        UpdateScore(puntajeGanado);
    }
    public void UpdateScore(int newMonedas)
    {
    	GameObject.Find("PanelMonedas").GetComponent<Puntaje>().AñadirMonedas(newMonedas);
    }
    
}
