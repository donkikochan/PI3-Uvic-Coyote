using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    GameObject GenericVRPlayerPrefab;
    public GameObject[] points;
    public Vector3 spawnPosition;
   
    // Start is called before the first frame update
    void Start()
    {
        int index = 0;
        if (PhotonNetwork.IsConnectedAndReady)
        {
            Debug.Log("PhotonNetwork.IsConnectedAndReady");
            index = PhotonNetwork.PlayerList.Length;
            Debug.Log("[SpawnManager] Num of players: " + index);
            spawnPosition.x = points[index-1].transform.position.x;
            spawnPosition.y = points[index-1].transform.position.y;
            spawnPosition.z = points[index-1].transform.position.z;
            PhotonNetwork.Instantiate(GenericVRPlayerPrefab.name, spawnPosition, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

  
}
