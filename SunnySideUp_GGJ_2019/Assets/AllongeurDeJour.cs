using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllongeurDeJour : MonoBehaviour
{
    public float quantiteJourAjoutee;

    private GameManager gameManager;

    private void OnEnable() {
        gameManager = FindObjectOfType<GameManager>();

        // Rajouter la durée du jour nécessaire !
        gameManager.dureeJour += quantiteJourAjoutee;
    }
}
