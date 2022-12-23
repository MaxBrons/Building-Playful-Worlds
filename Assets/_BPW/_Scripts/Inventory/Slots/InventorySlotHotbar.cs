using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlotHotbar : InventorySlot
{
    [SerializeField] private TextMeshProUGUI m_HotbarNumber;
    [SerializeField] private Image m_SelectBackground;
    [SerializeField] private float m_AlphaOnSelect = 0.4f;

    private int m_HotbarIndex;
    private InventoryItemData m_Data;

    public override void Start() {
        base.Start();
    }

    public void Initialize(InventoryItemData data, int hotbarIndex) {
        base.Initialize(data);

        m_Data = data;

        m_HotbarIndex = hotbarIndex;
        if (m_HotbarNumber) {
            m_HotbarNumber.text = m_HotbarIndex.ToString();
        }
    }

    public InventoryItemData GetData() {
        return m_Data;
    }

    public void Select() {
        var color = m_SelectBackground.color;
        m_SelectBackground.color = new Color(color.r, color.g, color.b, m_AlphaOnSelect);
    }

    public void Deselect() {
        var color = m_SelectBackground.color;
        m_SelectBackground.color = new Color(color.r, color.g, color.b, 0.0f);
    }
}
