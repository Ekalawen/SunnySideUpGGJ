using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCamera : MonoBehaviour
{
    public Camera m_mainCamera;

    GameObject m_player;
    Camera m_playerCamera;
    Camera m_startCamera;
    Animator m_anim;
    GameObject m_startButton;

    // Start is called before the first frame update
    void Start()
    {
        m_player = GameObject.FindWithTag("Player");
        m_playerCamera = m_player.GetComponentInChildren<Camera>();
        m_playerCamera.enabled = false;

        m_anim = GetComponentInChildren<Animator>();

        m_startCamera = GetComponentInChildren<Camera>();
        m_startCamera.enabled = true;

        m_mainCamera.enabled = false;

        m_startButton = GameObject.Find("Start");
    }

    // Update is called once per frame
    void Update()
    {   
    }

    internal void PlayStartAnim()
    {
        m_anim.SetTrigger("StartCamera");
        m_startButton.SetActive(false);
    }

    public void TriggerPlayerFallFromTop(float theValue)
    {
        Debug.Log("Fall from top");
        PlayerAnim playerAnim = m_player.GetComponentInChildren<PlayerAnim>();
        playerAnim.FallFromTop();
    }
}
