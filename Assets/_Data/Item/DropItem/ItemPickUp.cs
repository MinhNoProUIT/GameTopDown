using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(SphereCollider))]

public class ItemPickUp : JunkAbstract
{
    [SerializeField] SphereCollider sphereCollider;
    protected override void LoadComponents()
    {
        base.LoadComponents();
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

    public static ItemCode String2ItemCode(string itemName)
    {
        return (ItemCode)System.Enum.Parse(typeof(ItemCode), itemName);
    }

    public virtual ItemCode GetItemCode()
    {
        return ItemPickUp.String2ItemCode(transform.parent.name);
    }

    public virtual void Pickup()
    {
        this.junkCtrl.JunkDespawn.DespawnObject();
    }
}
