using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicaManager : MonoBehaviour
{
    private GameObject Musica;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToogleMusic ()
    {
        Musica = GameObject.FindGameObjectWithTag("Musica");
        if (Musica.GetComponent<AudioSource>().isPlaying)
        {
            Musica.GetComponent<MusicBg>().IsPaused = true;
            Musica.GetComponent<AudioSource>().Pause();
        } else
        {
            Musica.GetComponent<MusicBg>().IsPaused = false;
            Musica.GetComponent<AudioSource>().Play();
        }
            
    }
}
