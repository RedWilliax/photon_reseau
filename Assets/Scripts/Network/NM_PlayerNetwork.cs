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
            stream.SendNext(localMovement.x);
            stream.SendNext(localMovement.y);
            stream.SendNext(localMovement.z);
        }
        else
        {
            localMovement.x = (float)stream.ReceiveNext();
            localMovement.y = (float)stream.ReceiveNext();
            localMovement.z = (float)stream.ReceiveNext();
        }
    }


}
