using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NM_PlayerNetwork : MonoBehaviour, IPunObservable
{
    [SerializeField] PhotonView myID = null;
    Vector3 localMovement = Vector3.zero;

    void Start()
    {
        name = myID.ViewID.ToString();
    }

    void Update()
    {
        if(myID.IsMine)
            OnLocalMovement();
        else 
            OnOnlineMovement();
    }

    void OnLocalMovement()
    {
        localMovement = transform.position;
    }
    void OnOnlineMovement()
    {
        transform.position = Vector3.MoveTowards(transform.position, localMovement, Time.deltaTime * 20);
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.IsWriting)
        {
            SendElement(stream, localMovement.x);
            SendElement(stream, localMovement.y);
            SendElement(stream, localMovement.z);
        }
        else
        {
            ReceiveElement(stream, ref localMovement.x);
            ReceiveElement(stream, ref localMovement.y);
            ReceiveElement(stream, ref localMovement.z);
        }
    }

    void SendElement<T>(PhotonStream stream, T _element) => stream.SendNext(_element);
    void ReceiveElement<T>(PhotonStream stream, ref T _element) => _element = (T)stream.ReceiveNext();


}
