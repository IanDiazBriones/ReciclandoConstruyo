using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scriptactividaduno : MonoBehaviour
{
    public Image UIimagen;


    int NumeroAleatorio(){
    	 
   		 int numero = UnityEngine.Random.Range(1,3);
   		 
   		 return (numero);

    }

    void Start()
    {
        UIimagen = GameObject.Find("ImagenCambiante").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("q"))
        {
        	print("Sprites/"+NumeroAleatorio().ToString());
        	UIimagen.sprite = Resources.Load<Sprite>("Sprites/"+NumeroAleatorio().ToString());
        }

    }
}
