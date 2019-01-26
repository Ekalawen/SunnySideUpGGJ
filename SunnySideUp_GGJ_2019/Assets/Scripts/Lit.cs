using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lit : Interactible
{
    public Text text;

    private GameManager gameManager;

    // Start is called before the first frame update
    void Start() {
        gameManager = FindObjectOfType<GameManager>();
        
    }

    // Update is called once per frame
    void Update()
    {
        text.gameObject.SetActive(CanBeUsed());
    }

    bool CanBeUsed() {
        return gameManager.heure == GameManager.Heure.NUIT || gameManager.heure == GameManager.Heure.CREPUSCULE;
    }

    public override void Interact() {
        base.Interact();

        if (CanBeUsed()) {
            gameManager.ChangerHeure(GameManager.Heure.JOUR);
        }
    }
}
