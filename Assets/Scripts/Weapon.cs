using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int maxAmmo = 30;        // 弹夹容量
    public int currentAmmo = 30;    // 当前子弹
    public int reserveAmmo = 90;    // 备用子弹

    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletForce = 20f;

    public void Shoot()
    {
        if (currentAmmo <= 0) return;

        currentAmmo--;

        if (bulletPrefab != null && firePoint != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(firePoint.forward * bulletForce, ForceMode.Impulse);
            }
        }
    }

    public void Reload()
    {
        int neededAmmo = maxAmmo - currentAmmo;

        if (reserveAmmo <= 0 || neededAmmo <= 0) return;

        int ammoToLoad = Mathf.Min(neededAmmo, reserveAmmo);

        currentAmmo += ammoToLoad;
        reserveAmmo -= ammoToLoad;
    }
}