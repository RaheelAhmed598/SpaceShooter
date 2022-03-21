using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//enemy object pooling 
public class EnemyPooler : MonoBehaviour
{
    //object pooling through list 
    public static EnemyPooler SharedInstance;
    public List<GameObject> pooledObjects;
    public GameObject objectsToPool;

    //amount of the object pooling 
    public int amountToPool;

    private void Awake()
    {
        SharedInstance = this;
    }
    void Start()
    {
        //enemy amount of pooling 
        pooledObjects = new List<GameObject>();
        for (int i = 0; i < amountToPool; i++)
        {
            GameObject obj = (GameObject)Instantiate(objectsToPool);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
    }

    //which prefab is get to pool
    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        return null;
    }
}
