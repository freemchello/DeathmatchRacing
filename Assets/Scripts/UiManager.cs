using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField] private Button _signInButton;
    [SerializeField] private Button _createAccountButton;
    [SerializeField] private Button _backToMenuButton;
    [SerializeField] private Canvas _backToMenuCanvas;
    [SerializeField] private Canvas _enterInGameCanvas;
    [SerializeField] private Canvas _createAccountCanvas;
    [SerializeField] private Canvas _signInCanvas;

    private void Start()
    {
        _signInButton.onClick.AddListener(OpenSignInWindow);
        _createAccountButton.onClick.AddListener(OpenCreateAccountWindow);
        _backToMenuButton.onClick.AddListener(BackToMenu);
    }

    public void OpenSignInWindow()
    {
        _createAccountCanvas.enabled = false;
        _signInCanvas.enabled = true;
        _enterInGameCanvas.enabled = false;
        _backToMenuCanvas.enabled = false;

    }

    public void OpenCreateAccountWindow()
    {
        _createAccountCanvas.enabled = true;
        _signInCanvas.enabled = false;
        _enterInGameCanvas.enabled = false;
        _backToMenuCanvas.enabled = true;
    }

    public void BackToMenu()
    {
        _createAccountCanvas.enabled = false;
        _signInCanvas.enabled = false;
        _enterInGameCanvas.enabled = true;
        _backToMenuCanvas.enabled = true;
    }
}