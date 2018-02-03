using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCrate : MonoBehaviour {


    private int[] Restore = new int[] { 0, 24, 4, 48, 8, 160 }; //Van en orden: Ballesta, Pociones, Rayo, Misil y Lanzallamas. El primero es un cero por la armadura

    [SerializeField] private EBehaviour currentType;

    public enum EBehaviour
    {
        Armor,
        Crossbow,
        Potions,
        Thunder,
        Missile,
        Flamethrower
    }

    // Use this for initialization
    void Start ()
    {
        int indexChild = (int)currentType;
        transform.GetChild(indexChild).gameObject.SetActive(true);
	}

    /// <summary>
    /// Al pasar por la caja, añadimos el arma al jugador, y le damos munición. Si ya tiene arma, se queda sólo con la munición
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {

        ShooterCharacter chara = other.GetComponent<ShooterCharacter>();
        if (chara)
        {
            if ((int)currentType > 0)
            {
                int indexOfWeapon = (int)currentType; //Ballesta es 1, Pociones es 2, Rayo es 3, Misil es 4, Lanzallamas es 5
                chara.AddWeapon(indexOfWeapon);

                chara.AddAmmo(indexOfWeapon, Restore[(int)currentType]);
            }
            else
            {
                //Es armadura, no arma
                chara.AddArmor();
            }
            Destroy(transform.root.gameObject);
        }
    }

}
