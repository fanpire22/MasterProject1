using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{

    //Estas propiedades se heredan
    [Header("Weapon Base")]
    [SerializeField]
    protected int _damage;
    [SerializeField] protected float _RoF = 0.1f;
    [SerializeField] protected float _RoA = 0.1f;
    [SerializeField] private int _maxAmmo;
    [SerializeField] private AudioClip _sfxEmpty;
    [SerializeField] private bool _bInfiniteAmmo = false;
    public Sprite imagen;

    public bool bInventory;

    private float _nextShot;
    private float _nextAction;
    private int _currentAmmo;

    /// <summary>
    /// Este evento se usa para disparar. Tiene un control interno de la cadencia de fuego
    /// </summary>
    protected abstract void OnShoot();

    /// <summary>
    /// Evento para el alt fire de un arma
    /// </summary>
    protected abstract void OnSecondAction();

    public bool AddAmmo(int amount)
    {
        bool haCambiado = _currentAmmo != _maxAmmo;
        _currentAmmo = Mathf.Min(_currentAmmo + amount, _maxAmmo);
        return haCambiado;
    }

    /// <summary>
    /// Función que determina si podemos disparar (y llamar por tanto al evento) o no.
    /// Lee la hora, y si es posible disparar, llama al evento de disparo y además actualiza un valor interno,
    /// al sumarle la hora actual con el Ratio de Fuego (_RoF)
    /// </summary>
    public virtual void Shoot()
    {
        if (Time.time > _nextShot && (_currentAmmo > 0 || _bInfiniteAmmo))
        {
            if (!_bInfiniteAmmo) --_currentAmmo;
            _nextShot = Time.time + _RoF;
            OnShoot();
        }
        else if (_sfxEmpty)
        {
            AudioSource.PlayClipAtPoint(_sfxEmpty, transform.position);
        }
    }

    public int GetCurrentAmmo()
    {
        return _currentAmmo;
    }

    public int GetMaxAmmo()
    {
        return _maxAmmo;
    }
    /// <summary>
    /// Función que determina si podemos realizar la acción secundaria (y llamar por tanto al evento) o no
    /// Lee la hora, y si es posible realizar la acción, llama al evento de acción y además actualiza un valor interno,
    /// al sumarle la hora actual con el Ratio de Acción (_RoA)
    /// </summary>
    public virtual void SecondAction()
    {
        if (Time.time > _nextAction)
        {
            _nextAction = Time.time + _RoA;
            OnSecondAction();
        }
    }

}
