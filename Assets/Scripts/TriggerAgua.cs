using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAgua : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        col.gameObject.GetComponent<Animator>().SetBool("IsInWater", true);
        col.gameObject.GetComponent<Inventario>().CurrentZona = 1;
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        col.gameObject.GetComponent<Animator>().SetBool("IsInWater", false);
        col.gameObject.GetComponent<Inventario>().CurrentZona = 0;
    }

}
