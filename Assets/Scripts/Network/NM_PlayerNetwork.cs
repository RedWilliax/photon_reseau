using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NM_PlayerNetwork : MonoBehaviour, IPunObservable
{
    [SerializeField] PhotonView myID = null;

    [SerializeField] NM_RotationMovment rotation = null;

    [SerializeField] NM_MovementNetwork movement = null;

    [SerializeField] NM_ScaleNetwork scale = null;

    [SerializeField] NM_ColorNetwork color = null;


    string nickName = "DefaultName";

    bool IsValid => movement != null && rotation != null && scale != null;

    void Start()
    {
        movement = new NM_MovementNetwork(gameObject);
        rotation = new NM_RotationMovment(gameObject);
        scale = new NM_ScaleNetwork(gameObject);
        color = new NM_ColorNetwork(gameObject);
        nickName = myID.name;

    }

    void Update()
    {
        if (!IsValid) return;

        if (myID.IsMine)
        {
            movement.OnLocalMovement();
            rotation.OnLocalRotation();
            scale.OnLocalScale();
            color.OnLocalColor();
        }
        else
        {
            movement.OnOnlineMovement();
            rotation.OnLineRotation();
            scale.OnLineScale();
            color.OnLineColor();
        }
    }


    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            SendElement(stream, movement.localMovement.x);
            SendElement(stream, movement.localMovement.y);
            SendElement(stream, movement.localMovement.z);

            SendElement(stream, rotation.localRotation.x);
            SendElement(stream, rotation.localRotation.y);
            SendElement(stream, rotation.localRotation.z);
            SendElement(stream, rotation.localRotation.w);

            SendElement(stream, scale.localScale.x);
            SendElement(stream, scale.localScale.y);
            SendElement(stream, scale.localScale.z);

            SendElement(stream, color.color.r);
            SendElement(stream, color.color.g);
            SendElement(stream, color.color.b);
            SendElement(stream, color.color.a);

        }

        else
        {
            ReceiveElement(stream, ref movement.localMovement.x);
            ReceiveElement(stream, ref movement.localMovement.y);
            ReceiveElement(stream, ref movement.localMovement.z);

            ReceiveElement(stream, ref rotation.localRotation.x);
            ReceiveElement(stream, ref rotation.localRotation.y);
            ReceiveElement(stream, ref rotation.localRotation.z);
            ReceiveElement(stream, ref rotation.localRotation.w);

            ReceiveElement(stream, ref scale.localScale.x);
            ReceiveElement(stream, ref scale.localScale.y);
            ReceiveElement(stream, ref scale.localScale.z);

            ReceiveElement(stream, ref color.color.r);
            ReceiveElement(stream, ref color.color.g);
            ReceiveElement(stream, ref color.color.b);
            ReceiveElement(stream, ref color.color.a);

        }
    }


    public void SendElement<T>(PhotonStream stream, T _element) => stream.SendNext(_element);

    public void ReceiveElement<T>(PhotonStream stream, ref T _element) => _element = (T)stream.ReceiveNext();

}
