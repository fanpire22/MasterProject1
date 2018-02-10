using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBolt : MonoBehaviour
{

    public AudioClip sfxExplosion;
    public GameObject prefExplosion;
    public float Radius;
    public int Damage;

    private Rigidbody rig;

    private void Awake()
    {
        rig = GameObject.FindObjectOfType<Rigidbody>();
    }


    public void AddForce(int Force)
    {
        rig.AddForce(transform.forward * Force, ForceMode.Impulse);
    }


    /// <summary>
    /// Hemos chocado con algo: la bola explota
    /// <param name="collision"></param>
    public void OnCollisionEnter(Collision collision)
    {
        Explode();
    }

    /// <summary>
    /// Función que controla la explosión de la bola de energía
    /// </summary>
    private void Explode()
    {
        //Dibujamos una esfera centrada en la explosión, con el radio 
        //de la granada, y obtenemos los objetos dentro de dicha esfera,
        //y recorremos cada objeto para dañarlo
        foreach (Collider c in Physics.OverlapSphere(transform.position, Radius))
        {
            //TODO: Dañar los objetos dañables
            Damageable d = c.GetComponent<Damageable>();
            if (d)
                d.GetDamage(Damage, 0);
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
