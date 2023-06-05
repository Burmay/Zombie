using System;
using UnityEngine;

public class EnemyInteractor : Interactor
{
    //Zombie generation and control

    public Action ZoombieDied;

    private const string ENEMY_PATH = "Prefabs/Enemy";
    private const string MEGA_ENEMY_PATH = "Prefabs/MegaEnemy";
    private WaveInteractor waveInteractor;
    private UIInteractor uiInteractor;
    private int zoombieCount;

    public override void OnCreate()
    {
        waveInteractor = Game.GetInteractor<WaveInteractor>();
        uiInteractor = Game.GetInteractor<UIInteractor>();
        zoombieCount = 0;
        ZoombieDied += this.SubtractZombie;
    }

    public void SpawnEnemy(Transform[] spawnPoint, int countPoint, int[] countEnemyPerWave)
    {
        var wave = waveInteractor.WaveNumber;
        var prefab = Resources.Load<Enemy>(ENEMY_PATH);
        System.Random random = new System.Random();

        for (int i = 0; i < countEnemyPerWave[wave]; i++)
        {
            var instance = Instantiate(prefab, spawnPoint[random.Next(countPoint)].position, Quaternion.identity);
            zoombieCount++;
        }
    }

    public void SpawnMegaEnemy(Transform[] spawnPoint, int countPoint)
    {
        var wave = waveInteractor.WaveNumber;
        var prefab = Resources.Load<MegaEnemy>(MEGA_ENEMY_PATH);
        System.Random random = new System.Random();

        for (int i = 0; i < waveInteractor.countMegaEnemyPerWave[wave]; i++)
        {
            int rand = random.Next(countPoint);
            Vector3 megapos = new Vector3(spawnPoint[rand].position.x, spawnPoint[rand].position.y + 5, spawnPoint[rand].position.z);
            var instance = Instantiate(prefab, megapos, Quaternion.identity);
            zoombieCount++;
        }
    }

    public void UpdateScore()
    {
        uiInteractor.UpdateScore();
    }    

    public void SubtractZombie()
    {
        zoombieCount--;
        if(zoombieCount == 0)
        {
            waveInteractor.EndWave();
        }
    }
}
