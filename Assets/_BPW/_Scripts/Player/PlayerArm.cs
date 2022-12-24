using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerArm : MonoBehaviour
{

    [SerializeField] private float m_RotOffset = 90;
    [SerializeField] private bool m_FlipOnHalf = true;
    [SerializeField] private SpriteRenderer m_Renderer;

    private PlayerController m_Controller;

    private void Start() {
        //m_Renderer = GetComponent<SpriteRenderer>();
        m_Controller = GameManager.Instance.Player.GetComponent<PlayerController>();
        m_Controller.OnFlipped += OnFlipped;
    }

    private void OnFlipped(bool shouldFlip) {
        m_Renderer.flipY = !shouldFlip;
        m_Renderer.flipX = !shouldFlip;
    }

    public void OnCursorMove(InputValue value) {
        var mousePos = value.Get<Vector2>();
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mousePos);

        var signedRot = Mathf.Atan2(transform.position.y - mouseWorldPosition.y,
                                    transform.position.x - mouseWorldPosition.x) * Mathf.Rad2Deg + m_RotOffset;

        transform.rotation = Quaternion.Euler(new(0f, 0f, signedRot));
    }
}