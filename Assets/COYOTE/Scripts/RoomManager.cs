using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class RoomManager : MonoBehaviourPunCallbacks
{
    public TextMeshProUGUI lbl_NumOfPlayers;
    #region Unity Methods
    void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        if (!PhotonNetwork.IsConnectedAndReady)
        {
            PhotonNetwork.ConnectUsingSettings();
        }
        else{
            PhotonNetwork.JoinLobby();
        }
    }
    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to servers again.");
        PhotonNetwork.JoinLobby();
    }
    public override void OnJoinedLobby()
    {
        Debug.Log("Joined the Lobby");
    }
    #endregion
    #region UI Callback Methods
    public void JoinRandomRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }
    #endregion

    #region Photon Callback Methods
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log(message);
        CreateAndJoinRoom();
    }
    public override void OnCreatedRoom()
    {
        Debug.Log("A room is created with name: "+PhotonNetwork.CurrentRoom.Name);
    }
    public override void OnJoinedRoom()
    {
        Debug.Log("The local player: " + PhotonNetwork.NickName + " joined to " + PhotonNetwork.CurrentRoom.Name + ". Player count " + PhotonNetwork.CurrentRoom.PlayerCount);
        PhotonNetwork.LoadLevel("GameScene");
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("Player count: " + PhotonNetwork.CurrentRoom.PlayerCount);
    }
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        if (roomList.Count == 0)
        {
            lbl_NumOfPlayers.text = "0 / 20";
        }
        else
        {
            lbl_NumOfPlayers.text = roomList[0].PlayerCount + " / 20";
        }
    }
    #endregion

    void CreateAndJoinRoom()
    {
        string randomRoomName = "Room_" + Random.Range(0, 10000);
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 20;

        PhotonNetwork.CreateRoom(randomRoomName, roomOptions);
    }

}
