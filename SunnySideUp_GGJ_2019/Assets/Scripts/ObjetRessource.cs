using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjetRessource : Interactible {

    public enum TypeRessource { BOIS, FER };

    public GameObject ressource;
    public Text text;
    public TypeRessource type;
    public int quantite;

    public float distanceVisibilitePrix;

    private Player player;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start() {
        player = FindObjectOfType<Player>();
        gameManager = FindObjectOfType<GameManager>();

        ressource.SetActive(true);

        text.text = "" + quantite;
    }

    public override void Interact() {
        base.Interact();

        if (gameManager.heure != GameManager.Heure.NUIT)
        {
            Miner();
        }
    }

    void Miner() {
        if (quantite > 0) {
            player.inventaire.Add(type, 1);
            quantite--;
            text.text = "" + quantite;

            if(quantite == 0)
            {
                Destroy(this.gameObject);
            }
        }
    }

    private void Update()
    {
        // Update l'affichage du texte, ne s'affiche que si le joueur est assez proche
        float distance = Vector3.Distance(player.gameObject.transform.position, transform.position);
        if (distance <= distanceVisibilitePrix && gameManager.heure != GameManager.Heure.NUIT) {
            text.gameObject.SetActive(true);
        } else {
            text.gameObject.SetActive(false);
        }
    }

}
