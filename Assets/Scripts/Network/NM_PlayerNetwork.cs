using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NM_PlayerNetwork : MonoBehaviour, IPunObservable
{
    [SerializeField] PhotonView myID = null;
    Vector3 localMovement = Vector3.zero;

    [SerializeField] NM_MovementNetwork movement;

    bool IsValid => movement != null;

    void Start()
    {
        movement = new NM_MovementNetwork(gameObject);
        name = myID.ViewID.ToString();
    }

    void Update()
    {
        if (!IsValid) return;

        if(myID.IsMine)
            movement.OnLocalMovement();
        else 
            movement.OnOnlineMovement();
    }

   
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.IsWriting)
        {
            movement.Send(stream);
        }
        else
        {

            movement.Receive(stream);
        }
    }

    
}
