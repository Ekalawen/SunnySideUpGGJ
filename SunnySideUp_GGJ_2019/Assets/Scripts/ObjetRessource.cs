using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjetRessource : Interactible {

    public enum TypeRessource { BOIS, FER };

    public GameObject ressource;
    public Text text;
    public TypeRessource type;
    public int quantiteMax;
    private int quantiteActuelle;

    public float distanceVisibilitePrix;

    public bool respawnable = false;

    private Player player;
    private GameManager gameManager;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start() {
        quantiteActuelle = quantiteMax;

        player = FindObjectOfType<Player>();
        gameManager = FindObjectOfType<GameManager>();
        audioSource = GetComponent<AudioSource>();

        ressource.SetActive(true);

        text.text = "" + quantiteActuelle;
    }

    public override void Interact() {
        base.Interact();

        if (gameManager.heure != GameManager.Heure.NUIT)
        {
            audioSource.Play();
            Miner();
        }
    }

    void Miner() {
        if (quantiteActuelle > 0) {
            int valIncrement = player.inventaire.GetIncrementRessource(type);
            player.inventaire.Add(type, valIncrement);
            quantiteActuelle--;
            text.text = "" + quantiteActuelle;

            if(quantiteActuelle == 0)
            {
                if (respawnable)
                {
                    this.gameObject.SetActive(false);
                }
                else
                    Destroy(this.gameObject);
            }
        }
    }

    public void Respawn() {
        quantiteActuelle = quantiteMax;
        text.text = "" + quantiteActuelle;
        this.gameObject.SetActive(true);
    }

    private void Update()
    {
        if (!player || !player.enabled)
            return;

        // Update l'affichage du texte, ne s'affiche que si le joueur est assez proche
        float distance = Vector3.Distance(player.gameObject.transform.position, transform.position);
        if (distance <= distanceVisibilitePrix && gameManager.heure != GameManager.Heure.NUIT) {
            text.gameObject.SetActive(true);
        } else {
            text.gameObject.SetActive(false);
        }
    }

}
