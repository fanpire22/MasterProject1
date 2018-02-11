using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour {

    /// <summary>
    /// Si es el jugador el que entra, ganamos la partida
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        ShooterCharacter player = other.gameObject.GetComponent<ShooterCharacter>();
        if (player)
            player.OnWin();
    }
}
