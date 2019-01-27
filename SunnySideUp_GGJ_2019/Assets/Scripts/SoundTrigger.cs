using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTrigger : MonoBehaviour
{
    public AudioSource audioSrce;
    public AudioClip[] defaultFloorClips;
    public AudioClip[] woodFloorClips;
    public AudioClip[] grassFloorClips;

    // Start is called before the first frame update
    void Awake()
    {
//        audioSrce = GameObject.FindObjectOfType<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void RandomizeClip(LOVs.FloorType floorType)
    {
        int clipSelection = Mathf.FloorToInt(Random.Range(0.0f,2.9f));
        switch (floorType)
        {
            case LOVs.FloorType.Concrete:
                audioSrce.clip = defaultFloorClips[clipSelection];
                break;
            case LOVs.FloorType.Grass:
                audioSrce.clip = grassFloorClips[clipSelection];
                break;
            case LOVs.FloorType.Wood:
                audioSrce.clip = woodFloorClips[clipSelection];
                break;
            default:
                audioSrce.clip = defaultFloorClips[clipSelection];
                break;
        }
    }

    private void OnTriggerEnter(Collider col)
    { 
        if(col.gameObject.GetComponent<Base_Bloc_Script>())
        {
            RandomizeClip(col.gameObject.GetComponent<Base_Bloc_Script>().floorType);
            audioSrce.Play();
            Debug.Log("Touch...");
        }
    }
}
