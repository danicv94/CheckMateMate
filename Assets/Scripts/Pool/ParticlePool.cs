using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePool : MonoBehaviour
{

    public GameObject Prefabs;
    public int PoolSize;
    private int _poolInstantiationRate = 2;
    private PrefabPool _pool;

    private void Awake()
    {
        _pool = new PrefabPool(Prefabs, transform.position);
        _pool.AddItems(5, Prefabs);
    }

    public GameObject GetParticle()
    {
        if (_pool.Size() == 0)
        {
            _pool.AddItems(5, Prefabs);
        }
        GameObject particle = _pool.GetFreeItem();
        particle.GetComponent<ParticleController>().resetParticle();
        particle.SetActive(true);
        particle.transform.SetParent(null);
        return particle;
    }

    public void FreeEnemy(GameObject go)
    {
        go.SetActive(false);
        go.transform.SetParent(transform);
        _pool.AddFreeItem(go);
    }
}
