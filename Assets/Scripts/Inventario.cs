using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Inventario : MonoBehaviour
{
    // Start is called before the first frame update
	[SerializeField]
    private bool CarringObject = false;
    [SerializeField]
    private string CurrentObject, CurrentBasurero;
    private PhotonView photonView;
    [SerializeField]
    private int CurrentObjectID;
    public Animator animator;
    public GameObject Botella;
    public GameObject Cajita;
    public GameObject Lata;
    public GameObject BotellaBebida;
    public GameObject FrascoVidrio;
    public GameObject BDController;
    public int CurrentZona = 0;
    private int tipoobjeto = 0;

    void Start()
    {
        Botella = GameObject.Find("BotellaFlotando");
        Cajita = GameObject.Find("CajitaFlotando");
        Lata = GameObject.Find("LataFlotando");
        BotellaBebida = GameObject.Find("BotellaBebidaFlotando");
        FrascoVidrio = GameObject.Find("FrascoVidrioFlotando");

        BDController = GameObject.Find("BDController");
        photonView = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public bool ReturnCarringObject()
    {
    	return CarringObject;
    }

    public int ReturnIdCarringObject()
    {
        return CurrentObjectID;
    }

    public void TomarObjeto(string objeto, string basurero, int idobject, int tipo)
    {
        tipoobjeto = tipo;
        if (PhotonNetwork.IsConnected)
        {
            if (photonView.IsMine)
            {
                Debug.Log(photonView.IsMine);
                CarringObject = true;
                CurrentObject = objeto;
                CurrentBasurero = basurero;
                CurrentObjectID = idobject;

                BDController.GetComponent<ConexionBD>().ElementoReciclado(CurrentObjectID, 9008, 2, CurrentZona, tipo);

                animator.SetBool("IsHoldingObject", true);
                switch (CurrentObject)
                {
                    case "Botella":
                        Botella.GetComponent<SpriteRenderer>().enabled = true;
                        break;
                    case "Cajita":
                        Cajita.GetComponent<SpriteRenderer>().enabled = true;
                        break;
                    case "Botella Bebida":
                        BotellaBebida.GetComponent<SpriteRenderer>().enabled = true;
                        break;
                    case "Lata":
                        Lata.GetComponent<SpriteRenderer>().enabled = true;
                        break;
                    case "Frasco Vidrio":
                        FrascoVidrio.GetComponent<SpriteRenderer>().enabled = true;
                        break;
                }
            }
        } else
        {
            CarringObject = true;
            CurrentObject = objeto;
            CurrentBasurero = basurero;
            CurrentObjectID = idobject;

            BDController.GetComponent<ConexionBD>().ElementoReciclado(CurrentObjectID, 9008, 2, CurrentZona, tipo);

            animator.SetBool("IsHoldingObject", true);
            switch (CurrentObject)
            {
                case "Botella":
                    Botella.GetComponent<SpriteRenderer>().enabled = true;
                    break;
                case "Cajita":
                    Cajita.GetComponent<SpriteRenderer>().enabled = true;
                    break;
                case "Botella Bebida":
                    BotellaBebida.GetComponent<SpriteRenderer>().enabled = true;
                    break;
                case "Lata":
                    Lata.GetComponent<SpriteRenderer>().enabled = true;
                    break;
                case "Frasco Vidrio":
                    FrascoVidrio.GetComponent<SpriteRenderer>().enabled = true;
                    break;
            }
        }
        
    }

    public string ReturnCurrentBasurero()
    {
    	
    	return CurrentBasurero;
    }

    public void ReciclarObjeto()
    {
    	switch (CurrentObject)
        {
            case "Botella":
    		    Botella.GetComponent<SpriteRenderer>().enabled = false;
                break;
    	    case "Cajita":
    		    Cajita.GetComponent<SpriteRenderer>().enabled = false;
                break;
            case "Botella Bebida":
                BotellaBebida.GetComponent<SpriteRenderer>().enabled = false;
                break;
            case "Lata":
                Lata.GetComponent<SpriteRenderer>().enabled = false;
                break;
            case "Frasco Vidrio":
                FrascoVidrio.GetComponent<SpriteRenderer>().enabled = false;
                break;


        }
        BDController.GetComponent<ConexionBD>().ElementoReciclado(CurrentObjectID, 9008, 1, CurrentZona, tipoobjeto);
        CarringObject = false;
    	CurrentObject = "";
    	CurrentBasurero = "";
        CurrentObjectID = 0;
        animator.SetBool("IsHoldingObject", false);
        

    }

    public void ReciclarObjetoIncorrecto()
    {
        
        BDController.GetComponent<ConexionBD>().ElementoReciclado(CurrentObjectID, 9008, 0, CurrentZona, tipoobjeto);
        


    }

}
