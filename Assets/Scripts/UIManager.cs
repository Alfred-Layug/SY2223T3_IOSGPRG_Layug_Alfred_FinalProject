using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _pistolAmmoCountText;
    [SerializeField] private TextMeshProUGUI _automaticRifleAmmoCountText;
    [SerializeField] private TextMeshProUGUI _shotgunAmmoCountText;

    private int _currentPistolAmmo;
    private int _currentAutomaticRifleAmmo;
    private int _currentShotgunAmmo;

    private void Start()
    {
        _currentPistolAmmo = 0;
        _currentAutomaticRifleAmmo = 0;
        _currentShotgunAmmo = 0;
    }

    private void Update()
    {
        
    }

    public void UpdatePistolAmmoCount(int amount)
    {
        _currentPistolAmmo += amount;
        _pistolAmmoCountText.SetText(_currentPistolAmmo.ToString());
    }

    public void UpdateAutomaticRifleAmmoCount(int amount)
    {
        _currentAutomaticRifleAmmo += amount;
        _automaticRifleAmmoCountText.SetText(_currentAutomaticRifleAmmo.ToString());
    }

    public void UpdateShotgunAmmoCount(int amount)
    {
        _currentShotgunAmmo += amount;
        _shotgunAmmoCountText.SetText(_currentShotgunAmmo.ToString());
    }
}
