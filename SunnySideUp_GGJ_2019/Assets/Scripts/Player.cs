using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 10.0f;
    private float speed_mod = 0.0f;
    public float jump_force = 12.0f;
    private float jump_force_mod = 0.0f;
    public float jump_time = 0.5f;
    public float jump_coef = 2.8f;

    public float gravity = -9.81f;

    Vector3 forces;
    Rigidbody m_Rigidbody;
    CharacterController controller;
    [HideInInspector]
    public Inventaire inventaire;
    private GameManager gameManager;

    private bool blockMove;

    //Pour les modificateur de vitesse
    public float real_speed()
    {
        return (speed + speed_mod);
    }
    public void addToSpeedMod(float mod)
    {
        speed_mod += mod;
    }
    public void set_speed_mod(float mod)
    {
        speed_mod = mod;
    }

    //Pour la force du saut
    public float real_jump()
    {
        return (jump_force + jump_force_mod);
    }
    public void addToJumpMod(float mod)
    {
        jump_force_mod += mod;
    }
    public void set_jump_mod(float mod)
    {
        jump_force_mod = mod;
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
        float translation = Input.GetAxis("Horizontal");
        forces = new Vector3();
        translation *= Time.deltaTime * real_speed();

        forces.x += translation;
        if (ShouldAllowYAxis())
        {
            forces.y += Input.GetAxis("Vertical") * Time.deltaTime * real_speed();
        }
        

        if (!blockMove)
        {
            if (ShouldApplyGravity())
            {
                forces.y += gravity * Time.deltaTime;
            }
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
        foreach(Collider c in Physics.OverlapSphere(transform.position, 2.0f))
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
            controller.Move(new Vector3(0, real_jump() * Time.deltaTime*jump_coef*remaining_time,0));
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
