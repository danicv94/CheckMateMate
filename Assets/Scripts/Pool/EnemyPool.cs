using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    public List<GameObject> Prefabs;
    public List<int> PoolSize;
    public List<EnemyType> PoolTypes;
    private int _poolInstantiationRate = 2;

    private Hashtable _pool;

    private void Awake()
    {
        _pool = new Hashtable();
        for (int i = 0; i < Prefabs.Count; i++)
        {
            if (Prefabs[i] != null)
            {
                _pool.Add(PoolTypes[i], new PrefabPool(Prefabs[i], transform.position));
            }
        }
    }

    private void Update()
    {
        for (int i = 0; i < Prefabs.Count; i++)
        {
            if (Prefabs[i] != null && ((PrefabPool)_pool[PoolTypes[i]]).Size() < PoolSize[i])
            {
                ((PrefabPool)_pool[PoolTypes[i]]).AddItems(_poolInstantiationRate, gameObject);
            }
        }
    }

    public GameObject GetEnemy(EnemyType type)
    {
        if (((PrefabPool)_pool[type]).Size() == 0)
        {
            ((PrefabPool)_pool[type]).AddItems(_poolInstantiationRate, gameObject);
        }

        GameObject enemy = ((PrefabPool)_pool[type]).GetFreeItem();
        enemy.SetActive(true);
        enemy.transform.SetParent(null);
        enemy.GetComponent<GameEntityController>().ResetValues();
        return enemy;
    }

    public void FreeEnemy(EnemyType type, GameObject go)
    {
        go.SetActive(false);
        go.transform.SetParent(transform);
        ((PrefabPool)_pool[type]).AddFreeItem(go);
    }
}