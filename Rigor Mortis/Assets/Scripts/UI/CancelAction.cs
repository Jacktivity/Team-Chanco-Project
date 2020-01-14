using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CancelAction : MonoBehaviour
{
    private UIManager uiManager;
    public void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
    }



    public void CancelCurrentAction()
    {
        uiManager.DeleteCurrentPopupButtons();
    }
}
