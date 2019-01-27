using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StockDeRessources : MonoBehaviour
{
    public ObjetRessource.TypeRessource typeRessource;
    public int quantiteRessource;

    private Player player;

    private void OnEnable() {
        player = FindObjectOfType<Player>();

        // Rajouter le max au ressources
        int newVal = player.inventaire.GetValMax(typeRessource) + quantiteRessource;
        player.inventaire.SetValMax(typeRessource, newVal);
        
    }
}
