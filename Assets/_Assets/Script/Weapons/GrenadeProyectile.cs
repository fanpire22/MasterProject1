using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeProyectile : MonoBehaviour
{
    public AudioClip sfxExplosion;
    public GameObject prefExplosion;
    public float Fuse;
    public float Radius;
    public int damage;

    private Rigidbody rig;

    // Use this for initialization
    private void Start()
    {
        Invoke("Explode", Fuse);
    }

    private void Awake()
    {
        rig = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// Función para lanzar la granada
    /// </summary>
    /// <param name="direction">dirección a la que lanzarla</param>
    public void Throw(Vector3 direction)
    {
        rig.AddForce(direction, ForceMode.Impulse);
    }

    /// <summary>
    /// Función que controla la explosión de la granada
    /// </summary>
    private void Explode()
    {
        //Dibujamos una esfera centrada en la explosión, con el radio 
        //de la granada, y obtenemos los objetos dentro de dicha esfera,
        //y recorremos cada objeto para dañarlo
        foreach (Collider c in Physics.OverlapSphere(transform.position, Radius ))
        {
            //TODO: Dañar los objetos dañables
            Damageable d = c.GetComponent<Damageable>();
            if (d)
                d.GetDamage(damage,0);
        }

        //Creamos el sonido de la explosión
         AudioSource.PlayClipAtPoint(sfxExplosion, transform.position);

        //Creamos la partícula de la explosión y la temporizamos para destruirla después de la animación
        if (prefExplosion)
        {
            GameObject explosion = Instantiate(prefExplosion, transform.position, Quaternion.identity);
        }

        //Destruimos la granada
        Destroy(this.gameObject);
    }
}
