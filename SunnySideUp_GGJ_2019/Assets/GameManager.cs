using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public enum Heure { JOUR, NUIT, CREPUSCULE, AUBE };

    public float dureeJour = 10.0f; // 60.0f * 1.5f; // 1:30 de jour !
    public float dureeNuit = 10.0f; // 60.0f * 1.5f; // 1:30 de nuit !
    public Light directionalLight;

    [HideInInspector]
    public Heure heure;

    private float debutHeure;
    private List<ObjetRessource> objects_to_respawn = new List<ObjetRessource>();

    private bool late_start = true;

    // Start is called before the first frame update
    void Start() {// On commence le jour
        ChangerHeure(Heure.JOUR);
    }

    void LateStart()
    {
        foreach (ObjetRessource o in FindObjectsOfType<ObjetRessource>())
        {
            if (o.respawnable)
                objects_to_respawn.Add(o);
        }
    }

    // Update is called once per frame
    void Update() {
        if (late_start)
        {
            LateStart();
            late_start = false;
        }
        UpdateHeure();
    }

    void UpdateHeure() {
        if(heure == Heure.JOUR) {
            if(Time.time - debutHeure > dureeJour) {
                ChangerHeure(Heure.NUIT);
            }
        } else if (heure == Heure.NUIT) {
            if(Time.time - debutHeure > dureeNuit) {
                ChangerHeure(Heure.JOUR);
            }
        }
    }

    public void ChangerHeure(Heure nouvelleHeure) {
        if(nouvelleHeure == Heure.JOUR) {
            StartCoroutine(FaireLeverLeJour());
        } else {
            StartCoroutine(FaireTomberLaNuit());
        }
    }

    IEnumerator FaireTomberLaNuit() {
        heure = Heure.CREPUSCULE;
        float duree = 10.0f;
        float debut = Time.time;
        while (heure == Heure.CREPUSCULE && Time.time - debut < duree)
        {
            float avancement = (Time.time - debut) / duree;
            RenderSettings.ambientIntensity = 1.0f - avancement;
            directionalLight.intensity = 1.0f - avancement;
            yield return null;
        }
        if(heure == Heure.CREPUSCULE)
        {
            debutHeure = Time.time;
            heure = Heure.NUIT;
        }
    }


    IEnumerator FaireLeverLeJour() {
        heure = Heure.AUBE;
        float duree = 10.0f;
        float debut = Time.time;
        float avancementDebut = RenderSettings.ambientIntensity;
        foreach (ObjetRessource o in objects_to_respawn)
        {
            o.Respawn();
        }

        while (heure == Heure.AUBE && Time.time - debut < duree)
        {
            float avancement = (Time.time - debut) / duree;
            RenderSettings.ambientIntensity = avancementDebut + avancement * (1 - avancementDebut);
            directionalLight.intensity = avancementDebut + avancement * (1 - avancementDebut);
            yield return null;
        }

        if (heure == Heure.AUBE)
        {
            heure = Heure.JOUR;
            debutHeure = Time.time;
        }
    }


    public void EndGame() {
        // save any game data here
#if UNITY_EDITOR
        // Application.Quit() does not work in the editor so
        // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }
}
