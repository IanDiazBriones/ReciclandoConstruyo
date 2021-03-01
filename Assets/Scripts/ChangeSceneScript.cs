using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ChangeSceneScript : MonoBehaviour
{
    public string scenetoload;
    public GameObject BDController;
    int id_elemento;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LoadScene(string myString)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(myString, LoadSceneMode.Single);

    }

    public void PickKey(int valor)
    {
        PickKeyScript.SetKey(valor);

    }

    public void RandomKeyAssigned()
    {
        PickKeyScript.RandomKeyAssigned();

    }

    public void SetTimerScene(float timer)
    {
        TimerControl.SetTimer(timer);
        TimerControl.StartTime();

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



        BDController.GetComponent<ConexionBD>().InsertMovement(0,0,0,9007, id_elemento, System.DateTime.Now .ToString("yyyy-MM-dd HH:mm:ss.fff"));
    }

}
