using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

public class PickupManager : MonoBehaviour
{
    [SerializeField] private float m_PickupRange = 5f;
    [SerializeField] private CircleCollider2D m_RangeCollider;

    private List<GameObject> m_ItemsInRange = new List<GameObject>();


    private void Start() {
        if (m_RangeCollider) {
            m_RangeCollider.radius = m_PickupRange;
        }
    }
    private void OnInteract(InputValue value) {
        foreach (var item in m_ItemsInRange) {

            var pickup = item.GetComponent<IPickupable>();
            if (pickup != null) {
                pickup.OnPickup();
            }

            var data = item.GetComponent<InventoryItemData>();
            if (data) {
                var invItem = new Inventory.InventoryItem(data.GetData().ID, data.GetData().Image, item);
                InventoryManager.Instance.Inventory.AddItem(invItem);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        m_ItemsInRange.Add(collision.gameObject);
        print("New Item Found");
    }

    private void OnTriggerExit2D(Collider2D collision) {
        m_ItemsInRange.Remove(collision.gameObject);
        print("New Item Lost");

    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position, m_PickupRange);
    }
}