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
        for (int i = 0; i < m_ItemsInRange.Count; i++) {
            var item = m_ItemsInRange[i];
            var pickup = item.GetComponent<IPickupable>();
            if (pickup == null) {
                continue;
            }

            var itemWithData = item.GetComponent<Item>();
            if (itemWithData) {
                pickup.OnPickup();

                var newItem = Inventory.Instance.Add(itemWithData.InventoryItemData);
                if(newItem != null) {
                    Destroy(item);
                    i--;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        m_ItemsInRange.Add(collision.gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision) {
        m_ItemsInRange.Remove(collision.gameObject);
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position, m_PickupRange);
    }
}