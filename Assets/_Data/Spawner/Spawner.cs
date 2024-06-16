using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spawner : MainMonoBehaviour
{
    [SerializeField] protected Transform holder;
    [SerializeField] protected List<Transform> prefabs;
    [SerializeField] protected List<Transform> poolObjs;

    protected override void LoadComponents()
    {
        this.LoadPrefabs();
        this.LoadHodler();
    }

    // Ham tim kiem object Holder trong gameObject hien tai
    protected virtual void LoadHodler()
    {
        if (this.holder != null) return;
        this.holder = transform.Find("Holder");
        Debug.Log(transform.name + ": LoadHodler", gameObject);
    }

    // Ham load tat ca object co trong object Prefabs
    protected virtual void LoadPrefabs()
    {
        if (this.prefabs.Count > 0) return;

        Transform prefabObj = transform.Find("Prefabs");
        foreach(Transform prefab in prefabObj)
        {
            this.prefabs.Add(prefab);
        }

        this.HidePrefabs();

        Debug.Log(transform.name + ": LoadPrefabs", gameObject);
    }

    // Set gameObject cua prefabs la false
    protected virtual void HidePrefabs()
    {
        foreach(Transform prefab in this.prefabs)
        {
            prefab.gameObject.SetActive(false);
        }
    }

    // Tao ra doi tuong moi voi ten, vi tri, goc quay
    public virtual Transform Spawn(string prefabName, Vector3 spawnPos,Quaternion rotation)
    {
        Transform prefab = this.GetPrefabByName(prefabName);
        if(prefab == null)
        {
            Debug.LogWarning("Prefab not found: " + prefabName);
            return null;
        }

        Transform newPrefab = this.GetObjectFromPool(prefab);
        newPrefab.SetPositionAndRotation(spawnPos, rotation);

        newPrefab.parent = this.holder;
        return newPrefab;
    }

    

    // Them obj can xoa vao poolObj de tai su dung
    public virtual void Despawn(Transform obj)
    {
        this.poolObjs.Add(obj);
        obj.gameObject.SetActive(false);
    }

    // Tra ve doi tuong nam trong poolObjs
    protected virtual Transform GetObjectFromPool(Transform prefab)
    {
        //Duyet vong lap, neu prefab truyen vao co trong poolObjs thi tra ve poolObj, neu khong thi tao moi
        foreach (Transform poolObj in this.poolObjs)
        {
            if (poolObj.name == prefab.name)
            {
                this.poolObjs.Remove(poolObj);
                return poolObj;
            }
        }

        Transform newPrefab = Instantiate(prefab);
        newPrefab.name = prefab.name;
        return newPrefab;
    }

    // Lay object Prefab tu ten cua prefab duoc truyen vao
    public virtual Transform GetPrefabByName(string prefabName)
    {
        foreach(Transform prefab in this.prefabs)
        {
            if (prefab.name == prefabName) return prefab;
        }

        return null;
    }

}
