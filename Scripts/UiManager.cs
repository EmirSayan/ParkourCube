using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class UiManager : MonoBehaviour
{
    private Image RedDead;
    public Color targetColor;
    
    void Start()
    {
        GameManager.resetUiManager();
        RedDead = GameObject.FindGameObjectWithTag("RedDead").GetComponent<Image>();
    }

    public void RedDeadScreen()
    {
        if(RedDead != null)
        RedDead.color = targetColor;


    }

}
