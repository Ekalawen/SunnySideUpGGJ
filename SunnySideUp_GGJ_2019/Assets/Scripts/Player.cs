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
    [HideInInspector]
    public Inventaire inventaire;

    private bool blockMove;

    // Start is called before the first frame update
    void Start()
    {
        /*
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Rigidbody.constraints = RigidbodyConstraints.FreezePositionX;
        m_Rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
        */
        blockMove = false;

        inventaire = new Inventaire();

        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float translation = Input.GetAxis("Horizontal");
        forces = new Vector3();
        translation *= Time.deltaTime * speed;

        forces.x += translation;

        if (!blockMove)
        {
            forces.y += gravity * Time.deltaTime;
            if (controller.isGrounded && Input.GetButtonDown("Jump"))
            {
            StartCoroutine(Jump());
            }
        }
        


        controller.Move(forces);

        if (Input.GetButtonDown("Interact"))
        {
            Interaction();
        }         
    }

    void Interaction()
    {
        Debug.Log("------------------");
        List<Interactible> interactibles = new List<Interactible>();
        foreach(Collider c in Physics.OverlapBox(transform.position, new Vector3(1, 1, 3)))
        {
            Debug.Log(c.gameObject.name);

            if (c.gameObject.GetComponent<Interactible>() != null)
            {
                interactibles.Add(c.gameObject.GetComponent<Interactible>());
            }
        }

        float distMin = float.PositiveInfinity;
        Interactible interactionFinale = null;
        if(interactibles.Count > 0) {
            for (int i = 0; i < interactibles.Count; i++) {
                float dist = Vector3.Distance(transform.position, interactibles[i].gameObject.transform.position);
                Debug.Log(interactibles[i].gameObject.name + " dist = " + dist);
                if (dist < distMin)
                {
                    distMin = dist;
                    interactionFinale = interactibles[i];
                }
            }
            if(interactionFinale != null) {
                interactionFinale.Interact();
            }
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

    public void teleportCharacter(Transform t)
    {
        controller.transform.position = t.position;
    }

    public void setBlockMove(bool etat)
    {
        blockMove = etat;
    }
}
