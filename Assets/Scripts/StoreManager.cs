using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StoreManager : MonoBehaviour
{
    [SerializeField]
    private ElementoTienda[] ElementosTienda;
    public TextMeshProUGUI cantidadCarton;
    public TextMeshProUGUI cantidadPapel;
    public TextMeshProUGUI cantidadVidrio;
    public TextMeshProUGUI cantidadMetal;
    [SerializeField]
    private Transform Content;
    [SerializeField]
    private GameObject StoreCard;
    [HideInInspector]
    public int Carton = 0, Papel = 0, Vidrio = 0, Metal = 0;

    private void Start()
    {
        Carton = PlayerPrefs.GetInt("Carton", 0);
        Papel = PlayerPrefs.GetInt("Papel", 0);
        Vidrio = PlayerPrefs.GetInt("Vidrio", 0);
        Metal = PlayerPrefs.GetInt("Metal", 0);

        foreach (ElementoTienda ElementoTienda in ElementosTienda)
        {
            GameObject gameObject = Instantiate(StoreCard, Vector2.zero, Quaternion.identity, Content);
            gameObject.transform.GetChild(0).GetChild(0).gameObject.GetComponent<Image>().sprite = ElementoTienda.Image;
            gameObject.transform.GetChild(0).GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = ElementoTienda.Precio + "$";
            gameObject.GetComponent<CartaObjetoManager>().Elemento = ElementoTienda.Nombre;
            gameObject.GetComponent<CartaObjetoManager>().valor = ElementoTienda.Precio;
        }
    }

    private void Update()
    {
        cantidadCarton.text = Carton.ToString();
        cantidadPapel.text = Papel.ToString();
        cantidadVidrio.text = Vidrio.ToString();
        cantidadMetal.text = Metal.ToString();
    }
}
