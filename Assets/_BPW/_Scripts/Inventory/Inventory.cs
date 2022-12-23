using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Inventory : MonoBehaviour, IDisposable
{
    public Action OnInventoryChanged;

    public static Inventory Instance {
        get {
            s_Inventory ??= new Inventory();
            return s_Inventory;
        }
    }
    private static Inventory s_Inventory;

    private Dictionary<InventoryItemData, InventoryItem> m_Items = new Dictionary<InventoryItemData, InventoryItem>();

    public InventoryItem Add(InventoryItemData data) {
        if (m_Items.TryGetValue(data, out InventoryItem item)) {
            item.IncreaseCount();

            OnInventoryChanged?.Invoke();
            return item;
        }

        InventoryItem newItem = new InventoryItem(data, 1);
        if (m_Items.TryAdd(data, newItem)) {
            OnInventoryChanged?.Invoke();
            return newItem;
        }

        return null;
    }

    public void Remove(InventoryItemData data) {
        if (m_Items.TryGetValue(data, out InventoryItem item)) {
            int count = item.DecreaseCount();
            if (count <= 1) {
                m_Items.Remove(data);
            }

            OnInventoryChanged?.Invoke();
        }
    }

    public List<InventoryItem> GetItems() {
        return new List<InventoryItem>(m_Items.Values);
    }

    public void Dispose() {
        s_Inventory = null;
    }

    //public bool TryGetInventoryItem(InventoryItemData data, out InventoryItem item) {
    //    if (!data) {
    //        item = null;
    //        return false;
    //    }
    //    foreach (var key in m_Items.Keys) {
    //        if (key.ID == data.ID &&
    //            key.ItemName == data.ItemName &&
    //            key.Image == data.Image &&
    //            key.Type == data.Type) {
    //            item = m_Items[key];
    //            return true;
    //        }
    //    }
    //    item = null;
    //    return false;
    //}
}
