using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "PlayerData")]
public class PlayerData : ScriptableObject
{
    public int speed;
    public int ammo;

    public void GetData(Player player)
    {
        player.SetData(speed, ammo);
    }
}
