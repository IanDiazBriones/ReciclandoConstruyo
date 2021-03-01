using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class OverlapScript : MonoBehaviour
{
    // Start is called before the first frame update
    // 1 Triangulo, 2 cuadrado, 3 triangulo lateral
    public GameObject Obj1;
    public GameObject Obj2;
    public GameObject Mensaje;
    public TMP_Text texto;
    public GameObject LlaveCompleta;
    public GameObject Simetria;
    public GameObject Triangle;
    public GameObject Square;
    public GameObject TriangleLeft;
    public GameObject ButtonRetroceder;
    public AudioSource UnlockSound;
    [SerializeField]
    public static List<int> MovimientosRealizados;
    GameObject BDController;
    bool Win = false;
    Animator Anim;
    bool llavecorrecta = false;
    int movement = 5;
    int lastmovement = 5;
    int id_elemento = 0;

    void Start()
    {
    	MovimientosRealizados = new List<int>();
        Debug.Log(PickKeyScript.GetKeyAssigned());
        Obj1 = GameObject.FindWithTag("Key");
        Win = false;
        switch (PickKeyScript.GetKeyAssigned())
        {
        case 3:
            TriangleLeft.SetActive(true);
            Obj2 = GameObject.Find("TriangleLeftKeyHole");
            LlaveCompleta = GameObject.Find("TriangleLeftLlaveCompleta");
            LlaveCompleta.GetComponent<Image>().enabled = false;
            Simetria = GameObject.Find("TriangleLeftSimetria");
                id_elemento = 290016;
            break;
        case 2:
            Square.SetActive(true);
            Obj2 = GameObject.Find("SquareKeyHole");
            LlaveCompleta = GameObject.Find("SquareLlaveCompleta");
            LlaveCompleta.GetComponent<Image>().enabled = false;
            Simetria = GameObject.Find("SquareSimetria");
                id_elemento = 290015;
                break;
        case 1:
            Triangle.SetActive(true);
            Obj2 = GameObject.Find("TriangleKeyHole");
            LlaveCompleta = GameObject.Find("TriangleLlaveCompleta");
            LlaveCompleta.GetComponent<Image>().enabled = false;
            Simetria = GameObject.Find("TriangleSimetria");
                id_elemento = 290014;
                break;
        default:
            Triangle.SetActive(true);
            Obj2 = GameObject.Find("TriangleKeyHole");
            LlaveCompleta = GameObject.Find("TriangleLlaveCompleta");
            LlaveCompleta.GetComponent<Image>().enabled = false;
            Simetria = GameObject.Find("TriangleSimetria");
                id_elemento = 290014;
                break;
        }

        if(PickKeyScript.GetKey() == PickKeyScript.GetKeyAssigned())
        {
        	llavecorrecta = true;
        }

        BDController = GameObject.Find("BDController");
    }

    // Update is called once per frame
    void Update()
    {
    	
    	
  
        if (!Win){
        	
            if (llavecorrecta) {

            	if (Input.acceleration.y > 0 && Input.acceleration.x > 0)
            	{
            		movement = 9;
            	} else if (Input.acceleration.y > 0 && Input.acceleration.x < 0)
            	{
            		movement = 7;
            	} else if (Input.acceleration.y < 0 && Input.acceleration.x > 0)
            	{
            		movement = 3;
            	} else if (Input.acceleration.y > 0 && Input.acceleration.x < 0)
            	{
            		movement = 1;
            	}else if (Input.acceleration.y > 0)
            	{
            		movement = 8;
            	}else if (Input.acceleration.y < 0)
            	{
            		movement = 2;
            	}else if (Input.acceleration.x < 0)
            	{
            		movement = 6;
            	}else if (Input.acceleration.x > 0)
            	{
            		movement = 4;
            	}else
            	{
            		movement = 5;
            	}

            	if (movement != 5 && movement != lastmovement){
            		lastmovement = movement;
                    switch (movement)
                    {
                    case 9:
                        BDController.GetComponent<ConexionBD>().InsertMovement(1,1,2,9007, id_elemento, System.DateTime.Now .ToString("yyyy-MM-dd HH:mm:ss.fff"));
                        break;
                    case 8:
                        BDController.GetComponent<ConexionBD>().InsertMovement(0,1,2,9007, id_elemento, System.DateTime.Now .ToString("yyyy-MM-dd HH:mm:ss.fff"));
                        break;
                    case 7:
                        BDController.GetComponent<ConexionBD>().InsertMovement(-1,1,2,9007, id_elemento, System.DateTime.Now .ToString("yyyy-MM-dd HH:mm:ss.fff"));
                        break;
                    case 6:
                        BDController.GetComponent<ConexionBD>().InsertMovement(1,0,2,9007, id_elemento, System.DateTime.Now .ToString("yyyy-MM-dd HH:mm:ss.fff"));
                        break;
                    case 4:
                        BDController.GetComponent<ConexionBD>().InsertMovement(-1,0,2,9007, id_elemento, System.DateTime.Now .ToString("yyyy-MM-dd HH:mm:ss.fff"));
                        break;
                    case 3:
                        BDController.GetComponent<ConexionBD>().InsertMovement(1,-1,2,9007, id_elemento, System.DateTime.Now .ToString("yyyy-MM-dd HH:mm:ss.fff"));
                        break;
                    case 2:
                        BDController.GetComponent<ConexionBD>().InsertMovement(0,-1,2,9007, id_elemento, System.DateTime.Now .ToString("yyyy-MM-dd HH:mm:ss.fff"));
                        break;
                    case 1:
                        BDController.GetComponent<ConexionBD>().InsertMovement(-1,-1,2,9007, id_elemento, System.DateTime.Now .ToString("yyyy-MM-dd HH:mm:ss.fff"));
                        break;
                    }
                }




             	Debug.Log(Vector3.Distance(Obj1.transform.position, Obj2.transform.position));
            	if(Obj1.transform.rotation.eulerAngles.z >= -5 && Obj1.transform.rotation.eulerAngles.z <= 5)
                {
                    if(Vector3.Distance(Obj1.transform.position, Obj2.transform.position) < 2f){
                        
                        Win = true;
                        AfterWin();
                        

                    }

                } 
            }
        }
           
            
    }

    public void AfterWin()
    {
        TimerControl.StopTime();
        Obj1.SetActive(false);
        Obj2.SetActive(false);
        Simetria.SetActive(false);
        Mensaje.SetActive(true);
        BDController.GetComponent<ConexionBD>().InsertMovement(0,0,1,9007, id_elemento, System.DateTime.Now .ToString("yyyy-MM-dd HH:mm:ss.fff"));
        LlaveCompleta.GetComponent<Image>().enabled = true;
        Anim = LlaveCompleta.GetComponent<Animator>();
        Anim.Play("KeyRotate");
        UnlockSound.Play();
        KeyControlScript.setMoveAllowedToFalse();
        texto.text = "¡Lo has conseguido!, ya podemos acceder al edificio.";
        ButtonRetroceder.SetActive(false);
        BDController.GetComponent<ConexionBD>().InsertFinActividadAsync();
    }

    public void InsertRotation(int elemento)
    {
        BDController.GetComponent<ConexionBD>().InsertMovement(0,0,2,9007,elemento,System.DateTime.Now .ToString("yyyy-MM-dd HH:mm:ss.fff"));
    }

    public void OnApplicationQuit()
    {
        if(!Win){
            SetActivityasFailed();
        }
    }

    public void OnApplicationPause()
    {
        if (!Win)
        {
            SetActivityasFailed();
        }
    }

    public void SetActivityasFailed(){
        switch (PickKeyScript.GetKeyAssigned())
        {
            case 3:
                id_elemento = 290016;
                break;
            case 2:
                id_elemento = 290015;
                break;
            case 1:
                id_elemento = 290014;
                break;
            default:
                id_elemento = 290014;
                break;
        }



        BDController.GetComponent<ConexionBD>().InsertFinActividadAsync();
        BDController.GetComponent<ConexionBD>().InsertMovement(0,0,0,9007, id_elemento, System.DateTime.Now .ToString("yyyy-MM-dd HH:mm:ss.fff"));
    }



}


