using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectConstructible : Interactible
{

    public enum Etat { NON_CONSTRUIT, CONSTRUIT };

    public GameObject constructible;
    public GameObject construction;
    public Price price;
    public Player player;

    public int priceBois;
    public int pricePierre;

    public float distanceVisibilitePrix;

    private Etat etat;

    // Start is called before the first frame update
    void Start() {
        player = FindObjectOfType<Player>();

        // Mettre dans le bon état
        SetEtat(Etat.NON_CONSTRUIT);

        // Mettre à jour les prix
        price.SetupPrix(priceBois, pricePierre);
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
        // Update l'affichage du prix, ne s'affiche que si le joueur est assez proche
        if (etat == Etat.NON_CONSTRUIT) {
            float distance = Vector3.Distance(player.gameObject.transform.position, transform.position);
            if (distance <= distanceVisibilitePrix) {
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

        if(peutConstruire()) {
            // débitter le player
            /// TODO

            // Puis on peut construire :)
            Construire();
        }
    }

    // Pour construire l'objet
    public void Construire() {
        // Faire payer !

        // Afficher les bons éléments
        constructible.SetActive(false);
        construction.SetActive(true);
    }

    // Permet de savoir si l'on peut construire cette construction
    public bool peutConstruire() {
        return true;
    }
}
