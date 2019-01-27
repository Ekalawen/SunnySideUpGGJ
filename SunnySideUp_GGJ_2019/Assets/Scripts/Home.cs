using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Home : MonoBehaviour
{

    private Player player;
    public float mod;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        player.addToSpeedMod(mod);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
