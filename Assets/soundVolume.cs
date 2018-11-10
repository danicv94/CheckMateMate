using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundVolume : MonoBehaviour {
	
    public UnityEngine.UI.Slider volume;
    public AudioSource audioOst;

	// Update is called once per frame
	void Update () {
        audioOst.volume = volume.value / 100;
	}
}
