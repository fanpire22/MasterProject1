using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Damageable
{

    [SerializeField] private GameObject _prefMuerte;
    [SerializeField] private float _timePrefMuerte;
    [SerializeField] private AudioClip _sfxMuerte;

    protected override void OnDead()
    {

        if (_sfxMuerte)
        {
            AudioSource.PlayClipAtPoint(_sfxMuerte, transform.position);
        }
        if (_prefMuerte)
        {
            GameObject muerte = Instantiate(_prefMuerte);
            muerte.transform.position = transform.position;
            if (_timePrefMuerte > 0)
            {
                Destroy(muerte, _timePrefMuerte);
                gameObject.SetActive(false);
                Destroy(gameObject, _timePrefMuerte);
            }

        }
        else
        {
            Destroy(gameObject);
        }

    }


}
