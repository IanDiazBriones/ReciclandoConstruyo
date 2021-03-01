using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleMensajeTerremoto : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject MensajeTerremoto;
    public static bool ShowMensajeTerremoto;
    void Start()
    {
        if (ShowMensajeTerremoto)
        {
            MensajeTerremoto.SetActive(true);
        } else
        {
            MensajeTerremoto.SetActive(false);
        }
            
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setMensajeTerremoto(bool mensaje)
    {
        ShowMensajeTerremoto = mensaje;
    }
}
