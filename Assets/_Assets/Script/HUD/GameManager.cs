using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static bool Pause = false;

    [SerializeField] private Transform _wIcons;
    [SerializeField] private Text _hudAmmo;
    [SerializeField] private Image _life;
    [SerializeField] private Image _noAmmo;
    [SerializeField] private GameObject[] _Armor;
    [SerializeField] private Image _crossHair;

    private int currentW;
    private BGMPlayer player;

    public int CurrentWeapon
    {
        get
        {
            return currentW;
        }
    }

    private void Awake()
    {
        player = FindObjectOfType<BGMPlayer>();
        PlayMusic(0);
    }

    private void Start()
    {
        ActualizarArma(0);
        ActualizarVida(1f);
    }
    
    /// <summary>
    /// Función que actualiza el HUD de vida
    /// </summary>
    /// <param name="proporcion">proporción de vida a la que hay que dejar el HUD</param>
    public void ActualizarVida(float proporcion)
    {
        _life.fillAmount = proporcion;
    }

    /// <summary>
    /// Función que actualiza el HUD de Stamina
    /// </summary>
    /// <param name="proporcion"></param>
    public void ActualizarSta(float proporcion)
    {

    }

    /// <summary>
    /// Función que modifica el número en el HUD de munición visible.
    /// Si la munición llega a cero, muestra el icono del arco rojo
    /// </summary>
    /// <param name="amount"></param>
    public void ActualizarAmmo(int amount)
    {
        _hudAmmo.text = string.Format("{0}", amount);
    }

    /// <summary>
    /// Función que modifica el HUD de iconos de armadura
    /// </summary>
    /// <param name="valor">Número de iconos a mostrar (de cero a 3)</param>
    public void ActualizarProt(int valor)
    {
        for (int i = 0; i< valor; i++)
        {
            _Armor[i].SetActive(true);
        }
    }

    /// <summary>
    /// Función que modifica el HUD del icono del arma equipada
    /// </summary>
    /// <param name="valor">Arma a mostrar:
    /// 0 - Dagas
    /// 1 - Explosivos
    /// 2 - Ballesta
    /// 3 - Rayo
    /// 4 - Llamarada
    /// 5 - Misil</param>
    public void ActualizarArma(int valor)
    {
        _crossHair.gameObject.SetActive(false);
        if (valor == 1)
        {
            //Estamos cambiando a la ballesta. Mostramos crosshair
            _crossHair.gameObject.SetActive(true);
        }
        _wIcons.GetChild(currentW).gameObject.SetActive(false);
        _wIcons.GetChild(valor).gameObject.SetActive(true);
        currentW = valor;
    }

    /// <summary>
    /// función que cambia la música. Puede ser llamada por enemigos (Boss) o triggers
    /// </summary>
    /// <param name="index">Índice de la canción</param>
    public void PlayMusic(int index)
    {
        player.PlayBGM(index);
    }

}
