using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField] private TextMeshProUGUI _pistolAmmoCountText;
    [SerializeField] private TextMeshProUGUI _automaticRifleAmmoCountText;
    [SerializeField] private TextMeshProUGUI _shotgunAmmoCountText;

    private int _currentPistolAmmo;
    private int _currentAutomaticRifleAmmo;
    private int _currentShotgunAmmo;
    private int _maxPistolAmmo;
    private int _maxAutomaticRifleAmmo;
    private int _maxShotgunAmmo;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        _currentPistolAmmo = 0;
        _currentAutomaticRifleAmmo = 0;
        _currentShotgunAmmo = 0;
        _maxPistolAmmo = 90;
        _maxAutomaticRifleAmmo = 120;
        _maxShotgunAmmo = 60;
    }

    public void UpdateAmmoCount(AmmoType ammoType, int amount)
    {
        if (ammoType == AmmoType.PistolAmmo)
        {
            _currentPistolAmmo += amount;
            _currentPistolAmmo = Mathf.Min(_currentPistolAmmo, _maxPistolAmmo);
            _pistolAmmoCountText.SetText(_currentPistolAmmo.ToString());
        }
        else if (ammoType == AmmoType.AutomaticRifleAmmo)
        {
            _currentAutomaticRifleAmmo += amount;
            _currentAutomaticRifleAmmo = Mathf.Min(_currentAutomaticRifleAmmo, _maxAutomaticRifleAmmo);
            _automaticRifleAmmoCountText.SetText(_currentAutomaticRifleAmmo.ToString());

        }
        else if (ammoType == AmmoType.ShotgunAmmo)
        {
            _currentShotgunAmmo += amount;
            _currentShotgunAmmo = Mathf.Min(_currentShotgunAmmo, _maxShotgunAmmo);
            _shotgunAmmoCountText.SetText(_currentShotgunAmmo.ToString());
        }
    }
}
