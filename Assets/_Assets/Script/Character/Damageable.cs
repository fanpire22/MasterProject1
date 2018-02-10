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
    [SerializeField] GameObject[] _lootTable;
    [SerializeField] float[] _lootChances;

    public bool IsDead { get; private set; }

    private int _life;
    public float Life { get; private set; }

    protected virtual void Start()
    {
        _life = _maxLife;
        Life = 1;
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
    public virtual float GetDamage(int damage, int damageType)
    {
        if (_GodMode) return 1;

        //Hacemos un daño mínimo de 1
        _life -= Mathf.Max((damage - _armor),1);

        if (_life < 1)
        {
            IsDead = true;
            OnDead();
        }

        Life = (float)_life / _maxLife;

        return Life;
    }

    protected virtual void OnDead()
    {
        if (_lootTable.Length > 0)
        {
            //Lo que acabamos de matar puede soltar loot, hacemos tiradas en la tabla de loot.
            for(int i = 0; i < _lootTable.Length; i++)
            {
                if(_lootChances.Length > i)
                {
                    //Normalmente se espera que las tablas estén igualadas, pero si no lo están, no tiramos
                    if (Random.Range(0f, 100f) <= _lootChances[i])
                    {
                        //Ya no seguimos tirando: Spawneamos el objeto en sí en la ubicación de nuestro objeto (respetando la y para que el objeto 
                        //generado no esté bajo tierra) y destruimos el damageable
                        GameObject loot = Instantiate(_lootTable[i]);
                        loot.transform.position = new Vector3(transform.position.x, loot.transform.position.y, transform.position.z);
                        Destroy(this.gameObject);
                        return;
                    }
                }
            }
        }
        Destroy(this.gameObject);
    }

    public virtual bool Heal(int amount)
    {
        //Curamos hasta un máximo de nuestra vida máxima
        bool haCambiado = _life != _maxLife;
        _life = Mathf.Min((_life + amount), _maxLife);
        Life = (float)_life / _maxLife;
        return haCambiado;
    }

    public int AddArmor(int amount)
    {
        //añadimos una cantidad a nuestra armadura y devolvemos el valor final para que el HUD se actualice
        _armor += amount;
        return _armor;
    }
}
