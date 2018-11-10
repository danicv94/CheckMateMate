using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class getScore : MonoBehaviour {

    public Text texto;
	
	// Update is called once per frame
	void Update () {
        texto.text = PlayerPrefs.GetInt("Score").ToString();
	}
}
