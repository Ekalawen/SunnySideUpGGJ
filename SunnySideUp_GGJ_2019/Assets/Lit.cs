using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lit : Interactible
{

    private GameManager gameManager;

    // Start is called before the first frame update
    void Start() {
        gameManager = FindObjectOfType<GameManager>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Interact() {
        base.Interact();

        if (gameManager.heure == GameManager.Heure.NUIT || gameManager.heure == GameManager.Heure.CREPUSCULE) {
            gameManager.ChangerHeure(GameManager.Heure.JOUR);
        }
    }
}
