using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject scr_StartGame;
    [SerializeField] private GameObject scr_GamePlay;
    [SerializeField] private GameObject scr_GameOver;
    [SerializeField] private Button btn_start;
    [SerializeField] private Button btn_restart;
    [SerializeField] private Joystick joystick_Movement;
    [SerializeField] private Joystick joystick_Rotation;
    [SerializeField] private TMP_Text txt_GameTimeValue;

    public Joystick CurrentMovementJoystick { get => joystick_Movement; }
    public Joystick CurrentRotationJoystick { get => joystick_Rotation; }
    public void SetScreen(UIScreenType type)
    {
        scr_StartGame.SetActive(type == UIScreenType.StartScreen);
        scr_GamePlay.SetActive(type == UIScreenType.GamePlayScreen);
        scr_GameOver.SetActive(type == UIScreenType.GameOverScreen);
    }

    public void UpdateGameTimeValue(float timeInSec)
    {
        txt_GameTimeValue.text =
            (timeInSec > 60f ? Mathf.FloorToInt(timeInSec / 60f).ToString() + "m " : "")
            + Mathf.RoundToInt(timeInSec) % 60f + "s";
    }

    public void SubscribeStartAction(UnityAction action) 
    {
        btn_start.onClick.AddListener(action);
    }

    public void SubscribeRestartAction(UnityAction action)
    {
        btn_restart.onClick.AddListener(action);
    }

    //Unsubscribe actions
}
