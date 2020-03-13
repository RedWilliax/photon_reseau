using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NM_RotationMovment
{

    [SerializeField] float speedOfRotation = 20;

    GameObject owner = null;

    Vector3 localRotation = Vector3.zero;

    public NM_RotationMovment(GameObject owner)
    {
        this.owner = owner;
    }

    public void OnLocalRotation()
    {
        localRotation = owner.transform.eulerAngles;
    }

    public void OnLineRotation()
    {
        localRotation = Vector3.MoveTowards(owner.transform.eulerAngles, localRotation, Time.deltaTime * speedOfRotation);
    }

    public void Send(PhotonStream stream)
    {
        SendElement(stream, localRotation.x);
        SendElement(stream, localRotation.y);
        SendElement(stream, localRotation.z);
    }

    public void Receive(PhotonStream stream)
    {
        ReceiveElement(stream, ref localRotation.x);
        ReceiveElement(stream, ref localRotation.y);
        ReceiveElement(stream, ref localRotation.z);
    }


    void SendElement<T>(PhotonStream stream, T _element) => stream.SendNext(_element);
    void ReceiveElement<T>(PhotonStream stream, ref T _element) => _element = (T)stream.ReceiveNext();
}
