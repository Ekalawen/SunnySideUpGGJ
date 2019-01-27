using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTrigger : MonoBehaviour
{
    public AudioSource audioSrce;

    // Start is called before the first frame update
    void Start()
    {
        audioSrce = GameObject.FindObjectOfType<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject)
        {
            audioSrce.Play();
            Debug.Log("ouch!");
        }
    }

    void OnCollisionExit(Collision col)
    {
        if (col.gameObject)
        {
            audioSrce.Play();
            Debug.Log("ouch!");
        }
    }
}
