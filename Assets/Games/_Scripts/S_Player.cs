using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Player : MonoBehaviour
{
    [SerializeField] private int _lifePoints = 200;

    [SerializeField] private GameObject _activeGun;
    [SerializeField] private int _activeGunIndex = 0;
    [SerializeField] private List<ScriptableObject> _guns;

    [SerializeField] private GameObject _bullet;



    void Start()
    {

    }

    void Update()
    {

    }


    public void AddLifePoints(int lifePoints)
    {
        _lifePoints += lifePoints;
    }

    public void TakeDamage(int damage)
    {
        _lifePoints -= damage;

        if (_lifePoints <= 0)
        {
            Die();
        }
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
    }

    public void Shoot(GameObject cam)
    {
        GameObject canon = _activeGun.GetComponent<S_Guns>().canon;
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Vector3 hitPoint = hit.point;
            Vector3 hitNormal = hit.normal;

            Quaternion canonRotation = Quaternion.LookRotation(hitPoint - _activeGun.transform.position, hitNormal);

            GameObject bullet = Instantiate(_bullet, canon.transform.position, canonRotation);
        }

    }

}
