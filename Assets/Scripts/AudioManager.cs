using UnityEngine.Audio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    List <Clip> WalkingSounds = new List <Clip>();

    public void PlayClip(string _name)
    {
        if(_name == "Walking")
        {
            int seed = Random.Range(0, WalkingSounds.Count);

        }
    }



    [System.Serializable]
    public class Clip
    {
        string name;
        AudioClip audio;
    }
}
