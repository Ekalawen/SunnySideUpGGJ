using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudAnim : MonoBehaviour
{
    Animator m_anim;
    GameObject m_wind;

    // Start is called before the first frame update
    void Start()
    {
        m_anim = GetComponentInChildren<Animator>();
        m_wind = GameObject.FindWithTag("Wind");
    }

    // Update is called once per frame
    void Update()
    {
    }

    internal void MoveCloudsStart()
    {
        m_anim.SetTrigger("StartCloud");
        this.gameObject.SetActive(true);
        m_wind.SetActive(true);
    }

    public void TriggerStopWind(float theValue)
    {
        Debug.Log("Stop wind");
        m_wind.SetActive(false);
    }
}
