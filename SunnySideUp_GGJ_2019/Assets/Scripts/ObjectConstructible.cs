using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectConstructible : MonoBehaviour
{

    public GameObject constructible;
    public GameObject construction;
    public Price price;

    public int priceBois;
    public int pricePierre;

    // Start is called before the first frame update
    void Start() {
        // Afficher les bons éléments
        constructible.SetActive(true);
        price.gameObject.SetActive(true);
        construction.SetActive(false);

        // Mettre à jour les prix
        price.SetupPrix(priceBois, pricePierre);
    }

    // Update is called once per frame
    void Update() {
    }

    // Pour construire l'objet
    public void Construire() {
        // Faire payer !

        // Afficher les bons éléments
        constructible.SetActive(false);
        price.gameObject.SetActive(false);
        construction.SetActive(true);
    }

    // Permet de savoir si l'on peut construire cette construction
    public bool peutConstruire() {
        return true;
    }
}
