using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class InventoryItem
{
    private InventoryItemData m_Data;
    private int m_Count;

    public InventoryItem(InventoryItemData data, int count) {
        m_Data = data;
        m_Count = count;
    }

    public int IncreaseCount() => m_Count++;

    public int DecreaseCount() => m_Count--;

    public InventoryItemData Data => m_Data;
    public int Count => m_Count;
}
