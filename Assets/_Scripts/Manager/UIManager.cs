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
    private InputAction ReinforceAction;

    void OnEnable()
    {
        PauseAction = new InputAction(type: InputActionType.Button, binding: "<Keyboard>/escape");
        PauseAction.performed += ctx => SetPause();
        ConfigAction = new InputAction(type: InputActionType.Button, binding: "<Keyboard>/backquote");
        ConfigAction.performed += ctx => SetConfig();
        ReinforceAction = new InputAction(type: InputActionType.Button, binding: "<Keyboard>/e");
        ReinforceAction.performed += ctx => SetReinforced();
    }

    private void Start()
    {
        PauseAction.Disable();
        ConfigAction.Disable();
        ReinforceAction.Disable();
    }

    public void readyInput()
    {
        PauseAction.Enable();
        ConfigAction.Enable();
    }

    public void Eenable()
    {
        ReinforceAction.Enable();
    }

    public void Edisable()
    {
        ReinforceAction.Disable();
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
        UIs[3].gameObject.SetActive(true);         // UI 활성화
        Time.timeScale = 0f;                // 게임 시간 멈춤
        Cursor.visible = true;              // 커서 보이기 (필요시)
        Cursor.lockState = CursorLockMode.None;
        if (ReinforceManager.Instance == null)
        {
            Debug.Log("강화매니저 눌참조남");
            return;
        }

        ReinforceManager.Instance.Refresh();
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