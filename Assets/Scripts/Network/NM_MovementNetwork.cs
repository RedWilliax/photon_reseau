using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System;

[Serializable]
public class NM_MovementNetwork
{
    [SerializeField] float speedOfMovement = 20;

    public Vector3 localMovement = Vector3.zero;

    GameObject owner; 

    public NM_MovementNetwork(GameObject _owner)
    {
        owner = _owner;
    }

    public void OnLocalMovement()
    {
        localMovement = owner.transform.position;
    }

    public void OnOnlineMovement()
    {
        owner.transform.position = Vector3.MoveTowards(owner.transform.position, localMovement, Time.deltaTime * 20);
    }

}
