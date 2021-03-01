using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using EZCameraShake;

public class ButtonTerremoto : MonoBehaviour
{
    public GameObject Canvas;
    public GameObject LoadLevel;
    public GameObject TerremotoSound;
    // Start is called before the first frame update
    void Start()
    {
        Canvas = GameObject.Find("Canvas");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Terremoto ()
    {
        File.WriteAllText(Application.persistentDataPath + "/save.txt", "");
        CameraShaker.Instance.StartShake(4f, 3f, 10f);
        StartCoroutine(Vibrate());
        TerremotoSound.SetActive(true);
        Canvas.GetComponent<ToggleMensajeTerremoto>().setMensajeTerremoto(true);
        LoadLevel.GetComponent<AnimationLevelLoader>().LoadNextLevelTerremoto("TownScene");
        

    }

    public void Reiniciar()
    {
        File.WriteAllText(Application.persistentDataPath + "/save.txt", "");
        LoadLevel.GetComponent<AnimationLevelLoader>().LoadNextLevel("TownScene");

    }


    private IEnumerator Vibrate()
    {



        for (int t = 0; t <= 20; t += 1) // Change the end condition (t < 1) if you want
        {
            Handheld.Vibrate();
            yield return new WaitForSeconds(0.5f);
        }

       
    }

    
}
