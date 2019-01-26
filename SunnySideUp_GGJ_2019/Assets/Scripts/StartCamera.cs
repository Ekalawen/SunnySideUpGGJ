﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCamera : MonoBehaviour
{
    public Camera m_mainCamera;

    GameObject m_player;
    Camera m_startCamera;
    Animator m_anim;

    // Start is called before the first frame update
    void Start()
    {
        m_player = GameObject.FindWithTag("Player");
        m_player.SetActive(false);

        m_anim = GetComponentInChildren<Animator>();

        m_startCamera = GetComponentInChildren<Camera>();
        m_startCamera.enabled = true;

        m_mainCamera.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {   
    }

    internal void PlayStartAnim()
    {
        m_anim.SetTrigger("Start");
    }
}
