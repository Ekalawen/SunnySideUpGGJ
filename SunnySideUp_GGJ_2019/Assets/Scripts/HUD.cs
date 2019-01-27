using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{

    public Text boisValue;
    public Text ferValue;
    public Text MomentDeLaJournee;
    public Text TempsAvantChangementHeure;

    private Player player;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!player || !player.enabled)
            return;

        boisValue.text = ""+player.inventaire.Get(ObjetRessource.TypeRessource.BOIS);
        ferValue.text = ""+player.inventaire.Get(ObjetRessource.TypeRessource.FER);

        MomentDeLaJournee.text = gameManager.GetTextHeure();
        TempsAvantChangementHeure.text = "" + gameManager.TempsAvantChangementHeure().ToString("F2");
    }
}
