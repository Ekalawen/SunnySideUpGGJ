using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DestructibleBloc : Interactible
{
    private bool isDestroyed;

    public int resistance;
    public int distanceVisibiliteResistance;

    public Text text;

    private Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();

        isDestroyed = false;

        text.text = "" + resistance;
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
        
        if (distance <= distanceVisibiliteResistance)
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

        Miner();
    }

    void Miner()
    {
        if (resistance > 1)
        {
            resistance--;
            text.text = "" + resistance;
        }
        else
        {
            SetDestruction();
        }
    }
}
