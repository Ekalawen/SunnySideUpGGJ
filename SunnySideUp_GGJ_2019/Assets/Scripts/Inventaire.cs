using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventaire {
    public static int nbTypesRessources = ObjetRessource.TypeRessource.GetValues(typeof(ObjetRessource.TypeRessource)).Length;

    // Une liste du nombre de la quantité de ressources pour chaque type. Dans l'ordre de l'énumération.
    List<int> valeurs;
    List<int> valeursMax;
    List<int> incrementRessource;

    public Inventaire() {
        // On remplit la liste de valeurs vides
        valeurs = new List<int>();
        valeursMax = new List<int>();
        incrementRessource = new List<int>();
        for(int i = 0; i < nbTypesRessources; i++) {
            valeurs.Add(0);
            valeursMax.Add(15);
            incrementRessource.Add(1);
        }
    }

    public int Get(ObjetRessource.TypeRessource type) {
        int nb = (int)type;
        return valeurs[nb];
    }

    public void Add(ObjetRessource.TypeRessource type, int quantite) {
        if (Get(type) < GetValMax(type))
        {
            int nb = (int)type;
            valeurs[nb] += quantite;
        }
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

    public int GetValMax(ObjetRessource.TypeRessource type) {
        int nb = (int)type;
        return valeursMax[nb];
    }
    public void SetValMax(ObjetRessource.TypeRessource type, int newVal) {
        int nb = (int)type;
        valeursMax[nb] = newVal;
    }
    public int GetIncrementRessource(ObjetRessource.TypeRessource type) {
        int nb = (int)type;
        return incrementRessource[nb];
    }
    public void SetIncrementRessource(ObjetRessource.TypeRessource type, int newVal) {
        int nb = (int)type;
        incrementRessource[nb] = newVal;
    }
}
