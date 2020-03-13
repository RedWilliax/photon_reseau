using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;


public class NM_MovementNetwork 
{
    public Vector3 localMovement = Vector3.zero;

    GameObject owner; 

    public Vector3 LocalMovement => localMovement;

    public NM_MovementNetwork(GameObject _owner)
    {
        owner = _owner;
    }

    public void OnLocalMovement()
    {
        localMovement = owner.transform.position;
    }


    public void OnOnlineMovement()
    {
        localMovement = Vector3.MoveTowards(owner.transform.position, localMovement, Time.deltaTime * 20);
    }


    public void Send(PhotonStream _stream)
    {
        SendElement(_stream, localMovement.x);
        SendElement(_stream, localMovement.y);
        SendElement(_stream, localMovement.z);
    }

    public void Receive(PhotonStream _stream)
    {
        ReceiveElement(_stream, ref localMovement.x);
        ReceiveElement(_stream, ref localMovement.y);
        ReceiveElement(_stream,ref localMovement.z);
    }

    public void SendElement<T>(PhotonStream stream, T _element) => stream.SendNext(_element);

    public void ReceiveElement<T>(PhotonStream stream, ref T _element) => _element = (T)stream.ReceiveNext();
}
