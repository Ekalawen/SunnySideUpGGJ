using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nacelle : Interactible
{
    public GameObject start, end;
    public float vitesse;

    private Vector3 direction;
    private Player player;
    

    // Start is called before the first frame update
    void Start()
    {

        player = FindObjectOfType<Player>();

        //transform.position = start.transform.position - new Vector3(0.0f, 1.0f, 0.0f);
        direction = Vector3.Normalize(end.transform.position - start.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Interact()
    {
        Debug.Log("Interact");
        base.Interact();
        Debug.Log("Interact1");
        StartCoroutine(sliderNacelle());
        Debug.Log("Interact3");

    }

    IEnumerator sliderNacelle()
    {
        Debug.Log("sliderNacelle");
        Transform t = player.transform;
        player.transform.SetParent(transform);
        player.setBlockMove(true);
        float deplacementLineaire = 0.0f;
        Vector3 vecDepLin = Vector3.Normalize(new Vector3(1.0f, 0.0f, 0.0f) * direction[0]);
        while(Vector3.Distance( end.transform.position, transform.position) > 0.6f)
        {
            CharacterController cc = GetComponent<CharacterController>();
            //Debug.Log("" + Vector3.Distance(end.transform.position, transform.position));
            if (deplacementLineaire < 2)
            {                
                cc.Move(vitesse * direction * Time.deltaTime);
            }
            cc.Move(vitesse * direction * Time.deltaTime);
            yield return null;
        }
        player.transform.SetParent(t);
        player.setBlockMove(false);        
    }
}
