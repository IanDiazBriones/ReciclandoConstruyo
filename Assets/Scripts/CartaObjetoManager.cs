using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CartaObjetoManager : MonoBehaviour
{
    // Start is called before the first frame updateasdasda
    [SerializeField]
    private GameObject PanelMonedas, button, storeManager;
    public string Elemento;
    public int valor;
    void Start()
    {
        PanelMonedas = GameObject.Find("PanelMonedas");
        storeManager = GameObject.Find("StoreManager");
        button.GetComponent<Button>().onClick.AddListener(delegate { Comprar(valor, Elemento) ; });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Comprar(int valor, string elemento)
    {
        if(PanelMonedas.GetComponent<Puntaje>().GetMonedas() < valor)
        {
            Debug.Log("No Alcanza el dinero" + valor);
        } else
        {
            Debug.Log("Comprado" + valor );
            PanelMonedas.GetComponent<Puntaje>().RestarMonedas(valor);
            switch (elemento)
            {
            case "Carton":
                    storeManager.GetComponent<StoreManager>().Carton += 1;
                    PlayerPrefs.SetInt("Carton", storeManager.GetComponent<StoreManager>().Carton);
                    break;
            case "Papel":
                    storeManager.GetComponent<StoreManager>().Papel += 1;
                    PlayerPrefs.SetInt("Papel", storeManager.GetComponent<StoreManager>().Papel);
                    break;
            case "Vidrio":
                    storeManager.GetComponent<StoreManager>().Vidrio += 1;
                    PlayerPrefs.SetInt("Vidrio", storeManager.GetComponent<StoreManager>().Vidrio);
                    break;
            case "Metal":
                    storeManager.GetComponent<StoreManager>().Metal += 1;
                    PlayerPrefs.SetInt("Metal", storeManager.GetComponent<StoreManager>().Metal);
                    break;

            }
        }
    }
}
