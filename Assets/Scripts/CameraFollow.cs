using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	public Transform Player;
    public GameObject PlayerGameobject;

    void Start()
    {
        PlayerGameobject = GameObject.FindWithTag("Player");
        if (PlayerGameobject != null)
        {
            Player = PlayerGameobject.transform;
        }
    }

    private void Update()
    {
        if (PlayerGameobject == null)
        {
            PlayerGameobject = GameObject.FindWithTag("Player");
            if (PlayerGameobject != null)
            {
                Player = PlayerGameobject.transform;
            }
        }
    }

    void FixedUpdate() 
    {
        if (PlayerGameobject != null)
        {
            transform.position = new Vector3(Player.position.x, Player.position.y, transform.position.z);
        }
    }
}
