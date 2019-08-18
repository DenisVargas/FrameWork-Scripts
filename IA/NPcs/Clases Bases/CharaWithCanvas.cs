﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaWithCanvas : MonoBehaviour
{
	/*
	Este script permite situar una barra de vida sobre un personaje
	Lo usamos para que los npc tengan una barra de vida visible.
	*/
    public Transform life;

    // Use this for initialization
    void Start()
    {
        life = this.transform.FindChild("Image");
        life.parent = GameObject.Find("Canvas").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = RectTransformUtility.WorldToScreenPoint(Camera.main, this.transform.position);
        life.transform.position = pos + Vector3.up * 100;
    }
}
