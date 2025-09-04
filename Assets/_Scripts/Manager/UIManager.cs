using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIManager : SingletonMono<UIManager>
{
    [SerializeField] private List<Transform> UIs;
    ///</numbers>
    ///0: HUD
    ///1: CONFIG
    ///2: GAMEOVER
    ///3: REINFORCEMENT
    ///4: WANNAEXIT
    ///<numbers>
    private InputAction PauseAction;
    private InputAction ConfigAction;

    void OnEnable()
    {
        PauseAction = new InputAction(type: InputActionType.Button, binding: "<Keyboard>/escape");
        PauseAction.performed += ctx => SetPause();
        ConfigAction = new InputAction(type: InputActionType.Button, binding: "<Keyboard>/backquote");
        ConfigAction.performed += ctx => SetConfig();
    }

    private void Start()
    {
        PauseAction.Disable();
        ConfigAction.Disable();
    }

    public void readyInput()
    {
        PauseAction.Enable();
        ConfigAction.Enable();
    }

    public void SetPause()
    {
        UIs[4].gameObject.SetActive(true);
    }

    public void SetConfig()
    {
        UIs[1].gameObject.SetActive(true);
    }    

    public void SetReinforced()
    {
        UIs[3].gameObject.SetActive(true);
    }

    public void SetGameOver()
    {
        UIs[2].gameObject.SetActive(true);
    }

    public void SetHUD()
    {
        UIs[0].gameObject.SetActive(true);
    }
}