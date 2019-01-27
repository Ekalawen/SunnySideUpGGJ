using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DestructibleBloc : Interactible
{
    private bool isDestroyed;

    public GameObject meshIntact;
    public GameObject meshEndommage;
    public GameObject meshBeaucoupEndommage;

    public int resistance;
    private int resistanceMax;
    public int distanceVisibiliteResistance;

    public Text text;

    private Player player;
    private GameManager gameManager;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start() {
        player = FindObjectOfType<Player>();
        gameManager = FindObjectOfType<GameManager>();
        audioSource = GetComponent<AudioSource>();

        isDestroyed = false;

        text.text = "" + resistance;

        resistanceMax = resistance;

        transform.GetChild(0).gameObject.SetActive(true);
    }

    public void SetDestruction()
    {
        isDestroyed = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!player || !player.enabled)
            return;

        if (isDestroyed)
        {
            Destroy(this.gameObject);
        }
        // Update l'affichage du texte, ne s'affiche que si le joueur est assez proche
        float distance = Vector3.Distance(player.gameObject.transform.position, transform.position);
        if (gameManager.heure != GameManager.Heure.NUIT && distance <= distanceVisibiliteResistance)
        {
            text.gameObject.SetActive(true);
        }
        else
        {
            text.gameObject.SetActive(false);
        }

    }

    public override void Interact()
    {
        base.Interact();

        if (gameManager.heure != GameManager.Heure.NUIT)
        {
            audioSource.Play();
            Miner();
        }
    }

    void Miner()
    {
        this.GetComponent<AudioSource>().Play();
        if (resistance > 1)
        {
            resistance--;
            if((float)resistance  / (float)resistanceMax >= 0.5f) {
                meshIntact.SetActive(false);
                meshEndommage.SetActive(true);
                meshBeaucoupEndommage.SetActive(false);
            } else
            {
                meshIntact.SetActive(false);
                meshEndommage.SetActive(false);
                meshBeaucoupEndommage.SetActive(true);
            }
            text.text = "" + resistance;
        }
        else
        {
            SetDestruction();
        }
    }
}
