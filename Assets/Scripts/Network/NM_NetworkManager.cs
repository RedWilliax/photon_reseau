﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NM_NetworkManager : MonoBehaviourPunCallbacks
{
    [SerializeField] string nameServ = "1";
    //
    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        RoomOptions _roomSettings = new RoomOptions()
        {
            MaxPlayers = 7
        };
        PhotonNetwork.JoinOrCreateRoom("O3D", _roomSettings, new TypedLobby("O3D", LobbyType.Default));
    }
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log("Joined room O3D");
        PhotonView _id = PhotonNetwork.Instantiate("Player", Vector3.zero, Quaternion.identity).GetPhotonView();
        _id.name = _id.ViewID.ToString();
    }
    //
    void Connect()
    {
        PhotonNetwork.PhotonServerSettings.AppSettings.AppVersion = "2.3";
        PhotonNetwork.ConnectUsingSettings();
    }
    //
    private void OnGUI()
    {
        GUILayout.Box(PhotonNetwork.NetworkClientState.ToString());
        GUILayout.Box(PhotonNetwork.IsMasterClient.ToString());
        GUILayout.Box(PhotonNetwork.CurrentRoom?.Name);
        GUILayout.Box($"{PhotonNetwork.CurrentRoom?.PlayerCount}/{PhotonNetwork.CurrentRoom?.MaxPlayers}");
        if (GUILayout.Button("Join"))
            Connect();
    }
}
