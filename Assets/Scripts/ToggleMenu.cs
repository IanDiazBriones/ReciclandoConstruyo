using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject anim;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FadeMenu(bool variable)
    {
        if (variable)
        {
            anim.GetComponent<Animator>().SetBool("BotonPresionado", true);
        }
        else
        {
            anim.GetComponent<Animator>().SetBool("BotonPresionado", true);
        }
    }
}
