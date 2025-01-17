﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectConstructible : Interactible
{

    public enum Etat { NON_CONSTRUIT, CONSTRUIT };

    public GameObject constructible;
    public GameObject construction;
    public Price price;

    public int priceBois;
    public int priceFer;

    public float distanceVisibilitePrix;

    private Etat etat;
    private Player player;
    private GameManager gameManager;
    private AudioSource audioSource;

    public bool no_gravity = false;
    public bool y_axis = false;

    // Start is called before the first frame update
    void Start() {
        player = FindObjectOfType<Player>();
        gameManager = FindObjectOfType<GameManager>();
        audioSource = GetComponent<AudioSource>();

        // Mettre dans le bon état
        SetEtat(Etat.NON_CONSTRUIT);

        // Mettre à jour les prix
        price.SetupPrix(priceBois, priceFer);
    }

    public void SetEtat(Etat newEtat) {
        etat = newEtat;
        if (etat == Etat.NON_CONSTRUIT) {
            constructible.SetActive(true);
            construction.SetActive(false);
        } else {
            constructible.SetActive(false);
            construction.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update() {
        if (!player || !player.enabled)
            return;

        // Update l'affichage du prix, ne s'affiche que si le joueur est assez proche
        if (etat == Etat.NON_CONSTRUIT) {
            float distance = Vector3.Distance(player.gameObject.transform.position, transform.position);
            if (distance <= distanceVisibilitePrix && gameManager.heure != GameManager.Heure.NUIT) {
                price.gameObject.SetActive(true);
            } else {
                price.gameObject.SetActive(false);
            }
        } else {
            price.gameObject.SetActive(false);
        }
    }

    public override void Interact() {
        base.Interact();

        if (gameManager.heure != GameManager.Heure.NUIT)
        {
            if (peutConstruire())
            {
                // débitter le player
                player.inventaire.Use(ObjetRessource.TypeRessource.BOIS, priceBois);
                player.inventaire.Use(ObjetRessource.TypeRessource.FER, priceFer);

                // Puis on peut construire :)
                Construire();

                audioSource.Play();
            }
        }
    }

    // Pour construire l'objet
    public void Construire() {
        // Afficher les bons éléments
        constructible.SetActive(false);
        construction.SetActive(true);

        etat = Etat.CONSTRUIT;

        // On desactive le collider de ce script pour permettre aux objets suivants d'etre interactibles !
        if(!no_gravity && ! y_axis)
            gameObject.GetComponent<BoxCollider>().enabled = false;
    }

    // Permet de savoir si l'on peut construire cette construction
    public bool peutConstruire() {
        return etat == Etat.NON_CONSTRUIT
        && player.inventaire.CanUse(ObjetRessource.TypeRessource.BOIS, priceBois)
        && player.inventaire.CanUse(ObjetRessource.TypeRessource.FER, priceFer);
    }

    //permet de savoir si un objetConstructible et construit
    public bool estConstruit()
    {
        return etat == Etat.CONSTRUIT;
    }
}
