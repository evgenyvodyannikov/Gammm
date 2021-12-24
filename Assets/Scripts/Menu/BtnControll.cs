using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //������ � ������������
using UnityEngine.SceneManagement; //������ �� �������
using UnityEngine.Audio; //������ � �����

public class BtnControll : MonoBehaviour
{


    public GameObject settings;



    // Start is called before the first frame update
    void Start()
    {
        settings.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoToSettings()
    {
        settings.SetActive(true);
    }

    public void GoToMenu()
    {
        settings.SetActive(false);
    }
}

