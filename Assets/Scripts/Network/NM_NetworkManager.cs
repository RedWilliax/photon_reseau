using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NM_NetworkManager : MonoBehaviourPunCallbacks
{
    [SerializeField] string nameServ = "1";
    //

    [SerializeField] string nickName = "DefaultName";

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

        if (_id.IsMine)
            _id.name = nickName;

    }
    //
    void Connect()
    {
        PhotonNetwork.PhotonServerSettings.AppSettings.AppVersion = nameServ;
        PhotonNetwork.ConnectUsingSettings();
    }
    //
    private void OnGUI()
    {
        GUILayout.Box(PhotonNetwork.NetworkClientState.ToString());
        GUILayout.Box(PhotonNetwork.IsMasterClient.ToString());
        GUILayout.Box(PhotonNetwork.CurrentRoom?.Name);
        GUILayout.Box($"{PhotonNetwork.CurrentRoom?.PlayerCount}/{PhotonNetwork.CurrentRoom?.MaxPlayers}");

        nickName = GUILayout.TextField(nickName);

        if (GUILayout.Button("Join"))
            Connect();

        
    }
}
