using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ButtonReturnMulti : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisconnectFromServer()
    {
        PhotonNetwork.Disconnect();
        Destroy(GameObject.Find("RoomManager"));
    }
}
