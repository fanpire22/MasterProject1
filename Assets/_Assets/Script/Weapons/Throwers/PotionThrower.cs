using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionThrower : WeaponBase
{

    [Header("Potion")]
    [SerializeField] PotionProyectile prefGrenade;
    [SerializeField] float _forceArm;
    [SerializeField] float _forceUp;
    
    private void Awake()
    {
        base.AddAmmo(5);
    }

    /// <summary>
    /// Función para disparar, heredada de WeaponBase. Lanzamos una poción al aire que explotará al cabo de un rato o al tocar algo
    /// </summary>
    protected override void OnShoot()
    {
        PotionProyectile pocion = Instantiate(prefGrenade, transform.position, transform.rotation);

        Vector3 direction = (transform.forward + Vector3.up * _forceUp) * _forceArm;

        pocion.damage = base._damage;
        pocion.Throw(direction);
        
    }

    /// <summary>
    /// Función para disparar, heredada de WeaponBase
    /// </summary>
    protected override void OnSecondAction()
    {

    }
}
