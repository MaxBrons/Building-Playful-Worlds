using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InventoryUIHotbar : InventoryUI
{
    public Action<InventoryItemData, int> OnCurrentSelectionChanged;
    public Action<InventoryItemData, int> OnSelectionDeleted;
    public Action<List<InventoryItem>> OnSlotsChanged;

    [SerializeField] private List<InventorySlotHotbar> m_InventorySlots;

    private int m_CurrentSelectedIndex = 0;

    private void Start() {
        m_InventorySlots.ForEach((e) => {
            e.Initialize(null, m_InventorySlots.IndexOf(e) + 1);
        });

        SelectSlot(0);
    }

    public override void UpdateInventory() {
        var inv = Inventory.Instance.GetItems().Where((item) => item.Data.Type == ItemType.weapons).ToList();

        for (int i = 0; i < m_InventorySlots.Count; i++) {
            m_InventorySlots[i].Initialize(null, i + 1);
        }

        var total = 0;
        foreach (var item in inv) {
            for (int i = 0; i < item.Count && total < m_InventorySlots.Count; i++) {
                m_InventorySlots[total].Initialize(item.Data, total + 1);
                total++;
            }
        }

        OnCurrentSelectionChanged?.Invoke(m_InventorySlots[m_CurrentSelectedIndex].GetData(), m_CurrentSelectedIndex);
        OnSlotsChanged?.Invoke(inv);
    }

    private void SelectSlot(int index) {
        m_CurrentSelectedIndex = index < m_InventorySlots.Count ? index : -1;

        for (int i = 0; i < m_InventorySlots.Count; i++) {
            if (i == index) {
                m_InventorySlots[i].Select();

                OnCurrentSelectionChanged?.Invoke(m_InventorySlots[i].GetData(), i);
                continue;
            }

            m_InventorySlots[i].Deselect();
        }
    }

    private void OnScrollWheel(InputValue value) {
        var scrollValue = (int)Mathf.Clamp(value.Get<Vector2>().normalized.y, -1, 1);
        m_CurrentSelectedIndex -= scrollValue;
        if (m_CurrentSelectedIndex > m_InventorySlots.Count - 1) {
            m_CurrentSelectedIndex = 0;
        }
        else if (m_CurrentSelectedIndex < 0) {
            m_CurrentSelectedIndex = m_InventorySlots.Count - 1;
        }

        SelectSlot(m_CurrentSelectedIndex);
    }

    private void OnDelete() {
        var slot = m_InventorySlots[m_CurrentSelectedIndex].GetData();
        Inventory.Instance.Remove(slot);
        OnSelectionDeleted?.Invoke(slot, m_CurrentSelectedIndex);
    }

}
