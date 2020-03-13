using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NM_PlayerNetwork : MonoBehaviour, IPunObservable
{
    [SerializeField] PhotonView myID = null;

    NM_RotationMovment rotation = null;

    void Start()
    {
        rotation = new NM_RotationMovment(gameObject);

        name = myID.ViewID.ToString();
        
    }

    void Update()
    {
        if(myID.IsMine)
        {
            rotation.OnLocalRotation();

        }
        else 
        {
            rotation.OnLineRotation();

        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.IsWriting)
        {

            rotation.Send(stream);
            
        }

        else
        {

        }
    }

}
