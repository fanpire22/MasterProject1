using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CritterMove : Enemy {

    private CharacterController _controller;
    [SerializeField]private float _speed;
    [SerializeField] private float _minTime;
    [SerializeField] private float _maxTime;


    // Use this for initialization
    protected override void Start () {

        base.Start();

        _controller = GetComponent<CharacterController>();

        //llamamos a la función de rotar de modo aleatorio
        Invoke("RandomRotation", Random.Range(_minTime, _maxTime));
    }

    private void RandomRotation()
    {
        //Rotamos en un ángulo al azar entre 0 y 359
        transform.rotation = Quaternion.Euler(0, Random.Range(0.0f, 359.9f), 0);

        //volvemos a llamarnos de nuevo, en un momento al azar.
        Invoke("RandomRotation", Random.Range(_minTime, _maxTime));
    }
	
	// Update is called once per frame
	private void Update () {
        _controller.SimpleMove(transform.forward * _speed);
    }

    /// <summary>
    /// Si chocamos con algo, giramos 120 grados en una dirección al azar y volvemos a lanzar la función de rotación al azar
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        CancelInvoke();

        transform.rotation = Quaternion.Euler(0, ((Random.Range(0, 1) > 0) ? 120 : -120), 0);

        //llamamos a la función de rotar de modo aleatorio
        Invoke("RandomRotation", Random.Range(_minTime, _maxTime));
    }
}
