using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTirolienne : Interactible
{

    private Player player;
    public ObjectConstructible construction;
    public GameObject EndTirolienne;

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

        if (construction.estConstruit())
        {
            player.teleportCharacter(EndTirolienne.transform);
        }
    }
}
