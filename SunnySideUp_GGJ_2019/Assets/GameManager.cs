using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public enum Heure { JOUR, NUIT, CREPUSCULE, AUBE };

    public float dureeJour = 10.0f; // 60.0f * 1.5f; // 1:30 de jour !
    public float dureeNuit = 10.0f; // 60.0f * 1.5f; // 1:30 de nuit !
    public float dureeAube = 3.0f; // 60.0f * 1.5f; // 1:30 de jour !
    public float dureeCrepuscule = 3.0f; // 60.0f * 1.5f; // 1:30 de nuit !
    private float debutLuminosite = 1.0f;
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
        float avancement, val;
        switch(heure)
        {
            case Heure.AUBE:
                if(Time.time - debutHeure > dureeAube) {
                    ChangerHeure(Heure.JOUR);
                }
                avancement = (Time.time - debutHeure) / dureeAube;
                val = debutLuminosite + (1.0f - debutLuminosite) * avancement; ;
                RenderSettings.ambientIntensity = val;
                directionalLight.intensity = val;
                break;
            case Heure.CREPUSCULE: 
                if(Time.time - debutHeure > dureeCrepuscule) {
                    ChangerHeure(Heure.NUIT);
                }
                avancement = (Time.time - debutHeure) / dureeCrepuscule;
                val = debutLuminosite - debutLuminosite * avancement; ;
                RenderSettings.ambientIntensity = val;
                directionalLight.intensity = val;
                break;
            case Heure.JOUR: 
                if(Time.time - debutHeure > dureeJour) {
                    ChangerHeure(Heure.CREPUSCULE);
                }
                RenderSettings.ambientIntensity = 1.0f;
                directionalLight.intensity = 1.0f;
                break;
            case Heure.NUIT: 
                if(Time.time - debutHeure > dureeNuit) {
                    ChangerHeure(Heure.AUBE);
                }
                RenderSettings.ambientIntensity = 0.0f;
                directionalLight.intensity = 0.0f;
                break;
        }

    }

    public void ChangerHeure(Heure nouvelleHeure) {
        debutHeure = Time.time;
        debutLuminosite = RenderSettings.ambientIntensity;
        heure = nouvelleHeure;

        // Si le levé du jour alors on fait réapparaître les ressources !
        if(nouvelleHeure == Heure.AUBE)
        {
            foreach (ObjetRessource o in objects_to_respawn)
            {
                o.Respawn();
            }
        }
    }

    public string GetTextHeure() {
        switch(heure)
        {
            case Heure.AUBE: return "Aube";
            case Heure.CREPUSCULE: return "Crépuscule";
            case Heure.JOUR: return "Jour";
            case Heure.NUIT: return "Nuit";
            default: return "Je suis un canard <3";
        }
    }

    public float TempsAvantChangementHeure() {
        switch(heure)
        {
            case Heure.AUBE: return dureeAube - (Time.time - debutHeure);
            case Heure.CREPUSCULE: return dureeCrepuscule - (Time.time - debutHeure);
            case Heure.JOUR: return dureeJour - (Time.time - debutHeure);
            case Heure.NUIT: return dureeNuit - (Time.time - debutHeure);
            default: return -3.14159f;
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
