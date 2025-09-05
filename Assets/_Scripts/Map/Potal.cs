using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class Potal : MonoBehaviour
{
    [SerializeField] private GameObject fadeMoveObject;
    public string targetMapName;
    public Transform destination;
    public GameObject upKey;
    private bool canTransport = false;
    private PlayerStatus playerStatus;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            upKey.SetActive(true);
            canTransport = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            upKey.SetActive(false);
            canTransport = false;
        }
    }

    void Update()
    {
        if (canTransport && Input.GetKeyDown(KeyCode.E))
        {
            if (fadeMoveObject != null)
            {
                fadeMoveObject.SetActive(true); 
            }
            if (targetMapName == "VillageMap")
            {
                Player.Instance.ResetAllStatHealth();
                Debug.Log("플레이어 현재 공격력 "+ Player.Instance.playerstat.AttackPower);
                Debug.Log("플레이어 현재 체력 " + Player.Instance.currentHealth);
            }
            MapManager.Instance.TransitionToMap(targetMapName, destination.position);
        }
    }
}
