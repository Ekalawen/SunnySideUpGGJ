using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 10.0f;
    public float jump_force = 12.0f;
    public float jump_time = 0.5f;
    public float jump_coef = 2.8f;

    public float gravity = -9.81f;

    Vector3 forces;
    Rigidbody m_Rigidbody;
    CharacterController controller;

    // Start is called before the first frame update
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        controller = GetComponent<CharacterController>();
        m_Rigidbody.constraints = RigidbodyConstraints.FreezePositionX;
        m_Rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
    }

    // Update is called once per frame
    void Update()
    {
        forces = new Vector3();

        float translation = Input.GetAxis("Horizontal");
        
        translation *= Time.deltaTime * speed;
        
        forces.x += translation;
        forces.y += gravity*Time.deltaTime;

        controller.Move(forces);

        if (controller.isGrounded && Input.GetButtonDown("Jump"))
        {
            StartCoroutine(Jump());
        }

        if (Input.GetButtonDown("Interact"))
        {
            Debug.Log("Interaction");
        }


    }

    IEnumerator Jump()
    {
        float remaining_time = jump_time;
        while (remaining_time > 0.0f)
        {
            controller.Move(new Vector3(0,jump_force*Time.deltaTime*jump_coef*remaining_time,0));
            remaining_time -= Time.deltaTime;
            yield return null;
        }
    }
}
