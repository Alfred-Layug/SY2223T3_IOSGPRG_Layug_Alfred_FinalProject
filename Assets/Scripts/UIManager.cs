using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField] private Inventory inventoryScript;
    [SerializeField] private TextMeshProUGUI _pistolAmmoCountText;
    [SerializeField] private TextMeshProUGUI _automaticRifleAmmoCountText;
    [SerializeField] private TextMeshProUGUI _shotgunAmmoCountText;
    [SerializeField] private TextMeshProUGUI _primaryWeaponText;
    [SerializeField] private TextMeshProUGUI _secondaryWeaponText;
    [SerializeField] private TextMeshProUGUI _magazineAmmoCountText;
    [SerializeField] private TextMeshProUGUI _bagAmmoCountText;
    public TextMeshProUGUI _reloadingText;

    private int _currentPistolAmmo;
    private int _currentAutomaticRifleAmmo;
    private int _currentShotgunAmmo;
    private int _maxPistolAmmo;
    private int _maxAutomaticRifleAmmo;
    private int _maxShotgunAmmo;
    public int _currentPistolMagazineAmmo;
    public int _currentPistolBagAmmo;
    public int _currentAutomaticRifleMagazineAmmo;
    public int _currentAutomaticRifleBagAmmo;
    public int _currentShotgunMagazineAmmo;
    public int _currentShotgunBagAmmo;

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
        _currentPistolMagazineAmmo = 0;
        _currentAutomaticRifleMagazineAmmo = 0;
        _currentShotgunMagazineAmmo = 0;
        _primaryWeaponText.SetText("Primary: No Weapon");
        _secondaryWeaponText.SetText("Secondary: No Weapon");
        _reloadingText.enabled = false;
    }

    public void UpdateAmmoCount(AmmoType ammoType, int amount)
    {
        if (ammoType == AmmoType.PistolAmmo)
        {
            _currentPistolAmmo += amount;
            _currentPistolAmmo = Mathf.Min(_currentPistolAmmo, _maxPistolAmmo);
            _pistolAmmoCountText.SetText(_currentPistolAmmo.ToString());
            _currentPistolBagAmmo = _currentPistolAmmo - _currentPistolMagazineAmmo;
            if (!inventoryScript._primaryWeaponSelected && inventoryScript._secondaryWeapon == inventoryScript._gunTypes[0])
            {
                if (_currentPistolMagazineAmmo <= 0)
                {
                    inventoryScript.StartCoroutine("ReloadCurrentGun");
                    _reloadingText.enabled = true;
                }
                _magazineAmmoCountText.SetText(_currentPistolMagazineAmmo.ToString());
                _bagAmmoCountText.SetText(_currentPistolBagAmmo.ToString());
            }
        }
        else if (ammoType == AmmoType.AutomaticRifleAmmo)
        {
            _currentAutomaticRifleAmmo += amount;
            _currentAutomaticRifleAmmo = Mathf.Min(_currentAutomaticRifleAmmo, _maxAutomaticRifleAmmo);
            _automaticRifleAmmoCountText.SetText(_currentAutomaticRifleAmmo.ToString());
            _currentAutomaticRifleBagAmmo = _currentAutomaticRifleAmmo - _currentAutomaticRifleMagazineAmmo;
            if (inventoryScript._primaryWeaponSelected && inventoryScript._primaryWeapon == inventoryScript._gunTypes[1])
            {
                if (_currentAutomaticRifleMagazineAmmo <= 0)
                {
                    inventoryScript.StartCoroutine("ReloadCurrentGun");
                    _reloadingText.enabled = true;
                }
                _magazineAmmoCountText.SetText(_currentAutomaticRifleMagazineAmmo.ToString());
                _bagAmmoCountText.SetText(_currentAutomaticRifleBagAmmo.ToString());
            }
        }
        else if (ammoType == AmmoType.ShotgunAmmo)
        {
            _currentShotgunAmmo += amount;
            _currentShotgunAmmo = Mathf.Min(_currentShotgunAmmo, _maxShotgunAmmo);
            _shotgunAmmoCountText.SetText(_currentShotgunAmmo.ToString());
            _currentShotgunBagAmmo = _currentShotgunAmmo - _currentShotgunMagazineAmmo;
            if (inventoryScript._primaryWeaponSelected && inventoryScript._primaryWeapon == inventoryScript._gunTypes[2])
            {
                if (_currentShotgunMagazineAmmo <= 0)
                {
                    inventoryScript.StartCoroutine("ReloadCurrentGun");
                    _reloadingText.enabled = true;
                }
                _magazineAmmoCountText.SetText(_currentShotgunMagazineAmmo.ToString());
                _bagAmmoCountText.SetText(_currentShotgunBagAmmo.ToString());
            }
        }
    }

    public void UpdateWeaponSlotText(Weapon weapon)
    {
        if (weapon == Weapon.Pistol)
        {
            _secondaryWeaponText.SetText("Secondary: Pistol");
        }
        else if (weapon == Weapon.AutomaticRifle)
        {
            _primaryWeaponText.SetText("Primary: Automatic Rifle");
        }
        else if (weapon == Weapon.Shotgun)
        {
            _primaryWeaponText.SetText("Primary: Shotgun");
        }
    }

    public void UpdateCurrentWeaponAmmoCount(Gun gun, int totalAmmoCount)
    {
        if (gun.GetComponent<Pistol>() != null)
        {
            if (_currentPistolMagazineAmmo <= 0)
            {
                inventoryScript.StartCoroutine("ReloadCurrentGun");
                _currentPistolBagAmmo = totalAmmoCount;
                _magazineAmmoCountText.SetText("0");
                _bagAmmoCountText.SetText(_currentPistolBagAmmo.ToString());
                _reloadingText.enabled = true;
            }
            else
            {
                _magazineAmmoCountText.SetText(_currentPistolMagazineAmmo.ToString());
                _bagAmmoCountText.SetText(_currentPistolBagAmmo.ToString());
            }
        }
        else if (gun.GetComponent<AutomaticRifle>() != null)
        {
            if (_currentAutomaticRifleMagazineAmmo <= 0)
            {
                inventoryScript.StartCoroutine("ReloadCurrentGun");
                _currentAutomaticRifleBagAmmo = totalAmmoCount;
                _magazineAmmoCountText.SetText("0");
                _bagAmmoCountText.SetText(_currentAutomaticRifleBagAmmo.ToString());
                _reloadingText.enabled = true;
            }
            else
            {
                _magazineAmmoCountText.SetText(_currentAutomaticRifleMagazineAmmo.ToString());
                _bagAmmoCountText.SetText(_currentAutomaticRifleBagAmmo.ToString());
            }
        }
        else if (gun.GetComponent<Shotgun>() != null)
        {
            if (_currentShotgunMagazineAmmo <= 0)
            {
                inventoryScript.StartCoroutine("ReloadCurrentGun");
                _currentShotgunBagAmmo = Mathf.Max(0, totalAmmoCount - 2);
                _magazineAmmoCountText.SetText("0");
                _bagAmmoCountText.SetText(_currentShotgunBagAmmo.ToString());
                _reloadingText.enabled = true;
            }
            else
            {
                _magazineAmmoCountText.SetText(_currentShotgunMagazineAmmo.ToString());
                _bagAmmoCountText.SetText(_currentShotgunBagAmmo.ToString());
            }
        }
    }
}
