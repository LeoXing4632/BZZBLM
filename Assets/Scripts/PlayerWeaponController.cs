using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerWeaponController : MonoBehaviour
{
    public Transform weaponHolder;
    public Weapon currentWeapon;
    public Weapon startWeapon;

    public TMP_Text ammoText;

    void Start()
    {
        if (startWeapon != null)
        {
            PickWeapon(startWeapon);
        }
    }

    void Update()
    {
        if (currentWeapon != null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                currentWeapon.Shoot();
                UpdateAmmoUI();
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                currentWeapon.Reload();
                UpdateAmmoUI();
            }
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            DropWeapon();
        }

        UpdateAmmoUI();
    }

    public void PickWeapon(Weapon weapon)
    {
        if (weapon == null)
        {
            Debug.LogError("PickWeapon: weapon is null!");
            return;
        }

        if (weaponHolder == null)
        {
            Debug.LogError("PickWeapon: weaponHolder is not assigned!");
            return;
        }

        currentWeapon = weapon;

        Rigidbody rb = weapon.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            rb.isKinematic = true;
            rb.useGravity = false;
        }

        StartCoroutine(MoveWeaponToHand(weapon));
    }

    void DropWeapon()
    {
        if (currentWeapon == null) return;

        Weapon weaponToDrop = currentWeapon;
        currentWeapon = null;

        weaponToDrop.transform.SetParent(null);

        Rigidbody rb = weaponToDrop.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.isKinematic = false;
            rb.useGravity = true;

            rb.AddForce(transform.forward * 4f, ForceMode.Impulse);
        }
    }

    IEnumerator MoveWeaponToHand(Weapon weapon)
    {
        if (weapon == null || weaponHolder == null) yield break;

        weapon.transform.SetParent(weaponHolder);

        Vector3 startPos = weapon.transform.position;
        Vector3 endPos = weaponHolder.position;

        Quaternion startRot = weapon.transform.rotation;
        Quaternion endRot = weaponHolder.rotation;

        float t = 0;

        while (t < 1f)
        {
            t += Time.deltaTime * 5f;

            weapon.transform.position = Vector3.Lerp(startPos, endPos, t);
            weapon.transform.rotation = Quaternion.Slerp(startRot, endRot, t);

            yield return null;
        }

        weapon.transform.localPosition = Vector3.zero;
        weapon.transform.localRotation = Quaternion.identity;
    }

    void UpdateAmmoUI()
    {
        if (ammoText == null) return;

        if (currentWeapon != null)
        {
            ammoText.text = currentWeapon.currentAmmo + " / " + currentWeapon.reserveAmmo;
        }
        else
        {
            ammoText.text = "0 / 0";
        }
    }
}