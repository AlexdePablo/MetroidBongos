using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class VerticalPlatform : MonoBehaviour
{
    public PlayerControls m_PlayerControls;
    private PlatformEffector2D myPlatformEffector;


    private void Awake()
    {
        myPlatformEffector = GetComponent<PlatformEffector2D>();
        m_PlayerControls = new PlayerControls();
        m_PlayerControls.Player.A4.started += Agachadito;
        m_PlayerControls.Player.A4.canceled += YaNoAgachadito;
        m_PlayerControls.Player.Enable();
    }

    private void YaNoAgachadito(InputAction.CallbackContext obj)
    {
        // El platform effector se queda recto
        myPlatformEffector.rotationalOffset = 0f;
    }

    private void Agachadito(InputAction.CallbackContext obj)
    {
        // Invertir platform effector
        myPlatformEffector.rotationalOffset = 180f;
    }
}
