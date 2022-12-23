using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private InventoryItemData m_DataRef;

    public InventoryItemData InventoryItemData => m_DataRef;
}
