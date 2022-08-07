using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime; // we need Realtime directive for creating room

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

        // when player is connected we join any rroom available
        PhotonNetwork.JoinRandomRoom();


    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("[Disconnected]: " + cause);
    }

    // callbacks for creating andd joining room

    // on player enters room
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("Player entered in the room");
    }

    // when player left room
    public override void OnPlayerLeftRoom(Player otherPLayer)
    {
        Debug.Log("Player left the room");
    }

    // when player join room 
    public override void OnJoinedRoom()
    {
        Debug.Log("player join room");
    }

    // in this condition if player on master client left the room
    // it means player who created the room has left the room

    // public override void OnLeftRooom()
    // {
    //     Debug.Log("player on master client has been left the room");
    // }


    // when player trying to join any random room but failed to join room
    // if there is no room exist then player fails to join room
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("PLayer Failed to join random room" + message);

        // here we pass rroom name as parameter
        createRoom("room2001");
    }

    //  when player trying to join room but failed to join room
    // in this condition player knows what room to join
    // all room name is listed in list 

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        // here if user fails to room
        // then we create a new room and 
        // make player join the created room

        Debug.Log("Player Failed to join room" + message);
    }

    // when player try to create room but fails to create room
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to create room" + message);
    }

    // when player created room
    public override void OnCreatedRoom()
    {
        Debug.Log("Player Created Room" + PhotonNetwork.CurrentRoom.Name);
        // when room is created we spawn player into game
        // we called method and passed player object name in parameter
        SpawnPlayerInGame(player.name);
    }

    #endregion

    #region public method

    public void SpawnPlayerInGame(string playerPrefabName)
    {
        // PhotonNetwork.Instantiate("player prefab name", "player initial position", "player initial rotation");

        // spawning player in game scene
        PhotonNetwork.Instantiate(playerPrefabName, playerPosition.position, Quaternion.identity);
    }

    // here we create a method and will call this method
    // to create a room
    /**
        we pass room as a string in method/function parameters
    **/
    public void createRoom(string roomName)
    {
        /**
            1. roomName is passed through method parameter
            2. options
            3. here type will be null
        **/

        RoomOptions roomOptions = new RoomOptions();
        // room options
        roomOptions.MaxPlayers = 2;
        roomOptions.IsVisible = true;

        // here to create a room we called metthod CreateRoom()
        // with parameters room_name and room_options
        PhotonNetwork.CreateRoom(roomName, roomOptions, null);
    }
    #endregion
}
