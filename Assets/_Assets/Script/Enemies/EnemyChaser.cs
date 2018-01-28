using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaser : Enemy
{

    [SerializeField] float _speed;
    [SerializeField] float _speedRotation;
    [SerializeField] float _rof;
    [SerializeField] int _damage;
    [SerializeField] float _minDistanceAttack;
    [SerializeField] float _minDistanceLook;
    [SerializeField] float _timeRandomBehaviour;
    [SerializeField] float ViewTargetBaseOffset;
    [SerializeField] GameObject _prefShot;
    [SerializeField] Transform PewSpawner1;
    [SerializeField] Transform PewSpawner2;


    private CharacterController _chara;
    private float _timeStamp;
    private Transform _playerLocation;
    private Damageable _playerDamageable;
    private float _timeMovementRandom;

    RaycastHit hit;
    Quaternion _destinyOrientation;

    private void Awake()
    {
        _chara = GetComponent<CharacterController>();
    }

    protected override void Start()
    {
        base.Start();
        _playerLocation = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    private void Update()
    {

        if (_playerLocation)
        {
            Vector3 direction = _playerLocation.position - transform.position;
            float distance = direction.magnitude;

            direction.y = 0;

            if (distance < _minDistanceAttack)
            {
                _destinyOrientation = Quaternion.LookRotation(direction);
                //Attack
                _timeMovementRandom = 0;

                if (_playerDamageable && Time.time > _timeStamp && !_playerDamageable.IsDead)
                {
                    _timeStamp = Time.time + _rof;
                    if (_prefShot)
                    {
                        GameObject flash = Instantiate(_prefShot, PewSpawner1);
                        GameObject flash2 = Instantiate(_prefShot, PewSpawner2);
                        Destroy(flash, 0.2f);
                        Destroy(flash2, 0.2f);
                    }
                    _playerDamageable.GetDamage(_damage, 0);
                    
                }

            }
            else if (distance < _minDistanceLook)
            {
                //Look & Follow
                _destinyOrientation = Quaternion.LookRotation(direction);
                _timeMovementRandom = 0;

                Vector3 viewTargetPosition = transform.position + transform.forward * ViewTargetBaseOffset;
                if (Physics.Raycast(viewTargetPosition, transform.forward, out hit, _minDistanceLook))
                {
                    if (hit.collider.CompareTag("Player"))
                    {
                        _playerDamageable = hit.collider.GetComponent<Damageable>();

                        _chara.SimpleMove(transform.forward * _speed * Time.deltaTime);
                    }
                    else
                    {
                        //El jugador ha salido de nuestro rango de alcance
                        _playerDamageable = null;
                    }
                }

                _chara.SimpleMove(transform.forward * _speed);
            }
            else
            {
                //RandomBehaviour
                _timeMovementRandom += Time.deltaTime;
                if (_timeMovementRandom > _timeRandomBehaviour)
                {
                    _timeMovementRandom = 0;
                    _destinyOrientation = Quaternion.Euler(0, (Random.Range(0.0f, 359.9f)), 0);
                }
                _chara.SimpleMove(transform.forward * _speed);
            }

            Vector3 frameFWD = Vector3.RotateTowards(transform.forward, _destinyOrientation * Vector3.forward, _speedRotation * Mathf.Deg2Rad * Time.deltaTime, 1);
            transform.rotation = _destinyOrientation;
        }
    }
}
