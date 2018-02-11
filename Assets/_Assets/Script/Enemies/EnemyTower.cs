using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTower : Enemy
{
    [SerializeField] private float _attackDistance;
    [SerializeField] private int _damage;
    [SerializeField] private float _roF;
    [SerializeField] GameObject _fireballPrefab;

    private float timeLapse;
    private float spawnerDistance;

    private ShooterCharacter player;
    RaycastHit hit;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<ShooterCharacter>();
        spawnerDistance = GetComponent<SphereCollider>().radius + 0.65f;
    }

    // Use this for initialization
    protected override void Start()
    {
        timeLapse = Time.time + _roF;
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        //Disparamos si vemos al jugador y no está pausado el juego... o se ha muerto, claro
        if (!player.IsDead && !GameManager.Pause)
        {
            Vector3 direction = player.transform.position - transform.position;

            transform.rotation = Quaternion.LookRotation(direction.normalized);

            if (Physics.Raycast(transform.position + transform.forward, transform.forward, out hit, _attackDistance))
            {
                if (hit.collider.CompareTag("Player"))
                    Shoot();
            }
        }
    }

    /// <summary>
    /// Si hay un enemigo a distancia, lo disparo (respetando mi Rate of Fire)
    /// </summary>
    private void Shoot()
    {
        if (Time.time > timeLapse)
        {
            timeLapse = Time.time + _roF;

            Vector3 spawnPos = transform.position + (transform.forward * spawnerDistance * 1.02f);

            MagicBolt mag = GameObject.Instantiate(_fireballPrefab, spawnPos, transform.rotation).GetComponentInChildren<MagicBolt>();
            mag.Damage = _damage;
            mag.AddForce(50);

        }
    }
}
