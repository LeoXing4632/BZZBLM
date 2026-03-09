using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public Weapon weapon; // 指向当前武器

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
        {
            PlayerWeaponController pwc = other.GetComponent<PlayerWeaponController>();
            if (pwc != null && weapon != null)
            {
                pwc.PickWeapon(weapon);  // 传入正确 Weapon
            }
        }
    }
}