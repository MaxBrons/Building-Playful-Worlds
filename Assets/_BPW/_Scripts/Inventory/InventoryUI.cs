using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class InventoryUI : MonoBehaviour
{
    public virtual void OnEnable() {
        Inventory.Instance.OnInventoryChanged += UpdateInventory;
    }

    public virtual void OnDisable() {
        Inventory.Instance.OnInventoryChanged -= UpdateInventory;
    }

    public abstract void UpdateInventory();
}
