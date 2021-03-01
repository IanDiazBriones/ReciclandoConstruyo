using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    [HideInInspector]
    public int Carton = 100, Papel = 100, Vidrio = 100, Metal = 100;

    public TextMeshProUGUI cantidadCarton;
    public TextMeshProUGUI cantidadPapel;
    public TextMeshProUGUI cantidadVidrio;
    public TextMeshProUGUI cantidadMetal;
    // Start is called before the first frame update
    void Start()
    {
        Carton = PlayerPrefs.GetInt("Carton", 0);
        Papel = PlayerPrefs.GetInt("Papel", 0);
        Vidrio = PlayerPrefs.GetInt("Vidrio", 0);
        Metal = PlayerPrefs.GetInt("Metal", 0);
        


    }

    // Update is called once per frame
    private void Update()
    {
        cantidadCarton.text = Carton.ToString();
        cantidadPapel.text = Papel.ToString();
        cantidadVidrio.text = Vidrio.ToString();
        cantidadMetal.text = Metal.ToString();
    }
}
