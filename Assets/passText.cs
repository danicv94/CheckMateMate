using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class passText : MonoBehaviour {

    public UnityEngine.UI.Text texto;
    public UnityEngine.UI.InputField input;

    void Update()
    {
        Debug.Log(texto, input);
    }


}
