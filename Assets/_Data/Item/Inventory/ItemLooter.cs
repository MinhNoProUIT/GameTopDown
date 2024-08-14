using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(Rigidbody))]
public class ItemLooter : SaiMonoBehaviour
{
    [SerializeField] Inventory inventory;
    [SerializeField] Rigidbody rigidbody;
    [SerializeField] SphereCollider sphereCollider;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadInventory();
        LoadRigidbody();
        LoadCollider();
    }

    private void LoadCollider()
    {
        if (sphereCollider != null) return;
        sphereCollider = transform.GetComponent<SphereCollider>();
        this.sphereCollider.isTrigger = true;
        this.sphereCollider.radius = 0.3f;
        Debug.LogWarning(transform.name + " LoadCollider", gameObject);
    }

    private void LoadRigidbody()
    {
        if (rigidbody != null) return;
        rigidbody = transform.GetComponent<Rigidbody>();
        this.rigidbody.isKinematic = true;
        //this.rigidbody2D.useGravity = true;
        this.rigidbody.useGravity = false;
        Debug.LogWarning(transform.name + " LoadCollider", gameObject);
    }

    private void LoadInventory()
    {
        if (inventory != null) return;
        inventory = transform.parent.GetComponent<Inventory>();
        Debug.LogWarning(transform.name + "Load Inventory", gameObject);

    }

    private void OnTriggerEnter(Collider other)
    {
        ItemPickUp itemPickUp = other.GetComponent<ItemPickUp>();
        if (itemPickUp == null) return;
        Debug.Log(other.name);
        Debug.Log(other.transform.parent.name);
        Debug.Log("Pick up a item");

    }
}
