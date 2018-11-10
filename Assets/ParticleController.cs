using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour {

    private float ellapsetTime = 0;
    private float duration = 1;
    // Update is called once per frame
    void Update () {
        ellapsetTime += Time.deltaTime;
        if (ellapsetTime > duration) {
            GameObject.Find("Pools").GetComponentInChildren<ParticlePool>().FreeEnemy(gameObject);
        }
	}

    public void resetParticle() {
        ellapsetTime = 0;
    }
}
