using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Price : MonoBehaviour
{

    public Text prixBois;
    public Text prixPierre;

    // Start is called before the first frame update
    void Start() {
    }

    // Update is called once per frame
    void Update() {
    }

    public void SetupPrix(int _prixBois, int _prixPierre)
    {
        prixBois.text = "Bois : " + _prixBois;
        prixPierre.text = "Pierre : " + _prixPierre;
    }
}
