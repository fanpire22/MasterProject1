using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaggerThrower : WeaponBase {

    [Header("DaggerAttributes")]
    [SerializeField] DaggerProyectile prefDagger;
    [SerializeField] float _forceArm;

    private void Awake()
    {
        base.AddAmmo(30);
    }

    /// <summary>
    /// Función para disparar, heredada de WeaponBase. Lanzamos una daga hacia adelante
    /// </summary>
    protected override void OnShoot()
    {
        DaggerProyectile daga = Instantiate(prefDagger, transform.position, transform.rotation);

        Vector3 direction = (transform.forward) * _forceArm;

        daga.damage = base._damage;
        daga.Throw(direction);

    }

    /// <summary>
    /// Función para disparar, heredada de WeaponBase
    /// </summary>
    protected override void OnSecondAction()
    {

    }
}
