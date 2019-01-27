using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmeliorateurDeMinage : MonoBehaviour
{
    public ObjetRessource.TypeRessource typeRessource;

    private Player player;

    private void OnEnable() {
        player = FindObjectOfType<Player>();

        // Rajouter le max au ressources
        int newVal = player.inventaire.GetIncrementRessource(typeRessource) + 1;
        player.inventaire.SetIncrementRessource(typeRessource, newVal);
        
    }
}
