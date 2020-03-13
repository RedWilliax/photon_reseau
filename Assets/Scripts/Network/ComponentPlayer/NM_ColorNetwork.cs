using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class NM_ColorNetwork
{
    GameObject owner = null;

    [SerializeField] Material material = null;

    public Color color = Color.white;

    public NM_ColorNetwork(GameObject owner)
    {
        this.owner = owner;

        material = owner.GetComponent<Renderer>().material;

        material.SetColor("_Color", new Color(1, 0, 0));
    }

    public void OnLocalColor()
    {
        color = material.GetColor("_Color");
    }

    public void OnLineColor()
    {
        material.SetColor("_Color", color);
    }


}
