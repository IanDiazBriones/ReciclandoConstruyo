using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;

public class Act1GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Pregunta[] Preguntas;
    public static List<Pregunta> SinResponder;
    private Pregunta pregunta;
    public GameObject inputField;
    public TextMeshProUGUI TextoPregunta;
    public GameObject SliderManager;
    public GameObject StarEffect;
    [SerializeField]
    private GameObject BDController;

    void Start()
    {
        if (SinResponder == null || SinResponder.Count == 0)
        {
            SinResponder = BDController.GetComponent<ConexionBD>().ReturnPreguntas();
        }

        GetRandomQuestion();
    	Debug.Log(pregunta.pregunta+""+pregunta.respuesta);
    	TextoPregunta.text = pregunta.pregunta;
    }

    private void GetRandomQuestion()
    {
    	int RandomQuestionIndex = Random.Range(0, SinResponder.Count);
    	pregunta = SinResponder[RandomQuestionIndex];

    	SinResponder.RemoveAt(RandomQuestionIndex);
    }

    public void Responder()
    {
    	Debug.Log(System.Convert.ToInt32(inputField.GetComponent<TMP_InputField>().text));
        
        if (pregunta.respuesta == System.Convert.ToInt32(inputField.GetComponent<TMP_InputField>().text))
    	{
    		SliderManager.GetComponent<SliderManager>().DañoOponente(10);
    		GetRandomQuestion();
            Instantiate(StarEffect, new Vector3(0, 0, 0), Quaternion.identity, GameObject.FindGameObjectWithTag("Canvas").transform);
            TextoPregunta.text = pregunta.pregunta;
            BDController.GetComponent<ConexionBD>().InsertMovement(pregunta.tipo, System.Convert.ToInt32(inputField.GetComponent<TMP_InputField>().text), 1, 9006, 290322, "");
        } else {
    		SliderManager.GetComponent<SliderManager>().DañoJugador(30);
            BDController.GetComponent<ConexionBD>().InsertMovement(pregunta.tipo, System.Convert.ToInt32(inputField.GetComponent<TMP_InputField>().text), 0, 9006, 290322, "");

        }
    }
    
}
