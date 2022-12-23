using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ItemType
{
    weapons = 0,
    resources = 1
}

[CreateAssetMenu(fileName = "InventoryItemData", menuName = "ScriptableObjects/InventoryItemData", order = 1)]
public class InventoryItemData : ScriptableObject
{
    [SerializeField] private int m_ID;
    [SerializeField] private string m_ItemName;
    [SerializeField] private Sprite m_Image;
    [SerializeField] private ItemType m_Type;
    [SerializeField] private bool m_CanStack = false;
    [SerializeField] private GameObject m_ObjectReference;
    
    public InventoryItemData(int iD, string itemName, Sprite image, ItemType type, bool canStack, GameObject objectReference) {
        m_ID = iD;
        m_ItemName = itemName;
        m_Image = image;
        m_Type = type;
        m_CanStack = canStack;
        m_ObjectReference = objectReference;
    }

    public int ID => m_ID;
    public string ItemName => m_ItemName;
    public Sprite Image => m_Image;
    public ItemType Type => m_Type;
    public bool CanStack => m_CanStack;
    public GameObject ObjectReference => m_ObjectReference;

}
