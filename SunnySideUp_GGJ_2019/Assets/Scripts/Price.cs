using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Price : MonoBehaviour
{

    public Text prixBois;
    public Text prixFer;

    // Start is called before the first frame update
    void Start() {
    }

    // Update is called once per frame
    void Update() {
    }

    public void SetupPrix(int _prixBois, int _prixFer)
    {
        prixBois.text = "Bois : " + _prixBois;
        prixFer.text = "Fer : " + _prixFer;
    }
}
