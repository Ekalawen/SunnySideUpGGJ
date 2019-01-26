using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Price : MonoBehaviour
{

    public Text prix;

    // Start is called before the first frame update
    void Start() {
    }

    // Update is called once per frame
    void Update() {
    }

    public void SetupPrix(int _prixBois, int _prixFer)
    {
        if (_prixBois > 0 && _prixFer > 0)
            prix.text = _prixBois + " Bois\n" + _prixFer + " Fer";
        else if (_prixFer > 0)
            prix.text = _prixFer + " Fer";
        else if (_prixBois > 0)
            prix.text = _prixBois + " Bois";
        else
            prix.text = "PAS DE PRIX WTF ! :D";
    }
}
