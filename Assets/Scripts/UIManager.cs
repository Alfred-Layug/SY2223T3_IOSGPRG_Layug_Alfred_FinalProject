using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField] private Inventory _inventoryScript;
    [SerializeField] private TextMeshProUGUI _pistolAmmoCountText;
    [SerializeField] private TextMeshProUGUI _automaticRifleAmmoCountText;
    [SerializeField] private TextMeshProUGUI _shotgunAmmoCountText;
    [SerializeField] private TextMeshProUGUI _primaryWeaponText;
    [SerializeField] private TextMeshProUGUI _secondaryWeaponText;
    [SerializeField] private TextMeshProUGUI _magazineAmmoCountText;
    [SerializeField] private TextMeshProUGUI _bagAmmoCountText;
    [SerializeField] private TextMeshProUGUI _leftAliveCountText;
    [SerializeField] private Image _retryButton;
    public TextMeshProUGUI _reloadingText;
    public GameObject _gameplayCanvas;
    public GameObject _gameOverCanvas;

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
    }

    private void Start()
    {
        _primaryWeaponText.SetText("Primary: No Weapon");
        _secondaryWeaponText.SetText("Secondary: No Weapon");
        _reloadingText.enabled = false;
    }

    public void UpdateAmmoCount(Weapon weapon, int totalAmmoCount, int bagAmmoCount, int magazineAmmoCount)
    {
        if (weapon == Weapon.Pistol)
        {
            _pistolAmmoCountText.SetText(totalAmmoCount.ToString());
            if (!_inventoryScript._primaryWeaponSelected &&
                _inventoryScript._secondaryWeapon == _inventoryScript._gunTypes[(int)Weapon.Pistol])
            {
                if (magazineAmmoCount <= 0 && _inventoryScript._player._currentGun ==
                    _inventoryScript._gunTypes[(int)Weapon.Pistol])
                {
                    _inventoryScript.StartCoroutine("ReloadCurrentGun");
                    _reloadingText.enabled = true;
                }
                _magazineAmmoCountText.SetText(magazineAmmoCount.ToString());
                _bagAmmoCountText.SetText(bagAmmoCount.ToString());
            }
        }
        else if (weapon == Weapon.AutomaticRifle)
        {
            _automaticRifleAmmoCountText.SetText(totalAmmoCount.ToString());
            if (_inventoryScript._primaryWeaponSelected &&
                _inventoryScript._primaryWeapon == _inventoryScript._gunTypes[(int)Weapon.AutomaticRifle])
            {
                if (magazineAmmoCount <= 0 && _inventoryScript._player._currentGun ==
                    _inventoryScript._gunTypes[(int)Weapon.AutomaticRifle])
                {
                    _inventoryScript.StartCoroutine("ReloadCurrentGun");
                    _reloadingText.enabled = true;
                }
                _magazineAmmoCountText.SetText(magazineAmmoCount.ToString());
                _bagAmmoCountText.SetText(bagAmmoCount.ToString());
            }
        }
        else if (weapon == Weapon.Shotgun)
        {
            _shotgunAmmoCountText.SetText(totalAmmoCount.ToString());
            if (_inventoryScript._primaryWeaponSelected &&
                _inventoryScript._primaryWeapon == _inventoryScript._gunTypes[(int)Weapon.Shotgun])
            {
                if (magazineAmmoCount <= 0 && _inventoryScript._player._currentGun ==
                    _inventoryScript._gunTypes[(int)Weapon.Shotgun])
                {
                    _inventoryScript.StartCoroutine("ReloadCurrentGun");
                    _reloadingText.enabled = true;
                }
                _magazineAmmoCountText.SetText(magazineAmmoCount.ToString());
                _bagAmmoCountText.SetText(bagAmmoCount.ToString());
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
        else if (weapon == Weapon.RocketLauncher)
        {
            _primaryWeaponText.SetText("Primary: Rocket Launcher");
        }
    }

    public void UpdateCurrentWeaponAmmoCount(int magazineAmmoCount, int bagAmmoCount)
    {
        if (magazineAmmoCount <= 0)
        {
            _inventoryScript.StartCoroutine("ReloadCurrentGun");
            _magazineAmmoCountText.SetText("0");
            _bagAmmoCountText.SetText(bagAmmoCount.ToString());
            _reloadingText.enabled = true;
        }
        else
        {
            _magazineAmmoCountText.SetText(magazineAmmoCount.ToString());
            _bagAmmoCountText.SetText(bagAmmoCount.ToString());
        }
    }

    public void UpdateLeftAliveText(int amount)
    {
        _leftAliveCountText.SetText(amount.ToString());
    }

    public void RestartMatch()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
