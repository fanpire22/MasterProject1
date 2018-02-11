using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crossbow : WeaponBase
{
    
    [SerializeField] private int _numImpacts;
    [SerializeField] GameObject _bolt;
    [SerializeField] AudioClip _sfxPew;
    private Transform _rayOrigin;

    private void Awake()
    {
        _rayOrigin = transform.Find("Bolt");
        base.AddAmmo(30);
    }

    /// <summary>
    /// Función para disparar, heredada de WeaponBase. Lanzamos una daga hacia adelante
    /// </summary>
    protected override void OnShoot()
    {
        if (_sfxPew) AudioSource.PlayClipAtPoint(_sfxPew, _rayOrigin.position);
        _bolt.SetActive(false);
        Invoke("MuestraVirote", base._RoF);
        Vector3 CenterScreen = Camera.main.ViewportToScreenPoint(new Vector3(.5f, .5f));
        Ray ray = Camera.main.ScreenPointToRay(CenterScreen);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, float.PositiveInfinity))
        {
            Vector3 worldPoint = hit.point;
            Vector3 dirWorldPoint = (worldPoint - _rayOrigin.position).normalized;

            RaycastHit[] hits = Physics.RaycastAll(_rayOrigin.position, dirWorldPoint, 1000);
            
            for (int i = 0; i < hits.Length; i++)
            {
                if (i > _numImpacts-1) break;

                
                Damageable dmg = hits[i].collider.GetComponent<Damageable>();

                if (dmg)
                {
                    dmg.GetDamage(base._damage, 0);
                    break;
                }
            }
        }
    }

    private void MuestraVirote()
    {
        _bolt.SetActive(true);
    }

    /// <summary>
    /// Función para disparar, heredada de WeaponBase
    /// </summary>
    protected override void OnSecondAction()
    {

    }
}
