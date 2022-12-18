using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    
    public Inventory Inventory { get; private set; }

    private void Start() {
        Instance = Instance != null ? Instance : this;
        Inventory = new Inventory();
    }
}
