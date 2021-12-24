using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogAniimator : MonoBehaviour
{
    public Animator startAnim;
    public DialogManager dm;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        startAnim.SetBool("IsDialogStarted", true);
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        startAnim.SetBool("IsDialogStarted", false);
    }
}
