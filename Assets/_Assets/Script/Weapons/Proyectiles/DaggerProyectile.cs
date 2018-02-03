using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaggerProyectile : MonoBehaviour
{
    public AudioClip sfxImpact;
    public int damage;

    private Rigidbody rig;

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
    /// Hemos chocado con algo: la daga daña
    /// </summary>
    /// <param name="collision"></param>
    public void OnCollisionEnter(Collision collision)
    {
        //Creamos el sonido del impacto
        AudioSource.PlayClipAtPoint(sfxImpact, transform.position);

        Damageable objetivo = collision.collider.GetComponent<Damageable>();

        if (objetivo) objetivo.GetDamage(damage, 1);

        //Destruimos la daga
        Destroy(this.gameObject);
    }
    
}
