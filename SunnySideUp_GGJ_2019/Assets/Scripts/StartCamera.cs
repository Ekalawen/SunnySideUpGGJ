using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCamera : MonoBehaviour
{
    GameObject m_player;
    GameObject m_clouds;
    GameObject m_wind;
    Camera m_playerCamera;
    Camera m_startCamera;
    Animator m_anim;
    GameObject m_startButton;

    // Start is called before the first frame update
    void Start()
    {
        m_player = GameObject.FindWithTag("Player");
        m_clouds = GameObject.FindWithTag("Clouds");
        m_wind = GameObject.FindWithTag("Wind");

        m_clouds.SetActive(false);
        m_wind.SetActive(false);

        m_playerCamera = m_player.GetComponentInChildren<Camera>();
        m_playerCamera.enabled = false;

        m_anim = GetComponentInChildren<Animator>();

        m_startCamera = GetComponentInChildren<Camera>();
        m_startCamera.enabled = true;

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
        m_clouds.SetActive(true);
        m_wind.SetActive(true);
        CloudAnim cloudAnim = m_clouds.GetComponentInChildren<CloudAnim>();
        cloudAnim.MoveCloudsStart();
    }

    public void TriggerPlayerFallFromTop(float theValue)
    {
        Debug.Log("Fall from top");
        m_anim.StopPlayback();
        m_anim.enabled = false;
        m_startCamera.transform.parent = m_player.transform;
        m_startCamera.transform.localPosition = Vector3.zero;
        PlayerAnim playerAnim = m_player.GetComponentInChildren<PlayerAnim>();
        playerAnim.FallFromTop();
    }
}
