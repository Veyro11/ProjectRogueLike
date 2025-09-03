using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Potal : MonoBehaviour
{
    public string targetMapName;
    public Transform destination;
    public GameObject upKey;
    private bool canTransport = false;

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
        if (canTransport && Input.GetKeyDown(KeyCode.W))
        {
            MapManager.Instance.TransitionToMap(targetMapName, destination.position);
            canTransport = false;
        }
    }
}
