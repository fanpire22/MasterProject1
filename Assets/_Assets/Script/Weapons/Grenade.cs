using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : WeaponBase
{

    [Header("Grenade")]
    [SerializeField] GrenadeProyectile prefGrenade;
    [SerializeField] float _forceArm;
    [SerializeField] float _forceUp;

    // Update is called once per frame
    private void Awake()
    {
        base.AddAmmo(5);
    }

    /// <summary>
    /// Función para disparar, heredada de WeaponBase
    /// </summary>
    protected override void OnShoot()
    {
        GrenadeProyectile granada = Instantiate(prefGrenade, transform.position, transform.rotation);

        Vector3 direction = (transform.forward + Vector3.up * _forceUp) * _forceArm;

        granada.damage = base._damage;
        granada.Throw(direction);
        
    }

    /// <summary>
    /// Función para disparar, heredada de WeaponBase
    /// </summary>
    protected override void OnSecondAction()
    {

    }
}
