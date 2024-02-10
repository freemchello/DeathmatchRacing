using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField] private Button _signInButton;
    [SerializeField] private Button _createAccountButton;
    [SerializeField] private Button _backToMenuButton;
    [SerializeField] private GameObject _backToMenuPrefab;
    [SerializeField] private GameObject _enterInGamePrefab;
    [SerializeField] private GameObject _createAccountPrefab;
    [SerializeField] private GameObject _signInPrefab;

    private void Start()
    {
        _signInButton.onClick.AddListener(OpenSignInWindow);
        _createAccountButton.onClick.AddListener(OpenCreateAccountWindow);
        _backToMenuButton.onClick.AddListener(BackToMenu);
    }

    public void OpenSignInWindow()
    {

        _createAccountPrefab.gameObject.SetActive(false);
        _signInPrefab.gameObject.SetActive(true);
        _enterInGamePrefab.gameObject.SetActive(false);
        _backToMenuPrefab.gameObject.SetActive(true);
    }

    public void OpenCreateAccountWindow()
    {

        _createAccountPrefab.gameObject.SetActive(true);
        _signInPrefab.gameObject.SetActive(false);
        _enterInGamePrefab.gameObject.SetActive(false);
        _backToMenuPrefab.gameObject.SetActive(true);
    }

    public void BackToMenu()
    {

        _createAccountPrefab.gameObject.SetActive(false);
        _signInPrefab.gameObject.SetActive(false);
        _enterInGamePrefab.gameObject.SetActive(true);
        _backToMenuPrefab.gameObject.SetActive(false);
    }
}