using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nacelle : Interactible
{
    public GameObject start, end;
    public float vitesse;
    public Transform positionPlayer;

    private Vector3 direction;
    private Player player;
    private bool bIsMoving = false;

    

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
        base.Interact();
        if (!bIsMoving)
        {
            StartCoroutine(sliderNacelle());
        }
    }

    IEnumerator sliderNacelle()
    {
        bIsMoving = true;
        // L'allez
        Debug.Log("sliderNacelle");
        Transform t = player.transform.parent;
        player.transform.SetParent(transform);
        player.setBlockMove(true);
        player.transform.position = positionPlayer.position;
        while(Vector3.Distance( end.transform.position, transform.position) > 0.6f)
        {
            CharacterController cc = GetComponent<CharacterController>();
            cc.Move(vitesse * direction * Time.deltaTime);
            yield return null;
        }
        player.transform.SetParent(t);
        player.setBlockMove(false);

        // Retour !
        while(Vector3.Distance( start.transform.position, transform.position) > 0.6f)
        {
            CharacterController cc = GetComponent<CharacterController>();
            cc.Move(vitesse * direction * Time.deltaTime * -1);
            yield return null;
        }

        bIsMoving = false;
    }
}
