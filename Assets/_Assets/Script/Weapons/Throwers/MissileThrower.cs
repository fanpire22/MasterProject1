using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileThrower : WeaponBase
{

    [Header("MagicMissileAttributes")]
    [SerializeField]
    MagicMissile prefMissile;

    private void Awake()
    {
        base.AddAmmo(5);
    }

    /// <summary>
    /// Función para disparar, heredada de WeaponBase. Lanzamos una daga hacia adelante
    /// </summary>
    protected override void OnShoot()
    {
        MagicMissile misil = Instantiate(prefMissile, transform.position, transform.rotation);

        misil.Damage = base._damage;

    }

    /// <summary>
    /// Función para disparar, heredada de WeaponBase
    /// </summary>
    protected override void OnSecondAction()
    {

    }
}
