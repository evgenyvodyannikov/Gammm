using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatabaseManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private IEnumerator Send()
    {
        WWWForm form = new WWWForm();
        form.AddField("22", "");
        WWW wWW = new WWW("", form);
        yield return wWW;
        if (wWW.error != null)
        {
            Debug.Log("Сервер ответил: " + wWW.error);
            yield break;
        }
        Debug.Log("Сервер ответил: " + wWW.text);
    }
}
