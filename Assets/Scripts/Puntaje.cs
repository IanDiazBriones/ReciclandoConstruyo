using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Puntaje : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject MonedasGameObj;
    public static int MonedasAct;
    void Awake()
    {
        MonedasAct = PlayerPrefs.GetInt("Monedas", 0);
    }

    // Update is called once per frame
    void Update()
    {
    
        MonedasGameObj.GetComponent<TMPro.TextMeshProUGUI>().text = ""+MonedasAct;
        //Debug.Log("Monedas : "+MonedasAct);
    }

    public void AñadirMonedas(int Monedas)
    {
    	MonedasAct += Monedas;
        PlayerPrefs.SetInt("Monedas", MonedasAct);
    }


    public void RestarMonedas(int Monedas)
    {
    	MonedasAct -= Monedas;
        PlayerPrefs.SetInt("Monedas", MonedasAct);
    }

    public int GetMonedas()
    {
        return MonedasAct;
    }



}
