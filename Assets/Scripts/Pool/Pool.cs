using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Pool : MonoBehaviour
{
    [SerializeField] GameObject poolItemtPrefab;
    [SerializeField] int startPoolsize = 10;
    [SerializeField] int poolExpandStep = 5;
    [SerializeField] bool unparantedItems = true;

    List<GameObject> itemsPool;


    //create pool with new items
    void PoolInit()
    {
        itemsPool = new List<GameObject>();

        PoolExpand(startPoolsize);
    }

    /// <summary>
    /// Turn all items to default state
    /// </summary>
    public virtual void ResetPool()
    {
        for (int i = 0; i < itemsPool.Count; i++)
        {
            itemsPool[i].SetActive(false);
        }
    }

    /// <summary>
    /// Return first inactive item from pool
    /// </summary>
    public GameObject GetPoolItem()
    {
        if (itemsPool == null)
            PoolInit();

        for (int i = 0; i < itemsPool.Count; i++)
        {
            if (!itemsPool[i].activeSelf)
            {
                ExpandNeedCheck(i);
                return itemsPool[i];
            }
        }

        return null;
    }

    public void SetPoolItem(GameObject prefab)
    {
        poolItemtPrefab = prefab;

        if(itemsPool != null)
        {
            PoolInit();
        }
    }

    void PoolExpand(int quantity)
    {
        for (int i = 0; i < quantity; i++)
        {
            GameObject go;

            if (unparantedItems)
            {
                go = Instantiate(poolItemtPrefab);
            }
            else
            {
                go = Instantiate(poolItemtPrefab, transform);
            }

            go.SetActive(false);
            itemsPool.Add(go);
        }
    }

    //check if all pool elements in using
    void ExpandNeedCheck(int lastIndex)
    {
        if (lastIndex == itemsPool.Count - 1)
            PoolExpand(poolExpandStep);
    }

    int GetActiveElementsCount()
    {
        int counnt = 0;

        for (int i = 0; i < itemsPool.Count; i++)
        {
            if (itemsPool[i].activeSelf)
                counnt++;
            else
                break;
        }
        return counnt;

    }

    GameObject GetItemByIndex(int index)
    {
        return itemsPool[index];
    }

    int GetPoolSize()
    {
        return itemsPool.Count;
    }
}
