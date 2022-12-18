using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class Inventory
{
    public Action<InventoryItem.ItemDataStruct, int> OnInventoryChanged;

    private List<InventoryItem> m_Inventory = new List<InventoryItem>();
    private Dictionary<InventoryItem.ItemDataStruct, int> m_SlotsData = new Dictionary<InventoryItem.ItemDataStruct, int>();

    public void AddItem(InventoryItem item) {
        if (item.ItemData.ID > -1 && item.ItemData.Image) {
            m_Inventory.Add(item);
            ChangeSlotData(item, 1);

            OnInventoryChanged?.Invoke(item.ItemData, m_SlotsData[item.ItemData]);
            Debug.Log(m_SlotsData.Count);
        }
    }


    public GameObject RemoveItem(InventoryItem item) {
        if (m_Inventory.Contains(item)) {
            m_Inventory.Remove(item);
            ChangeSlotData(item, -1);

            OnInventoryChanged?.Invoke(item.ItemData, m_SlotsData[item.ItemData]);
            return item.ObjectRef;
        }
        return null;
    }

    private void ChangeSlotData(InventoryItem item, int amount) {
        if (m_SlotsData.ContainsKey(item.ItemData)) {
            if ((m_SlotsData[item.ItemData] += amount) <= 0) {
                m_SlotsData.Remove(item.ItemData);
            }
        }
        else {
            m_SlotsData.Add(item.ItemData, 1);
        }
    }

    public class InventoryItem
    {
        [Serializable]
        public struct ItemDataStruct
        {
            public int ID;
            public Texture2D Image;
        }
        public ItemDataStruct ItemData { get { return m_ItemData; } }
        public GameObject ObjectRef { get { return m_ObjectRef; } }

        private GameObject m_ObjectRef;
        private ItemDataStruct m_ItemData;

        public InventoryItem(int itemID, Texture2D itemImage, GameObject objectRef) {
            m_ItemData.ID = itemID;
            m_ItemData.Image = itemImage;
            m_ObjectRef = objectRef;
        }

    }

    //public Action<InventorySlot[]> OnInventoryChanged;

    //private InventorySlot[] m_Slots;

    //public Inventory(int size) {
    //    m_Slots = new InventorySlot[size];
    //}

    //public InventorySlot AddItem(InventoryItem item) {
    //    var slot = m_Slots.First((s) => s.IsAvailable());
    //    slot.SetItem(item);

    //    return slot;
    //}

    //public InventorySlot[] AddItem(params InventoryItem[] items) {
    //    InventorySlot[] returnItems = new InventorySlot[items.Length];

    //    for (int i = 0; i < items.Length; i++) {
    //        var slot = m_Slots.First((s) => s.IsAvailable());
    //        returnItems[i] = slot;
    //        slot.SetItem(items[i]);
    //    }

    //    return returnItems;
    //}

    //public void IncreaseCount(int amount) => ItemCount += amount;
    //public void DecreaseCount(int amount) => ItemCount -= amount;
}


//public class InventorySlot
//{
//    public InventoryItem Item { get; private set; }
//    public bool SetItem(InventoryItem item) {
//        if (Item != null || item == null)
//            return false;

//        Item = item;
//        return true;
//    }



//    public bool IsAvailable() {
//        return Item == null;
//    }

//    public static implicit operator bool(InventorySlot slot) {
//        return slot is not null;
//    }
//}