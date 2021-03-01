using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UrlManager : MonoBehaviour
{
    public string web1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Ulearnet()
    {
        Application.OpenURL(web1);
    }

    
}
