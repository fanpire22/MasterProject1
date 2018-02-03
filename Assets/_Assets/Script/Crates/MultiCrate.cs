using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiCrate : MonoBehaviour
{
    public EBehaviour CurrentBehaviour;

    private  int[] Restore = new int[] { 6, 12, 2, 24, 4, 80 }; //Van en orden: Dagas, Ballesta, Pociones, Rayo, Misil y Lanzallamas
    private int lifeRestore = 50; //La vida que el jugador va a recuperar. Se separan para que la recuperación de armas sea más streamlined

    public enum EBehaviour
    {
        life = 0,
        Ammo = 1,
        Both = 2
    }

    private void Start()
    {
        MeshRenderer mr = GetComponent<MeshRenderer>();

        Material mat = mr.material;

        switch (CurrentBehaviour)
        {
            case EBehaviour.life:
                mat.SetColor("_Color", new Color(0.4f, 0.1f, 0.1f));
                break;
            case EBehaviour.Ammo:
                mat.SetColor("_Color", new Color(0.4f, 0.4f, 1));
                break;
            case EBehaviour.Both:
                mat.SetColor("_Color", new Color(0.4f, 0.1f, 0.4f));
                break;
            default:
                mat.SetColor("_Color", new Color(0.4f, 0.5f, 0.6f));
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        ShooterCharacter player = other.GetComponent<ShooterCharacter>();
        if (!player) return;

        switch (CurrentBehaviour)
        {
            case EBehaviour.life:
                //Curamos al jugador
                if (player.Heal(lifeRestore)) Destroy(transform.root.gameObject);
                break;
            case EBehaviour.Ammo:
                //Recargamos todas las armas que haya recogido el jugador
                if (player.AddAmmo(Restore)) Destroy(transform.root.gameObject);
                break;
            case EBehaviour.Both:
                //Curamos y recargamos
                if (player.Heal(lifeRestore) || player.AddAmmo(Restore)) Destroy(transform.root.gameObject);
                break;
            default:
                //Curamos y recargamos
                if (player.Heal(lifeRestore) || player.AddAmmo(Restore)) Destroy(transform.root.gameObject);
                break;
        }
        
    }
}
