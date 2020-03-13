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

    public Vector3 localRotation = Vector3.zero;

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
        owner.transform.eulerAngles = Vector3.MoveTowards(owner.transform.eulerAngles, localRotation, Time.deltaTime * speedOfRotation);
    }

}
