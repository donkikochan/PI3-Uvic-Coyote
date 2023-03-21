using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class VR_GameManager : MonoBehaviourPunCallbacks
{
    #region Photon Callback Methods
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("Player count: " + PhotonNetwork.CurrentRoom.PlayerCount);
    }
    #endregion
}
