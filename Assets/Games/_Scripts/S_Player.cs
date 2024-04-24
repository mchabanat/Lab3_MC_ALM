using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Player : MonoBehaviour
{

    [SerializeField] private GameObject _activeGun;
    [SerializeField] private int _activeGunIndex = 0;
    [SerializeField] private List<GameObject> _guns;

    [SerializeField] private GameObject _bullet;

    [SerializeField] private GameObject _gunTransform;

    [SerializeField] private int ammo = 32;

    [SerializeField] private GameObject _HUD;

    [SerializeField] private int _maxHealth = 100;
    private int _currentHealth;
    void Start()
    {
        _activeGunIndex = 0;
        _currentHealth = _maxHealth;
        _HUD.GetComponent<S_HUD>().UpdateAmmo(ammo);
        _HUD.GetComponent<S_HUD>().UpdateHealth(_currentHealth);
    }

    void Update()
    {
        Debug.Log(gameObject.GetComponent<Rigidbody>().mass);
    }

    private void Die()
    {
        Debug.Log("Player died");
    }

    public void changeGun()
    {
        _activeGunIndex++;
        if (_activeGunIndex >= _guns.Count)
        {
            _activeGunIndex = 0;
        }
        Destroy(_activeGun.gameObject);
        _activeGun = Instantiate(_guns[_activeGunIndex], _gunTransform.transform.position, _gunTransform.transform.rotation);
        _activeGun.transform.parent = _gunTransform.transform;
    }

    public void Shoot(GameObject cam)
    {
        if (ammo <= 0)
        {
            return;
        }


        GameObject canon = _activeGun.GetComponent<S_Guns>().canon;
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Vector3 hitPoint = hit.point;
            Vector3 hitNormal = hit.normal;

            Quaternion canonRotation = Quaternion.LookRotation(hitPoint - _activeGun.transform.position, hitNormal);

            GameObject bullet = Instantiate(_bullet, canon.transform.position, canonRotation);
            if (_activeGun.GetComponent<S_Guns>().explosive)
            {
                bullet.GetComponent<S_Projectile>().setExplosive(true);
            }
            if (_activeGun.GetComponent<S_Guns>().bouncy)
            {
                bullet.GetComponent<S_Projectile>().setBouncy(true);
            }

            _activeGun.GetComponent<S_Guns>().gunFire.Play();

            ammo--;
            _HUD.GetComponent<S_HUD>().UpdateAmmo(ammo);
        }
    }

    public void AddAmmo(int ammo)
    {
        this.ammo += ammo;
        _HUD.GetComponent<S_HUD>().UpdateAmmo(ammo);
    }

    public void takeDamage(int damage)
    {
        _currentHealth -= damage;
        _HUD.GetComponent<S_HUD>().UpdateHealth(_currentHealth);
        if (_currentHealth <= 0)
        {
            Die();
        }
    }

}
