using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionSpawner : MonoBehaviour
{
    [SerializeField] private float _triggerDistance;
    [SerializeField] private float _spawnDistance;
    [SerializeField] private float _spawnTimer;
    [SerializeField] private GameObject _prefMinion;
    [SerializeField] private int _maxSpawns;

    private int _numberSpawns;
    private ShooterCharacter _player;

    private float _spawnerDistance;
    private float _timeLapse;
    private GameManager _gm;

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<ShooterCharacter>();
        _gm = GameObject.FindObjectOfType<GameManager>();
    }

    private void Start()
    {
        _timeLapse = Time.time + _spawnTimer;
    }

    /// Si el jugador entra dentro del área de trigger, no hemos llegado al máximo de spawns y el juego está corriendo,
    ///empezamos a spawnear enemigos
    void Update()
    {
        if (_timeLapse >= Time.time) return;

        if (_numberSpawns < _maxSpawns && !_player.IsDead && !GameManager.Pause)
        {
            Vector3 direction = _player.transform.position - transform.position;

            if (direction.magnitude < _triggerDistance)
            {
                Spawn();
            }
        }
    }

    /// <summary>
    /// Función que instancia un minion dentro del radio de spawneo
    /// </summary>
    private void Spawn()
    {
        _gm.PlayMusic(1);
        _timeLapse = Time.time + _spawnTimer;
        GameObject minion = Instantiate(_prefMinion);

        //Giramos el objeto para spawnear dentro del vector "forward"
        transform.Rotate(Vector3.up, Random.Range(0f, 359f));
        Vector3 posicionFinal = transform.position  + (transform.forward.normalized * Random.Range(0,_spawnDistance));

        //Igualamos la y para que el minion no aparezca atravesado en el suelo
        posicionFinal.y = minion.transform.position.y;
        minion.transform.position = posicionFinal;
        _numberSpawns++;
    }
}
