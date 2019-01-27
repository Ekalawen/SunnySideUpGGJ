using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DonneurDuDoubleSaut : MonoBehaviour
{
    public int ajoutCoefForceSaut;

    private Player player;

    private void OnEnable() {
        player = FindObjectOfType<Player>();

        // Ajouter une force au saut qui correspond à un carreau de plus !
        player.jump_force += ajoutCoefForceSaut;
        
    }
}
