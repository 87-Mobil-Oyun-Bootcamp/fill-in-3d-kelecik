using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public GameObject taptoplayText;


    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && GameManager.instance.gameStatus == GameStatus.PLAYABLE)
        {
            GameManager.instance.gameStatus = GameStatus.PLAYING;
        }
        TapToPlay();
    }

    void TapToPlay()
    {
        if (GameManager.instance.gameStatus == GameStatus.PLAYABLE)
        {
            taptoplayText.SetActive(true);
        }
        else
        {
            taptoplayText.SetActive(false);
        }   
    }
}
