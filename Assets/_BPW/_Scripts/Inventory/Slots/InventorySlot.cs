using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class InventorySlot : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI m_Title;
    [SerializeField] protected Image m_Renderer;

    protected Sprite m_Image;
    protected string m_TitleText;

    public virtual void Start() {
        if (!m_Renderer) {
            m_Renderer = GetComponent<Image>();
        }

        if (!m_Title) {
            m_Title = GetComponentInChildren<TextMeshProUGUI>();
        }
    }

    public virtual void Initialize(InventoryItemData data) {
        m_Image = data ? data.Image : null;
        m_TitleText = data ? data.ItemName : "";

        if (m_Renderer) {
            m_Renderer.sprite = m_Image;
        }

        if (m_Title) {
            m_Title.text = m_TitleText.ToString();
        }
    }
}
