using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMPlayer : MonoBehaviour
{

    [SerializeField] private AudioClip[] _BGM;
    private int currentSong = -1;
    private AudioSource _SFX;

    private void Awake () {

        _SFX = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Reproduce el BGM justo centrado en el jugador
    /// </summary>
    /// <param name="index"></param>
    public void PlayBGM(int index)
    {
        if(_BGM.Length > 0)
        {
            if (currentSong == index) return;

            if (_SFX.isPlaying) _SFX.Stop();

            //Hemos inicializado alguna canción para el BGMPlayer
            if(_BGM.Length > index)
            {
                //La canción está dentro del rango de nuestras canciones
                _SFX.clip = _BGM[index];
                currentSong = index;
            }
            else
            {
                //Reproducimos la última canción
                _SFX.clip = _BGM[_BGM.Length-1];
                currentSong = _BGM.Length-1;
            }
            _SFX.Play();
        }
    }
}
