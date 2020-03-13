using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NM_PlayerNetwork : MonoBehaviour, IPunObservable
{
    [SerializeField] PhotonView myID = null;

    NM_RotationMovment rotation = null;

    [SerializeField] NM_MovementNetwork movement;

    bool IsValid => movement != null && rotation != null;

    void Start()
    {
        movement = new NM_MovementNetwork(gameObject);
        rotation = new NM_RotationMovment(gameObject);
        name = myID.ViewID.ToString();
        
    }

    void Update()
    {
        if (!IsValid) return;

        if(myID.IsMine)
        {
            movement.OnLocalMovement();
            rotation.OnLocalRotation();

        }
        else 
        {
            rotation.OnLineRotation();
            movement.OnOnlineMovement();
        }
    }


    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.IsWriting)
        {

            movement.Send(stream);
            rotation.Send(stream);
        }

        else
        {
            rotation.Receive(stream);
            movement.Receive(stream);
        }
    }

    

        
    


}
