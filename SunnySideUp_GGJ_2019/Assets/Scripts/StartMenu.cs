using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenu : MonoBehaviour
{
    public GameObject m_startCamera;
    public GameObject image;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnStartClick()
    {
        // Launch start anim
        Debug.Log("start pressed");
        StartCamera camera = m_startCamera.GetComponent<StartCamera>();
        camera.PlayStartAnim();
        image.SetActive(false);
    }
}
