﻿using System.Collections;
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

    bool IsValid => movement != null && rotation != null && scale != null;

    void Start()
    {
        movement = new NM_MovementNetwork(gameObject);
        rotation = new NM_RotationMovment(gameObject);
        scale = new NM_ScaleNetwork(gameObject);
        name = myID.ViewID.ToString();

    }

    void Update()
    {
        if (!IsValid) return;

        if (myID.IsMine)
        {
            movement.OnLocalMovement();
            rotation.OnLocalRotation();
            scale.OnLocalScale();
        }
        else
        {
            movement.OnOnlineMovement();
            rotation.OnLineRotation();
            scale.OnLineScale();
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

            SendElement(stream, scale.localScale.x);
            SendElement(stream, scale.localScale.y);
            SendElement(stream, scale.localScale.z);

        }

        else
        {
            ReceiveElement(stream, ref movement.localMovement.x);
            ReceiveElement(stream, ref movement.localMovement.y);
            ReceiveElement(stream, ref movement.localMovement.z);

            ReceiveElement(stream, ref rotation.localRotation.x);
            ReceiveElement(stream, ref rotation.localRotation.y);
            ReceiveElement(stream, ref rotation.localRotation.z);

            ReceiveElement(stream, ref scale.localScale.x);
            ReceiveElement(stream, ref scale.localScale.y);
            ReceiveElement(stream, ref scale.localScale.z);

        }
    }


    public void SendElement<T>(PhotonStream stream, T _element) => stream.SendNext(_element);

    public void ReceiveElement<T>(PhotonStream stream, ref T _element) => _element = (T)stream.ReceiveNext();

}
