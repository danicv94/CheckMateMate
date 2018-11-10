using System.Collections;
using System.Collections.Generic;
using Enums;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public List<GameObject> Prefabs;
    public List<int> PoolSize;
    public List<BulletType> BulletTypes;

    private Hashtable _pool;
    private int _poolInstantiationRate = 2;

    private void Start()
    {
        _pool = new Hashtable();
        for (int i = 0; i < Prefabs.Count; i++)
        {
            if (Prefabs[i] != null)
            {
                _pool.Add(BulletTypes[i],new PrefabPool(Prefabs[i], transform.position));
            }
        }
    }

    private void Update()
    {
        for (int i = 0; i < Prefabs.Count; i++)
        {
            if (Prefabs[i] != null && ((PrefabPool)_pool[BulletTypes[i]]).Size()<PoolSize[i])
            {
                ((PrefabPool)_pool[BulletTypes[i]]).AddItems(_poolInstantiationRate,gameObject);
            }
        }
    }

    public GameObject GetBullet(BulletType type)
    {
        if (((PrefabPool)_pool[type]).Size() == 0)
        {
            ((PrefabPool)_pool[type]).AddItems(_poolInstantiationRate,gameObject);
        }

        GameObject bullet = ((PrefabPool)_pool[type]).GetFreeItem();
        bullet.SetActive(true);
        bullet.transform.SetParent(null);
        bullet.GetComponent<BulletController>().ResetValues();
        return bullet;
    }

    public void FreeBullet(BulletType type, GameObject go)
    {
        go.SetActive(false);
        go.transform.SetParent(transform);
        ((PrefabPool)_pool[type]).AddFreeItem(go);
    }
}