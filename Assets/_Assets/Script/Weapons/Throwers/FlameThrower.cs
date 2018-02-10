using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrower : WeaponBase {

    [SerializeField] GameObject _prefFire;
    [SerializeField] GameObject _prefSmoke;
    [SerializeField] AudioSource _sfx;

    //Lista de objetivos que van a recibir daño
    List<Damageable> objetivos = new List<Damageable>();

    private void Awake()
    {
        base.AddAmmo(50);
    }

    private void OnTriggerEnter(Collider other)
    {
        Damageable dmg = other.GetComponent<Damageable>();
        if (dmg)
            objetivos.Add(dmg);
    }

    private void OnTriggerExit(Collider other)
    {

        Damageable dmg = other.GetComponent<Damageable>();
        if (dmg)
            objetivos.Remove(dmg);
    }

    /// <summary>
    /// Función para disparar, heredada de WeaponBase
    /// </summary>
    protected override void OnShoot()
    {
        RefreshParticles(true);
        Invoke("DisableParticles",0.5f);
        List<Damageable> ABorrar = new List<Damageable>();
        for(int i = 0; i < objetivos.Count; i++)
        {
            if(objetivos[i].GetDamage(base._damage, 2) <= 0)
                ABorrar.Add(objetivos[i]);
        }

        for(int i=0; i < ABorrar.Count; i++)
        {
            objetivos.Remove(ABorrar[i]);
        }

    }

    /// <summary>
    /// Función para disparar, heredada de WeaponBase
    /// </summary>
    protected override void OnSecondAction()
    {

    }

    private void DisableParticles()
    {
        RefreshParticles(false);
        objetivos.Clear();
    }

    private void RefreshParticles(bool Active)
    {
        CancelInvoke();
        _prefSmoke.SetActive(Active);
        _prefFire.SetActive(Active);
    }
}
