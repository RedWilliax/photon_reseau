using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System;

[Serializable]
public class NM_RotationMovment
{
    [SerializeField] float speedOfRotation = 100;

    GameObject owner = null;

    public Quaternion localRotation = Quaternion.identity;

    public NM_RotationMovment(GameObject owner)
    {
        this.owner = owner;
    }

    public void OnLocalRotation()
    {
        localRotation = owner.transform.rotation;
    }

    public void OnLineRotation()
    {
        owner.transform.rotation = Quaternion.RotateTowards(owner.transform.rotation, localRotation, Time.deltaTime * speedOfRotation);
    }

}
