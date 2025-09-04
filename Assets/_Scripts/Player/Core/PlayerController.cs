using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public PlayerInputs playerInput { get; private set; }
    public PlayerInputs.PlayerActions playerActions { get; private set; }

    private void Awake()
    {
        playerInput = new PlayerInputs();
        playerActions = playerInput.Player;
    }

    private void OnEnable()
    {
        if (Player.Instance.pause)
            return;

        playerInput.Enable();
    }

    private void OnDisable()
    {
        if (Player.Instance.pause)
            return;

        playerInput.Disable();
    }
}
