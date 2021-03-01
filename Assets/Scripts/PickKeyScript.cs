using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickKeyScript : MonoBehaviour
{
    // Start is called before the first frame update
    // 1 Triangulo, 2 cuadrado, 3 triangulo lateral
    public static int KeySelected = 0;
    public static int KeyAssigned = 0;
    public GameObject Key1;
    public GameObject Key2;
    public GameObject Key3;

    void Awake()
    {
    	switch (KeySelected)
        {
        case 3:
            Key3.SetActive(true);
            break;
        case 2:
			Key2.SetActive(true);
            break;
        case 1:
			Key1.SetActive(true);
            break;
        default:
			Key1.SetActive(true);
            break;
        }

        if (KeyAssigned == 0)
        {
			KeyAssigned = 1;
			KeySelected = 1;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void SetKey(int key){
    	KeySelected = key;
    }

    public static int GetKey(){
    	return KeySelected;
    }

    public static void RandomKeyAssigned(){
    	 KeyAssigned = Random.Range(1, 4);
    }

    public static int GetKeyAssigned(){
    	return KeyAssigned;
    }
}
