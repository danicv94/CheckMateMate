using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class getText : MonoBehaviour {

    public Text textoScore;
    public Text textInput;

    // Update is called once per frame
    void Update () {
        textoScore.text = textInput.text;
	}
}
