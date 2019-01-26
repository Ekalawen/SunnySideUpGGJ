using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetRessource : Interactible {

    public enum TypeRessource { BOIS, FER };

    public GameObject ressource;
    public TypeRessource type;
    public int quantite;

    private Player player;

    // Start is called before the first frame update
    void Start() {
        ressource.SetActive(true);
    }

    public override void Interact() {
        base.Interact();

        Miner();
    }

    void Miner() {
        if (quantite > 0) {
            player.inventaire.Add(type, 1);
            quantite--;
            Debug.Log("Inventaire = " + player.inventaire.Get(type));
        }
    }

}
