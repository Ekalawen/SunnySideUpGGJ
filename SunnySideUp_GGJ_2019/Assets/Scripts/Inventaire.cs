using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventaire {
    public static int nbTypesRessources = ObjetRessource.TypeRessource.GetValues(typeof(ObjetRessource.TypeRessource)).Length;

    // Une liste du nombre de la quantité de ressources pour chaque type. Dans l'ordre de l'énumération.
    List<int> valeurs;

    public Inventaire() {
        // On remplit la liste de valeurs vides
        valeurs = new List<int>();
        for(int i = 0; i < nbTypesRessources; i++) {
            valeurs.Add(1000);
        }
    }

    public int Get(ObjetRessource.TypeRessource type) {
        int nb = (int)type;
        return valeurs[nb];
    }

    public void Add(ObjetRessource.TypeRessource type, int quantite) {
        int nb = (int)type;
        valeurs[nb] += quantite;
    }

    public bool CanUse(ObjetRessource.TypeRessource type, int quantite) {
        int nb = (int)type;
        return valeurs[nb] >= quantite;
    }

    public void Use(ObjetRessource.TypeRessource type, int quantite) {
        if(CanUse(type, quantite)) {
            int nb = (int)type;
            valeurs[nb] -= quantite;
        } else {
            throw new System.Exception("Pas assez de ressources !");
        }
    }
}
