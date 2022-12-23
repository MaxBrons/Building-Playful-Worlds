using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;

public class InventoryUIBackpack : InventoryUI
{
    [SerializeField] private GameObject m_VisibilityRoot;
    [SerializeField] private List<InventorySlotBackpack> m_InventorySlots;
    [SerializeField] private Button m_UseItemButton;
    [SerializeField] private Button m_DropItemButton;

    private bool m_DefaultRootVisibility = false;
    private InventorySlotBackpack m_CurrentSelectedSlot;

    public void Start() {
        m_InventorySlots.ForEach((e) => {
            e.Initialize(null, 0);
        });

        m_InventorySlots.ForEach((s) => s.OnSelected += OnButtonPressed);

        m_UseItemButton.onClick.AddListener(UseItem);
        m_DropItemButton.onClick.AddListener(DropItem);

        SetInventoryEnabled(m_DefaultRootVisibility);

    }

    private void OnButtonPressed(InventorySlotBackpack slot) {
        m_CurrentSelectedSlot = slot;
    }

    private void UseItem() {
        
    }

    private void DropItem() {
        if (m_CurrentSelectedSlot) {
            Inventory.Instance.Remove(m_CurrentSelectedSlot.GetData());
            Instantiate(m_CurrentSelectedSlot.GetData().ObjectReference, GameManager.Instance.Player.transform.position, new Quaternion(0, 0, 0, 0));
        }
    }

    public void SetInventoryEnabled(bool value) {
        if (m_VisibilityRoot) {
            m_VisibilityRoot.SetActive(value);
        }
    }

    public override void UpdateInventory() {
        var inv = Inventory.Instance.GetItems().Where((item) => item.Data.Type == ItemType.resources).ToList();

        for (int i = 0; i < m_InventorySlots.Count; i++) {
            if (i >= inv.Count) {
                m_InventorySlots[i].Initialize(null, 0);
                continue;
            }
            m_InventorySlots[i].Initialize(inv[i].Data, inv[i].Count);
        }
    }

    public void OnNext(InputValue value) {
        bool active = m_VisibilityRoot.activeSelf;
        SetInventoryEnabled(!active);
        InputManager.Instance.SetPlayerInputActive(active);
    }
}
