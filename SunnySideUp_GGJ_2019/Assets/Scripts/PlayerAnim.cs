using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    Animator m_anim;

    // Start is called before the first frame update
    void Start()
    {
        m_anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    internal void FallFromTop()
    {
        m_anim.SetTrigger("FallFromTop");
    }
}
