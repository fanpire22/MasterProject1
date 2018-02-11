using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainedBolts : WeaponBase
{

    [Header("BoltAttributes")]
    [SerializeField] Proyectile prefBolt;
    [SerializeField] float _forceArm;
    [SerializeField] AudioClip _sfxThrow;

    private void Awake()
    {
        base.AddAmmo(60);
    }

    /// <summary>
    /// Función para disparar, heredada de WeaponBase. Lanzamos una daga hacia adelante
    /// </summary>
    protected override void OnShoot()
    {
        if (_sfxThrow) AudioSource.PlayClipAtPoint(_sfxThrow, transform.position);
        Proyectile bola = Instantiate(prefBolt, transform.position, transform.rotation);

        Vector3 direction = (transform.forward) * _forceArm;

        bola.damage = base._damage;
        bola.Throw(direction);

    }

    /// <summary>
    /// Función para disparar, heredada de WeaponBase
    /// </summary>
    protected override void OnSecondAction()
    {

    }
}