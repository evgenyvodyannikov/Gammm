using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameManager : MonoBehaviour
{
    public GameObject ActiveFrame;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ActiveFrame.SetActive(true); 
        }
    }

    private void OnTriggerExit(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ActiveFrame.SetActive(false);
        }
    }
}
