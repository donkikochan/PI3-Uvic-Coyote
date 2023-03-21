using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class LoginManager :MonoBehaviourPunCallbacks
{
    public TMP_InputField PlayerName;
    #region Unity Methods
    // Start is called before the first frame update
    void Start()
    {
        //PhotonNetwork.ConnectUsingSettings();
    }
    #endregion

    #region UI Callback Methods
    public void ConnectAnonymously()
    {
        PhotonNetwork.ConnectUsingSettings();
    }
    public void ConnectWithName()
    {
        if (PlayerName != null)
        {
            PhotonNetwork.NickName = PlayerName.text;
            PhotonNetwork.ConnectUsingSettings();
        }
        
    }


    #endregion
    #region Photon Callback Methods
    public override void OnConnected()
    {
        Debug.Log("OnConnected is called. The server is available!");
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("OnConnected to Master Server with player name: " + PhotonNetwork.NickName);
    }
    #endregion
}
