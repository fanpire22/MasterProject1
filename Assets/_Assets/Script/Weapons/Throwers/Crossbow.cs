using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crossbow : WeaponBase
{

    [Header("CrossbowAttributes")]
    [SerializeField]
    Bolt prefBolt;
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
        Bolt virote = Instantiate(prefBolt, transform.position, transform.rotation);

        Vector3 direction = (transform.forward) * _forceArm;

        virote.damage = base._damage;
        virote.Throw(direction);

    }

    /// <summary>
    /// Función para disparar, heredada de WeaponBase
    /// </summary>
    protected override void OnSecondAction()
    {

    }
}
