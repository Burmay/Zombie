using System;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "EnemyData")]

public class EnemyData : ScriptableObject
{
    [Header("Enemy")]
    public int enemyMinHp;
    public int enemyMaxHp;
    public int enemyMinSpeed;
    public int enemyMaxSpeed;

    [Header("MegaEnemy")]

    public int megaEnemyMinHp;
    public int megaEnemyMaxHp;
    public int megaEnemyMinSpeed;
    public int megaEnemyMaxSpeed;

    public void GetData(Enemy enemy)
    {
        
        if(enemy is MegaEnemy)
        {
            var megaEnemy = enemy as MegaEnemy;
            megaEnemy.Init(megaEnemyMinHp, megaEnemyMaxHp, megaEnemyMinSpeed, megaEnemyMaxSpeed);
        }
        else if (enemy is Enemy)
        {
            enemy.Init(enemyMinHp, enemyMaxHp, enemyMinSpeed, enemyMaxSpeed);
        }
        else
        {
            throw new Exception("Incorrect data access");
        }
    }

}
