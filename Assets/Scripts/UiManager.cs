using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField] private Button _signInButton;
    [SerializeField] private Button _createAccountButton;
    [SerializeField] private Button _backToMenuButton;
    [SerializeField] private Canvas _backToMenuPrefab;
    [SerializeField] private Canvas _enterInGamePrefab;
    [SerializeField] private Canvas _createAccountPrefab;
    [SerializeField] private Canvas _signInPrefab;

    private void Start()
    {
        _signInButton.onClick.AddListener(OpenSignInWindow);
        _createAccountButton.onClick.AddListener(OpenCreateAccountWindow);
        _backToMenuButton.onClick.AddListener(BackToMenu);
    }

    public void OpenSignInWindow()
    {
        
        _createAccountPrefab.enabled = (false);
        _signInPrefab.enabled = (true);
        _enterInGamePrefab.enabled = (false);
        _backToMenuPrefab.enabled = (true);

        //_createAccountPrefab.SetActive(false);
        //_signInPrefab.SetActive(true);
        //_enterInGamePrefab.SetActive(false);
        //_backToMenuPrefab.SetActive(true);
    }

    public void OpenCreateAccountWindow()
    {
        _createAccountPrefab.enabled = (true);
        _signInPrefab.enabled = (false);
        _enterInGamePrefab.enabled = (false);
        _backToMenuPrefab.enabled = (true);
        //_createAccountPrefab.SetActive(true);
        //_signInPrefab.SetActive(false);
        //_enterInGamePrefab.SetActive(false);
        //_backToMenuPrefab.SetActive(true);
    }

    public void BackToMenu()
    {
        _createAccountPrefab.enabled = (false);
        _signInPrefab.enabled = (false);
        _enterInGamePrefab.enabled = (true);
        _backToMenuPrefab.enabled = (false);
        //_createAccountPrefab.SetActive(false);
        //_signInPrefab.SetActive(false);
        //_enterInGamePrefab.SetActive(true);
        //_backToMenuPrefab.SetActive(false);
    }
}