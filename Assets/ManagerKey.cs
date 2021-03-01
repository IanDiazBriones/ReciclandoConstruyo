using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerKey : MonoBehaviour
{
    public GameObject Triangle;
    public GameObject Square;
    public GameObject TriangleLeft;
    // Start is called before the first frame update
    void Start()
    {

        
        switch (PickKeyScript.GetKeyAssigned())
        {
            case 3:
                TriangleLeft.SetActive(true);
                break;
            case 2:
                Square.SetActive(true);
                
                break;
            case 1:
                Triangle.SetActive(true);
                
                break;
            default:
                Triangle.SetActive(true);
                
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
