using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    //using the game objectpooling technique
    public static ObjectPooler SharedInstance;

    // list of objectpooling technique
    public List<GameObject> pooledObjects;

    //what to object to pool in game 
    public GameObject objectsToPool;

    //amout of the pooling object
    public int amountToPool;

    private void Awake()
    {
        //object pooling 
        SharedInstance = this;
    }
    void Start()
    {
        //list of the game object and amout of object pooling 
        pooledObjects = new List<GameObject>();
        for(int i = 0; i < amountToPool; i++)
        {
            GameObject obj = (GameObject)Instantiate(objectsToPool);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
    }

    //get object pool method 
    public GameObject GetPooledObject()
    {
        for(int i=0;i< pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        return null;
    }
}
