using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class CharacterControllerMove : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private AudioClip[] _footSteps;
    [SerializeField] private AudioClip _jump;
    [SerializeField] private AudioClip _land;


    [Header("Movement")]
    [SerializeField]
    private Vector2 _rotationSpeed;
    [SerializeField] private float _walkingSpeed;
    [SerializeField] private float _runningSpeed;
    [SerializeField] private float _jumpForce;
    [Range(0, 1)] [SerializeField] private float _airSpeedMultiplier = .1f;

    private float Gravity = 9.8f;
    private Vector3 currentDirection;
    private float cameraPitch;
    private float speed;
    private GameManager gm;

    private CharacterController _controller;
    private AudioSource _SFX;
    private bool bOnAir = false;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _SFX = GetComponent<AudioSource>();
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    private void Start()
    {
        //Dejamos el cursor centrado en la ventana, y lo ocultamos
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (GameManager.Pause) return;

        //Velocidad: Depende de si estamos andando o corriendo. También habría que desenfocar levemente la cámara para aumentar el efecto de velocidad
        if (_controller.isGrounded)
        {
            if (bOnAir)
            {
                //Hemos aterrizado. Reproducimos el sonido de aterrizaje
                _SFX.clip = _land;
                _SFX.Play();
                bOnAir = false;
            }
            if (Input.GetKey(KeyCode.LeftShift))
            {
                speed = _runningSpeed;
            }
            else
            {
                speed = _walkingSpeed;
            }
        }

        //Detección del movimiento horizontal y vertical del ratón
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = -Input.GetAxis("Mouse Y");

        //Obtenemos los dos ejes: Lateral y vertical
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        //Vector de movimiento: Debemos multiplicarlo por el tiempo de cada frame para hacerlo estable, y también por la velocidad
        Vector3 direction = transform.forward * vertical + transform.right * horizontal;

        //La velocidad depende de si estamos en el suelo o en el aire.
        currentDirection.x = direction.x * (_controller.isGrounded ? speed : speed * _airSpeedMultiplier);
        currentDirection.z = direction.z * (_controller.isGrounded ? speed : speed * _airSpeedMultiplier);

        //Saltamos, aplicamos fuerza al salto
        if (Input.GetButtonDown("Jump") && _controller.isGrounded)
        {
            currentDirection.y = _jumpForce;
            _SFX.clip = _jump;
            _SFX.Play();
            bOnAir = true;
        }

        //Si no estamos en el suelo, caemos
        if (!_controller.isGrounded)
        {
            currentDirection.y -= Gravity * Time.deltaTime;
        }

        //Movimiento
        _controller.Move(currentDirection * Time.deltaTime);

        //Rotación según el ratón
        transform.Rotate(Vector3.up * mouseX * _rotationSpeed.x * Time.deltaTime);
        cameraPitch += mouseY * _rotationSpeed.y;
        cameraPitch = Mathf.Clamp(cameraPitch, -90f, 75f);
        Camera.main.transform.localRotation = Quaternion.Euler(cameraPitch, 0, 0);

        //Si no estamos reproduciendo sonido, estamos en movimiento, y además en el suelo, reproducimos un paso al azar
        float magnitudeXZ = Mathf.Abs(horizontal) + Mathf.Abs(vertical);
        if (magnitudeXZ > 0.1 && !_SFX.isPlaying && _controller.isGrounded)
        {
            _SFX.clip = _footSteps[Random.Range(0, _footSteps.Length)];
            _SFX.Play();
        }
    }
}
