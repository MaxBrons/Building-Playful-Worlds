using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public class Pickup : MonoBehaviour, IPickupable
{
    public Action OnPickedUp;

    private void Start() {
        if (!GetComponent<Collider2D>()) {
            Debug.LogError($"No collider found on pickupable Item, on Object: {gameObject}.");
        }   
    }

    public void OnPickup() {
        OnPickedUp?.Invoke();
    }
}

public interface IPickupable
{
    public void OnPickup();
}