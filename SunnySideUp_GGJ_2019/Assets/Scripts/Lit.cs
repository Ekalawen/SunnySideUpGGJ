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

        switch(gameManager.heure)
        {
            case GameManager.Heure.NUIT:
                gameManager.ChangerHeure(GameManager.Heure.AUBE); break;
            case GameManager.Heure.JOUR:
                gameManager.ChangerHeure(GameManager.Heure.CREPUSCULE); break;
            case GameManager.Heure.AUBE:
                gameManager.ChangerHeure(GameManager.Heure.JOUR); break;
            case GameManager.Heure.CREPUSCULE:
                gameManager.ChangerHeure(GameManager.Heure.NUIT); break;
        }
    }
}
