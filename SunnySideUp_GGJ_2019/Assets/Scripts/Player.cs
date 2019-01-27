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
    public GameObject stepBox;
    [HideInInspector]
    public Inventaire inventaire;
    private GameManager gameManager;
    private float stepTimer;

    private bool blockMove;

    private void Step(float translation) {
        float pistonPos;
        stepTimer += translation;
        stepTimer = stepTimer % 100000;

        pistonPos = Mathf.Round(Mathf.PingPong(stepTimer, 10.0f)) / 20.0f - 0.25f;

        Vector3 newPos = stepBox.transform.localPosition;
        newPos.y = pistonPos;
        stepBox.transform.localPosition = newPos;

//        stepBox.transform.position.y = pistonPos;
//stepBox.transform.Translate(new Vector3(0.0f,Mathf.PingPong(stepTimer,0.5f),0.0f));
        //Debug.Log("Move: " + translation + " | Piston: " + pistonPos + " | BoxPos: " + stepBox.transform.localPosition.y);
    }

    // Start is called before the first frame update
    void Start()
    {
        blockMove = false;
        gameManager = FindObjectOfType<GameManager>();
        inventaire = new Inventaire();

        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!controller.enabled)
            return;

        // Prévention pour ne pas se faire pousser ^^
        Vector3 pos = transform.position;
        pos.z = 0.0f;
        transform.position = pos;

        if (!blockMove)
        {
            float translation = Input.GetAxis("Horizontal");
            forces = new Vector3();
            translation *= Time.deltaTime * speed;

            forces.x += translation;
            if (ShouldAllowYAxis())
            {
                forces.y += Input.GetAxis("Vertical") * Time.deltaTime * speed;
            }

            if (ShouldApplyGravity())
            {
                forces.y += gravity * Time.deltaTime;
            }
            if (controller.isGrounded && Input.GetButtonDown("Jump"))
            {
                StartCoroutine(Jump());
            }

            controller.Move(forces);
            Step(translation);
        }

        if (Input.GetButtonDown("Interact"))
        {
            Interaction();
        }         
    }

    bool ShouldApplyGravity() {
        foreach(Collider c in Physics.OverlapBox(transform.position, new Vector3(0.1f, 0.1f, 3)))
        {
            ObjectConstructible o = c.gameObject.GetComponent<ObjectConstructible>();
            if (o != null && o.estConstruit() && o.no_gravity)
            {
                return false;
            }
        }
        return true;
    }

    bool ShouldAllowYAxis()
    {
        foreach (Collider c in Physics.OverlapBox(transform.position, new Vector3(0.1f, 0.1f, 3)))
        {
            ObjectConstructible o = c.gameObject.GetComponent<ObjectConstructible>();
            if (o != null && o.estConstruit() && o.y_axis)
            {
                return true;
            }
        }
        return false;
    }

    void Interaction() {
        List<Interactible> interactibles = new List<Interactible>();
        foreach(Collider c in Physics.OverlapSphere(transform.position, 2.5f))
        {
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
