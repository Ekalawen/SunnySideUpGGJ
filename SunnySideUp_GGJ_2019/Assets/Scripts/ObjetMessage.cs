using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjetMessage : MonoBehaviour
{
    public GameObject ressource;
    public Text text;
    public string message;

    public float distanceVisibiliteMessage;

    private Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        // Update l'affichage du texte, ne s'affiche que si le joueur est assez proche
        float distance = Vector3.Distance(player.gameObject.transform.position, transform.position);
        if (distance <= distanceVisibiliteMessage)
        {
            text.text = message;
            text.gameObject.SetActive(true);
        }
        else
        {
            text.gameObject.SetActive(false);
        }
    }
}
