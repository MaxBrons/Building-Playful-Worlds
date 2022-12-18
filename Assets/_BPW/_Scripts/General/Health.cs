using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Health: MonoBehaviour
{
    public Action<GameObject,float> OnHealthChanged;
    public Action OnHealthDepleted;

    [SerializeField] private int m_Health = 100;
    private int m_BaseHealth;

    private void Start() {
        m_BaseHealth = m_Health;
    }

    public float IncreaseHealth(int amount) {
        m_Health = (int)Mathf.Clamp(m_Health + amount, 0, 100);
        OnHealthChanged?.Invoke(transform.gameObject, m_Health);
        return m_Health;
    }

    public float DecreaseHealth(int amount) {
        m_Health = (int)Mathf.Clamp(m_Health - amount, 0, 100);
        OnHealthChanged?.Invoke(transform.gameObject, m_Health);

        if(m_Health <= 0) {
            OnHealthDepleted?.Invoke();
        }
        return m_Health;
    }
    
    public void ResetHealth() {
        m_Health = m_BaseHealth;
        OnHealthChanged?.Invoke(transform.gameObject, m_Health);
    }
}
