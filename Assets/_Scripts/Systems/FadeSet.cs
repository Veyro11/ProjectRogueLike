using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeSet : MonoBehaviour
{
    public GameObject panel;

    public void OnFade()
    {
        panel.SetActive(false);
    }
}
