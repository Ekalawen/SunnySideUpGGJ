using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 15.0f;
    public float jump_force = 12.0f;
    public float jump_time = 0.5f;

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
        
        // Make it move 10 meters per second instead of 10 meters per frame...
        translation *= Time.deltaTime * speed;

        // Move translation along the object's z-axis
        //transform.Translate(0, 0, translation);
        forces.z += translation;
        forces.y += gravity*Time.deltaTime;

        controller.Move(forces);

        if (controller.isGrounded && Input.GetButtonDown("Jump"))
        {
            StartCoroutine(Jump());
            Debug.Log("hey"     );

            //m_Rigidbody.AddForce(new Vector3(0, jump_force, 0));
        }

        if (Input.GetButtonDown("Interact"))
        {
            Debug.Log("Interaction");
        }


    }

    IEnumerator Jump()
    {
        float finish_time = Time.time + jump_time;
        while (Time.time < finish_time)
        {
            controller.Move(new Vector3(0,jump_force*Time.deltaTime,0));
            yield return null;
        }
    }
}
