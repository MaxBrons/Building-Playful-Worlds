using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class InventoryItemData : MonoBehaviour
{
    [SerializeField] private Inventory.InventoryItem.ItemDataStruct m_ItemData;

    public Inventory.InventoryItem.ItemDataStruct GetData() => m_ItemData;
}
