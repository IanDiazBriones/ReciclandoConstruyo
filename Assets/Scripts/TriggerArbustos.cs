﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerArbustos : MonoBehaviour
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

        col.gameObject.GetComponent<Inventario>().CurrentZona = 2;
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        col.gameObject.GetComponent<Inventario>().CurrentZona = 0;
    }
}
