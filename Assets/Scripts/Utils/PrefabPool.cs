using System.Collections.Generic;
using UnityEngine;

public class PrefabPool
{
    private GameObject _prefab;
    private Vector3 _poolPosition;

    private Stack<GameObject> _poolObjects;

    public PrefabPool(GameObject p, Vector3 position)
    {
        _prefab = p;
        _poolPosition = position;

        _poolObjects = new Stack<GameObject>();
    }

    public int Size()
    {
        return _poolObjects.Count;
    }

    public void AddItems(int Quantity, GameObject poolGameObject)
    {
        GameObject tmpObject;
        for (int i = 0; i < Quantity; i++)
        {
            tmpObject = GameObject.Instantiate(_prefab);
            tmpObject.SetActive(false);
            //tmpObject.transform.SetParent(poolGameObject.transform);
            _poolObjects.Push(tmpObject);
        }
    }

    public GameObject GetFreeItem()
    {
        return _poolObjects.Pop();
    }

    public void AddFreeItem(GameObject Object)
    {
        _poolObjects.Push(Object);
    }
}