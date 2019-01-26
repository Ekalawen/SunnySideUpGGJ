using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{

    private Player player;
    public Text boisValue;
    public Text ferValue;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        boisValue.text = ""+player.inventaire.Get(ObjetRessource.TypeRessource.BOIS);
        ferValue.text = ""+player.inventaire.Get(ObjetRessource.TypeRessource.FER);
    }
}
