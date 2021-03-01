using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimerControl : MonoBehaviour
{
    private static float timeLeft = 999;
    public GameObject TimerGameObject;
    private static bool TimeRunning = false;
    public GameObject Events;
     
  
    void Update()
    {
    	if (TimeRunning){
    	if(timeLeft > 0)
    	{
        	timeLeft -= Time.deltaTime;
        	TimerGameObject.GetComponent<TMPro.TextMeshProUGUI>().text = ""+System.Convert.ToInt32(timeLeft);
        }
        if(timeLeft < 0)
        {
            Events.GetComponent<OverlapScript>().SetActivityasFailed();
            UnityEngine.SceneManagement.SceneManager.LoadScene ("Seleccionar Actividad Menu");
        }
        }
    }

    public static void SetTimer(float timer){
    	timeLeft = timer;
    }

    public static float GetLeftTime(){
    	return timeLeft;
    }

    public static void StartTime(){
    	TimeRunning = true;
    }

    public static void StopTime(){
    	TimeRunning = false;
    }
}
