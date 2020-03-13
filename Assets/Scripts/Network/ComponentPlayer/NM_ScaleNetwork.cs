using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class NM_ScaleNetwork
{
    [SerializeField] float speedOfScale = 20;

    GameObject owner = null;

    public Vector3 localScale = Vector3.zero;

    public NM_ScaleNetwork(GameObject owner)
    {
        this.owner = owner;
    }

    public void OnLocalScale()
    {
        localScale = owner.transform.localScale;
    }

    public void OnLineScale()
    {
        owner.transform.localScale = Vector3.MoveTowards(owner.transform.localScale, localScale, Time.deltaTime * speedOfScale);
    }


}
