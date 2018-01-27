using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public static bool Pause = false;

    /// <summary>
    /// Función que actualiza el HUD de vida
    /// </summary>
    /// <param name="proporcion"></param>
    public static void ActualizarVida(float proporcion)
    {

    }

    /// <summary>
    /// Función que actualiza el HUD de Stamina
    /// </summary>
    /// <param name="proporcion"></param>
    public static void ActualizarSta(float proporcion)
    {

    }

    /// <summary>
    /// Función que modifica el número en el HUD de munición visible.
    /// Si la munición llega a cero, muestra el icono del arco rojo
    /// </summary>
    /// <param name="amount"></param>
    public static void ActualizarAmmo(float amount)
    {

    }

    /// <summary>
    /// Función que modifica el HUD de iconos de armadura
    /// </summary>
    /// <param name="valor">Número de iconos a mostrar (de cero a 3)</param>
    public static void ActualizarProt(int valor)
    {

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
    public static void ActualizarArma(int valor)
    {

    }

}
