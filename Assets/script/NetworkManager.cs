using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NetworkManager : MonoBehaviourPunCallbacks
{

    // player prefab/object
    public GameObject player;
    // player fix position
    public Transform playerPosition;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Script runing..");

        // connecting to photon sevrer
        PhotonNetwork.ConnectUsingSettings();
    }

    #region Photon callbacks

    public override void OnConnected()
    {
        Debug.Log("Connected to Internet");
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected To Photon Server");
        // after connecting to server we just 
        // spawn player in the game scene

        // we called method and passed player object name in parameter
        SpawnPlayerInGame(player.name);
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("[Disconnected]: " + cause);
    }

    #endregion

    #region public method

    public void SpawnPlayerInGame(string playerPrefabName)
    {
        // PhotonNetwork.Instantiate("player prefab name", "player initial position", "player initial rotation");

        // spawning player in game scene
        PhotonNetwork.Instantiate(playerPrefabName, playerPosition.position, Quaternion.identity);
    }

    #endregion
}
