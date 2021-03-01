using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonGeneral : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SendInfoToBD(string ID)
    {
    	ConexionBD.SetidButtonPressed(ID);
    }
}
