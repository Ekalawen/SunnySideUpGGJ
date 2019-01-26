using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5.0f;
    public float jump_force = 250.0f;

    Rigidbody m_Rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Rigidbody.constraints = RigidbodyConstraints.FreezePositionZ;
        m_Rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
    }

    // Update is called once per frame
    void Update()
    {
        float translation = Input.GetAxis("Horizontal");
        
        // Make it move 10 meters per second instead of 10 meters per frame...
        translation *= Time.deltaTime * speed;

        // Move translation along the object's z-axis
        transform.Translate(0, 0, translation);

        if (Input.GetButtonDown("Jump"))
        {
            m_Rigidbody.AddForce(new Vector3(0, jump_force, 0));
        }

        if (Input.GetButtonDown("Interact"))
        {
            Debug.Log("Interaction");
        }
    }
}
