using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI;

public interface IInputHandler
{
    public void OnInputEnabled();
    public void OnInputDisabled();
}

[RequireComponent(typeof(PlayerInput))]
public class InputManager : MonoBehaviour
{
    public static InputManager Instance {
        get {
            if (!s_Instance) {
                s_Instance = new InputManager();
            }
            return s_Instance;
        }
    }
    private static InputManager s_Instance;

    private static List<PlayerInput> m_PlayerInput = new List<PlayerInput>();
    private static List<PlayerInput> m_UIInput = new List<PlayerInput>();
    private void Start() {
        var playerInputs = FindObjectsOfType<PlayerInput>();
        m_PlayerInput = playerInputs.Where((e) => e.defaultActionMap == "Player").ToList();
        m_UIInput = playerInputs.Where((e) => e.defaultActionMap == "UI").ToList();
        SetPlayerInputActive(true);
    }

    public void SetPlayerInputActive(bool value) {
        if (value) {
            m_PlayerInput.ForEach((e) => {
                e.ActivateInput();
                e.gameObject.GetComponentsInChildren<IInputHandler>().ToList().ForEach((input)=>input.OnInputEnabled());
            });
            return;
        }

        m_PlayerInput.ForEach((e) => {
            e.DeactivateInput();
            e.gameObject.GetComponentsInChildren<IInputHandler>().ToList().ForEach((input) => input.OnInputDisabled());
        });
    }

    public void SetUIInputActive(bool value) {
        if (value) {
            m_UIInput.ForEach((e) => {
                e.ActivateInput();
                e.gameObject.GetComponentsInChildren<IInputHandler>().ToList().ForEach((input) => input.OnInputEnabled());
            });
            return;
        }

        m_UIInput.ForEach((e) => {
            e.DeactivateInput();
            e.gameObject.GetComponentsInChildren<IInputHandler>().ToList().ForEach((input) => input.OnInputDisabled());
        });

    }

}
