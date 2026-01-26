using System.Collections;
using TMPro;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    // Disparar
    public bool isFiring, readytoFire;
    bool allowReset = true;
    public float shootingDelay = 0.5f;

    // Precision del spread
    private float spreadIntensity = 0.1f;

    // Bullet
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float bulletSpeed = 20;
    public float bulletPrefabLifetime = 3f;

    // Efectos
    public GameObject muzzleEffect;
    private Animator animator;

    // Recarga
    public float reloadTime;
    public int magazineSize, bulletsLeft;
    public bool isReloading;

   
    private void Awake()
    {
        readytoFire = true;
        animator = GetComponent<Animator>();
        bulletsLeft = magazineSize;
    }

    void Update()
    {
        if (bulletsLeft == 0 && isFiring)
        {
            SoundManager.instance.emptySoundM107.Play();
        }

        // Arma automática: mantener presionado para disparar
        isFiring = Input.GetMouseButton(0);

        // No disparar si recargando o sin balas
        if (readytoFire && isFiring && !isReloading && bulletsLeft >0)
        {
            FireWeapon();
        }
        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && isReloading == false)
        {
            Reload();
        }
        
        if (AmmoManager.instance.ammoDisplay != null)
        {
            AmmoManager.instance.ammoDisplay.text = bulletsLeft + " / " + magazineSize;
        }
    }

    private void Reload()
    { 
    isReloading = true;
    Invoke("FinishReloading", reloadTime);
    SoundManager.instance.reloadM107.Play();
    }
    private void FinishReloading()
    {
        bulletsLeft = magazineSize;
        isReloading = false;
    }


    private void FireWeapon()
    {   
        if (bulletsLeft == 0 && isFiring)
        {
            SoundManager.instance.emptySoundM107.Play();
        }
        // Si no hay balas o se está recargando, no disparar
        if (isReloading || bulletsLeft <=0)
        {
            return;
        }

        // Consumir una bala
        bulletsLeft--;



        muzzleEffect.GetComponent<ParticleSystem>().Play();
       animator.SetTrigger("RECOIL");
        SoundManager.instance.shootingSoundM107.Play();
        readytoFire = false;

        Vector3 shootingDirection = CalculateDirectionAndSpread().normalized;

        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);
        bullet.transform.forward = shootingDirection;
        bullet.GetComponent<Rigidbody>().AddForce(shootingDirection * bulletSpeed, ForceMode.Impulse);

        StartCoroutine(DestroyBulletAfterTime(bullet, bulletPrefabLifetime));

        if (allowReset)
        {
            Invoke("ResetShot", shootingDelay);
            allowReset = false;
        }
    }

    private void ResetShot()
    {
        readytoFire = true;
        allowReset = true;
    }

    public Vector3 CalculateDirectionAndSpread()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit))
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = ray.GetPoint(100);
        }

        Vector3 direction = targetPoint - bulletSpawn.position;
        float x = UnityEngine.Random.Range(-spreadIntensity, spreadIntensity);
        float y = UnityEngine.Random.Range(-spreadIntensity, spreadIntensity);
        return direction + new Vector3(x, y, 0);
    }

    private IEnumerator DestroyBulletAfterTime(GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(bullet);
    }
}


