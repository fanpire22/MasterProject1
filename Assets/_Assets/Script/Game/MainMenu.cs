using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    [SerializeField] GameObject LayoutMain;
    [SerializeField] GameObject LayoutCargando;

    public void NewGame()
    {
        LayoutMain.SetActive(false);
        LayoutCargando.SetActive(true);
        SceneManager.LoadSceneAsync("Castle-Interior");
    }
    /// <summary>
    ///Hacemos una validación PRAGMA: Si estamos en modo ejecución dentro de Unity, lo paramos. 
    ///Si estamos en ejecución, cerramos la aplicación 
    /// </summary>
    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

}
