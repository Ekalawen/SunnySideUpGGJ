using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    Animator m_anim;
    Camera m_playerCamera;
    Camera m_startCamera;
    CharacterController m_characterController;

    // Start is called before the first frame update
    void Start()
    {
        m_anim = GetComponentInChildren<Animator>();
        m_playerCamera = GetComponentInChildren<Camera>();
        m_startCamera = GameObject.Find("Start Camera").GetComponentInChildren<Camera>();
        m_characterController = GetComponentInChildren<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    internal void FallFromTop()
    {
        m_anim.SetTrigger("FallFromTop");
        m_characterController.enabled = false;
    }

    internal void ArrivedAtBottom()
    {
        m_characterController.enabled = true;
        m_startCamera.enabled = false;
        m_playerCamera.enabled = true;
        m_anim.enabled = false;
    }
}
