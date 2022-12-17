using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public struct InventoryItem
{
    public InventoryItem(int itemID, Texture2D itemImage, int itemCount) {
        ItemID = itemID;
        ItemImage = itemImage;
        ItemCount = itemCount;
    }
    public int ItemID { get; private set; }
    public Texture2D ItemImage { get; private set; }
    public int ItemCount { get; private set; }
}

public class Inventory
{
    private InventorySlot[] m_Slots;

    public Inventory(int size) {
        m_Slots = new InventorySlot[size];
    }
    public InventoryItem AddItem(InventoryItem item) {

        return item;
    }

    public InventoryItem[] AddItem(params InventoryItem[] items) {

        return items;
    }
}

public class InventorySlot
{
    public static implicit operator bool(InventorySlot slot) {
        return slot is not null;
    }
}