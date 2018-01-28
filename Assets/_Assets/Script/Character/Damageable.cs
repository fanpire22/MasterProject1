using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{

    [Header("Propiedades básicas Damageable")]
    [SerializeField] private int _maxLife;
    [SerializeField] private int _armor;
    [SerializeField] bool _GodMode = false;
    [SerializeField] GameObject _prefBulletHole;

    public bool IsDead { get; private set; }

    private int _life;

    protected virtual void Start()
    {
        _life = _maxLife;
    }

    public GameObject GetOverrideBulletHole()
    {
        if (_prefBulletHole) return _prefBulletHole; else return null;
    }

    /// <summary>
    /// Función que recibe daño de una fuente externa
    /// </summary>
    /// <param name="damage">Daño recibido de forma externa</param>
    /// <param name="damageType">Tipo de daño recibido (explosivo, balístico...). Por defecto, no importa</param>
    /// <returns>True = Destruído, False = Sigue vivo</returns>
    public virtual bool GetDamage(int damage, int damageType)
    {
        if (_GodMode) return false;

        //Hacemos un daño mínimo de 1
        _life -= Mathf.Max((damage - _armor),1);

        if (_life < 1)
        {
            IsDead = true;
            OnDead();
        }

        return _life < 1;
    }

    protected virtual void OnDead()
    {
        Destroy(this.gameObject);
    }

    public bool Heal(int amount)
    {
        //Curamos hasta un máximo de nuestra vida máxima
        bool haCambiado = _life != _maxLife;
        _life = Mathf.Min((_life + amount), _maxLife);
        return haCambiado;
    }

    public int AddArmor(int amount)
    {
        //añadimos una cantidad a nuestra armadura y devolvemos el valor final para que el HUD se actualice
        _armor += amount;
        return _armor;
    }
}
