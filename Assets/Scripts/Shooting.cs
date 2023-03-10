﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    //public Rigidbody rb;
    [SerializeField] private GameObject _laserPrefab;
    [SerializeField] private GameObject _triplePrefab;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private GameObject _muzzleFlash;
    [SerializeField] private GameObject _LeftWingFlash;
    [SerializeField] private GameObject _RightWingFlash;
    
    [SerializeField] private bool _hasTripleShot = false;
    [SerializeField] private float _bulletWaitTime = 0.25f;
    [SerializeField] private bool _canShoot = true;

    private float _powerUpWaitTimer;

    void Update()
    {
        if (_canShoot && Input.GetButton("Fire1")) Shoot();
    }

    public void Shoot()
    {
        if (!_hasTripleShot)
        {
            _muzzleFlash.GetComponent<ParticleSystem>().Play();
            Instantiate(_laserPrefab, _firePoint.position, Quaternion.identity);
        }
        else 
        {
            _muzzleFlash.GetComponent<ParticleSystem>().Play();
            _LeftWingFlash.GetComponent<ParticleSystem>().Play();
            _RightWingFlash.GetComponent<ParticleSystem>().Play();
            Instantiate(_triplePrefab, transform.position, Quaternion.identity);
        }
        _canShoot = false;
        StartCoroutine(BulletWaitTimer());
    }

    IEnumerator BulletWaitTimer()
    { 
        yield return new WaitForSeconds(_bulletWaitTime);
        _canShoot = true;
    }

    public void TripleShotActive(float timer)
    {
        _hasTripleShot= true;
        
        StartCoroutine(PowerUpTimer(timer));
    }

    IEnumerator PowerUpTimer(float timer)
    {
        yield return new WaitForSeconds(timer);
        _hasTripleShot= false;
    }
}
