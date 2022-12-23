using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Hotbar : MonoBehaviour
{

    [SerializeField] private InventoryUIHotbar m_HotbarUI;
    [SerializeField] private List<InventoryItemData> m_WeaponsData = new List<InventoryItemData>();
    [SerializeField] private GameObject m_Arm;

    private List<GameObject> m_Weapons = new List<GameObject>();
    private GameObject m_CurrentWeapon;

    private void Start() {
        if (m_HotbarUI) {
            m_HotbarUI.OnSelectionDeleted += OnItemDeleted;
            m_HotbarUI.OnCurrentSelectionChanged += UpdateHand;
        }

        foreach (var data in m_WeaponsData) {
            var obj = Instantiate(data.ObjectReference, m_Arm.transform);
            obj.transform.localPosition = new Vector3(0, 0.5f, 0);
            m_Weapons.Add(obj);
            obj.SetActive(false);
            obj.GetComponent<Pickup>().enabled = false;
            obj.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    private void UpdateHand(InventoryItemData data, int index) {
        var weapon = m_WeaponsData.Find((e) => e == data);
        if (weapon) {
            var newObject = m_Weapons[m_WeaponsData.IndexOf(weapon)];
            newObject.SetActive(true);
            m_CurrentWeapon = newObject;
            return;
        }
        if (m_CurrentWeapon) {
            m_CurrentWeapon.SetActive(false);
        }
    }

    private void OnItemDeleted(InventoryItemData data, int index) {
        if (m_CurrentWeapon) {
            m_CurrentWeapon.SetActive(false);
            Instantiate(data.ObjectReference, GameManager.Instance.Player.transform.position, new Quaternion(0, 0, 0, 0));
        }
    }
}
