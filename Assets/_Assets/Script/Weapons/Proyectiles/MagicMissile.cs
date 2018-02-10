using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicMissile : MonoBehaviour
{

    public AudioClip SfxExplosion;
    public GameObject PrefExplosion;
    public float Fuse;
    public float Radius;
    public int Damage;
    public float Speed;
    public float MinDistanceDetonation;
    public float MinDistanceLook;

    private Enemy _objective;
    private Rigidbody _rig;
    RaycastHit hit;
    Quaternion _destinyOrientation;

    /// <summary>
    /// Ponemos en marcha el contador para explotar
    /// </summary>
    private void Start()
    {
        Invoke("Explode", Fuse);
    }

    private void Awake()
    {
        _rig = transform.root.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (_objective)
        {
            //Rotamos en la dirección del objetivo, intentando alcanzarlo
            Vector3 direction = _objective.transform.position - transform.position;

            transform.rotation = Quaternion.LookRotation(direction.normalized);

            if (MinDistanceDetonation > direction.magnitude) Explode();
        }
        else
        {
            //Aún no tenemos un objetivo en la mira. Buscamos un Enemy dentro del radio de detección
            foreach (Collider c in Physics.OverlapSphere(transform.position, MinDistanceLook))
            {
                //Encontramos al enemigo, lo marcamos como objetivo
                Enemy d = c.GetComponent<Enemy>();
                if (d)
                {
                    _objective = d;
                    return;
                }
            }
        }

        _rig.MovePosition(transform.position + transform.forward.normalized * Speed * Time.deltaTime);

    }

    /// <summary>
    /// El misil explota, independientemente de si ha acertado a su objetivo o no
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        CancelInvoke();
        Explode();
    }

    private void Explode()
    {

        //Dibujamos una esfera centrada en la explosión, con el radio 
        //de la poción, y obtenemos los objetos dentro de dicha esfera,
        //y recorremos cada objeto para dañarlo
        foreach (Collider c in Physics.OverlapSphere(transform.position, Radius))
        {
            //TODO: Dañar los objetos dañables
            Damageable d = c.GetComponent<Damageable>();
            if (d)
                d.GetDamage(Damage, 0);
        }

        //Creamos el sonido de la explosión
        AudioSource.PlayClipAtPoint(SfxExplosion, transform.position);

        //Creamos la partícula de la explosión y la temporizamos para destruirla después de la animación
        if (PrefExplosion)
        {
            GameObject explosion = Instantiate(PrefExplosion, transform.position, Quaternion.identity);
        }

        //Destruimos el misil
        Destroy(transform.root.gameObject);
    }

}