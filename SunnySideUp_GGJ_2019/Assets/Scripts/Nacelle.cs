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
        // Aller !
        Debug.Log("sliderNacelle");
        Transform t = player.transform.parent;
        player.transform.SetParent(transform);
        player.setBlockMove(true);
        player.transform.position = positionPlayer.position;
        while(Vector3.Distance( end.transform.position, transform.position) > 0.6f)
        {
            direction = Vector3.Normalize(end.transform.position - transform.position);
            CharacterController cc = GetComponent<CharacterController>();
            cc.Move(vitesse * direction * Time.deltaTime);
            player.transform.position = positionPlayer.position;
            yield return null;
        }
        player.transform.SetParent(t);
        player.setBlockMove(false);

        // Retour !
        while(Vector3.Distance( start.transform.position, transform.position) > 0.6f)
        {
            direction = Vector3.Normalize(start.transform.position - transform.position);
            CharacterController cc = GetComponent<CharacterController>();
            cc.Move(vitesse * direction * Time.deltaTime);
            yield return null;
        }

        bIsMoving = false;
    }
}
