using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InventorySlotBackpack : InventorySlot, ISelectHandler
{
    public Action<InventorySlotBackpack> OnSelected;

    [SerializeField] private TextMeshProUGUI m_CountLabel;
    [SerializeField] private float m_SelectedAlpha = 150.0f;

    private int m_ItemCount;
    private InventoryItemData m_Data;

    public override void Start() {
        base.Start();

        if (!m_CountLabel) {
            m_CountLabel = GetComponentInChildren<TextMeshProUGUI>();
        }
    }

    public void Initialize(InventoryItemData data, int itemCount) {
        base.Initialize(data);

        m_ItemCount = itemCount;
        m_Data = data;

        m_CountLabel.enabled = itemCount > 0;

        if (m_CountLabel) {
            m_CountLabel.text = m_ItemCount.ToString();
        }
    }

    public void OnSelect(BaseEventData eventData) {
        OnSelected?.Invoke(this);
    }

    public InventoryItemData GetData() {
        return m_Data;
    }
}
