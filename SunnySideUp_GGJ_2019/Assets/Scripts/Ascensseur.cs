using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ascensseur : Interactible
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

    private bool direction = false;

    //Transform nacelle;
    public GameObject nacelle;
    public float speed = 1.0f;

    public Transform Up, Down;
    private float min, max;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();

        // Mettre dans le bon état
        SetEtat(Etat.NON_CONSTRUIT);

        // Mettre à jour les prix
        price.SetupPrix(priceBois, priceFer);

        min = Down.transform.position.y;
        max = Up.transform.position.y;
        Debug.Log("Min = " + min);
        Debug.Log("Max = " + max);

        Vector3 v = nacelle.transform.position;
        v.y = min;
        nacelle.transform.position = v;
    }

    public void SetEtat(Etat newEtat)
    {
        etat = newEtat;
        if (etat == Etat.NON_CONSTRUIT)
        {
            constructible.SetActive(true);
            construction.SetActive(false);
        }
        else
        {
            constructible.SetActive(false);
            construction.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Update l'affichage du prix, ne s'affiche que si le joueur est assez proche
        if (etat == Etat.NON_CONSTRUIT)
        {
            float distance = Vector3.Distance(player.gameObject.transform.position, transform.position);
            if (distance <= distanceVisibilitePrix)
            {
                price.gameObject.SetActive(true);
            }
            else
            {
                price.gameObject.SetActive(false);
            }
        }
        else
        {
            price.gameObject.SetActive(false);
            Vector3 v = nacelle.transform.position;
            if (direction && v.y < max)
            {
                v.y += speed * Time.deltaTime;
            }
            else if (!direction && v.y > min)
            {
                v.y -= speed * Time.deltaTime;
            }
            nacelle.transform.position = v;
        }        
    }

    public override void Interact()
    {
        base.Interact();
        if (peutConstruire() && etat == Etat.NON_CONSTRUIT)
        {
            // débitter le player
            player.inventaire.Use(ObjetRessource.TypeRessource.BOIS, priceBois);
            player.inventaire.Use(ObjetRessource.TypeRessource.FER, priceFer);

            // Puis on peut construire :)
            Construire();
        }
        else
        {
            direction = !direction;
        }       
    }

    public void Construire()
    {
        // Afficher les bons éléments
        constructible.SetActive(false);
        construction.SetActive(true);

        etat = Etat.CONSTRUIT;
    }

    // Permet de savoir si l'on peut construire cette construction
    public bool peutConstruire()
    {
        return player.transform.position.y >= transform.position.y - 3.0f
        && player.inventaire.CanUse(ObjetRessource.TypeRessource.BOIS, priceBois)
        && player.inventaire.CanUse(ObjetRessource.TypeRessource.FER, priceFer);
    }

    private void OnTriggerContact(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            player.setBlockMove(true);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            Vector3 v = player.gameObject.transform.position;
            if (direction && v.y < max)
            {
                v.y += speed * Time.deltaTime;
            }
            else if (!direction && v.y > min)
            {
                v.y -= speed * Time.deltaTime;
            }
            player.gameObject.transform.position = v;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            player.setBlockMove(false);
        }
    }
}
