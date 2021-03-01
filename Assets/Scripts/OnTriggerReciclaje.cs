using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class OnTriggerReciclaje : MonoBehaviour
{
    // Start is called before the first frame update
    public string NombreElemento;
    public string Colorbasurero;
    public bool IsElementoReciclable;
    [SerializeField]
    private GameObject Player;
    public Animator animatorFelicitacion;
    public Animator animatorIntentar;
    public GameObject BDController;
    public int elementoId;
    public int tipo;
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        BDController = GameObject.FindWithTag("BDController");
    }

    // Update is called once per frame
    void Update()
    {
        if (Player == null)
        {
            Player = GameObject.FindWithTag("Player");
        }

    }

    void OnCollisionEnter2D(Collision2D coll) 
    {
        
        if (IsElementoReciclable)
        {
            
            if(!coll.gameObject.GetComponent<Inventario>().ReturnCarringObject())
            {



                this.gameObject.GetComponent<PhotonView>().TransferOwnership(PhotonNetwork.LocalPlayer);
                coll.gameObject.GetComponent<Inventario>().TomarObjeto(NombreElemento, Colorbasurero, elementoId, tipo);
                this.gameObject.SetActive(false);
            }
        } else
        {
            if(coll.gameObject.GetComponent<Inventario>().ReturnCarringObject())
            {
                if(coll.gameObject.GetComponent<Inventario>().ReturnCurrentBasurero() == Colorbasurero)
                {
                    coll.gameObject.GetComponent<Inventario>().ReciclarObjeto();
                    animatorFelicitacion.Play("FadeText", -1, 0f);
                    GameObject.Find("PanelMonedas").GetComponent<Puntaje>().AñadirMonedas(500);
                } else 
                {
                    animatorIntentar.Play("FadeText", -1, 0f);
                    coll.gameObject.GetComponent<Inventario>().ReciclarObjetoIncorrecto();
                }
            }
        }
    }
}
