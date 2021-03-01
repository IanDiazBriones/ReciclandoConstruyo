    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using System.Linq;

public class Lobby : MonoBehaviourPunCallbacks
{
    public static Lobby Instance;
    // Start is called before the first frame update
    [SerializeField] TMP_Text RoomTitle;
    [SerializeField] TMP_Text errorText;
    [SerializeField] Transform roomListContent;
    [SerializeField] Transform PlayerListContent;
    [SerializeField] GameObject roomListPrefab;
    [SerializeField] GameObject PlayerNamePrefab;
    [SerializeField] GameObject StartButton;
    GameObject BDController;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        BDController = GameObject.FindGameObjectWithTag("BDController");
    }
    void OnPhotonMaxCcuReached()
    {
        MenuManager.Instance.OpenMenu("MaxCcu");

    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public override void OnJoinedLobby()
    {
        MenuManager.Instance.OpenMenu("title");

        PhotonNetwork.NickName = BDController.GetComponent<ConexionBD>().getNombre();
    }

    public void CreateRoom()
    {
        PhotonNetwork.JoinOrCreateRoom("Sala de "+PhotonNetwork.NickName, new RoomOptions { MaxPlayers = 4 }, TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        RoomTitle.text = PhotonNetwork.CurrentRoom.Name;
        MenuManager.Instance.OpenMenu("RoomMenu");

        foreach(Transform child in PlayerListContent)
        {
            Destroy(child.gameObject);
        }

        Player[] players = PhotonNetwork.PlayerList;
        for (int i = 0; i < players.Count(); i++)
        {
            Instantiate(PlayerNamePrefab, PlayerListContent).GetComponentInChildren<PlayerNamePrefab>().SetUp(players[i]);
        }

        StartButton.SetActive(PhotonNetwork.IsMasterClient);
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        StartButton.SetActive(PhotonNetwork.IsMasterClient);
    }


    public void StartGame()
    {
        PhotonNetwork.CurrentRoom.IsVisible = false;
        PhotonNetwork.CurrentRoom.IsOpen = false;
        PhotonNetwork.LoadLevel("Actividad 3 Multi");
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        MenuManager.Instance.OpenMenu("ErrorMenu");
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        MenuManager.Instance.OpenMenu("title");
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach(Transform trans in roomListContent)
        {
            Destroy(trans.gameObject);
        }
        for(int i = 0; i < roomList.Count; i++)
        {
            if (roomList[i].RemovedFromList)
                continue;
            Instantiate(roomListPrefab, roomListContent).GetComponentInChildren<RoomPrefab>().SetUp(roomList[i]);
        }
    }

    public void JoinRoom(RoomInfo info)
    {
        PhotonNetwork.JoinRoom(info.Name);
        MenuManager.Instance.OpenMenu("loading");

        
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    { 
        if (PhotonNetwork.PlayerList.Count() == PhotonNetwork.CurrentRoom.MaxPlayers)
        {
            if (PhotonNetwork.LocalPlayer.IsMasterClient == false)
                return;
            PhotonNetwork.CurrentRoom.IsOpen = false;
            PhotonNetwork.CurrentRoom.IsVisible = false;
            
        }
        Instantiate(PlayerNamePrefab, PlayerListContent).GetComponentInChildren<PlayerNamePrefab>().SetUp(newPlayer);
    }

    
}
