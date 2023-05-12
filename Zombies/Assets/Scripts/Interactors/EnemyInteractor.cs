using UnityEngine;

public class EnemyInteractor : Interactor
{
    //Zombie generation and control

    private int minHP = 25, maxHP = 50;
    private int megaMinHP = 550, megaMaxHP = 1100;

    private const string ENEMY_PATH = "Prefabs/Enemy";
    private const string MEGA_ENEMY_PATH = "Prefabs/MegaEnemy";
    private Enemy[] instance;
    private WaveInteractor waveInteractor;
    private UIInteractor uiInteractor;

    public override void OnCreate()
    {
        waveInteractor = Game.GetInteractor<WaveInteractor>();
        uiInteractor = Game.GetInteractor<UIInteractor>(); 
        instance = new Enemy[1000];
    }

    public void SpawnEnemy(Transform[] spawnPoint, int countPoint, int[] countEnemyPerWave)
    {
        var wave = waveInteractor.WaveNumber;
        var prefab = Resources.Load<Enemy>(ENEMY_PATH);
        System.Random random = new System.Random();

        for (int i = 0; i < countEnemyPerWave[wave]; i++)
        {
            instance[i] = Instantiate(prefab, spawnPoint[random.Next(countPoint)].position, Quaternion.identity);
            instance[i].Init(minHP, maxHP);
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
            instance[wave + i] = Instantiate(prefab, megapos, Quaternion.identity);
            instance[wave + i].Init(megaMinHP, megaMaxHP);
        }
    }

    public void CheckSurvivor()
    {
        GameObject[] survivors = GameObject.FindGameObjectsWithTag("Enemy");
        if(survivors.Length <= 1)
        {
            waveInteractor.EndWave();
        }
    }

    public void UpdateScore()
    {
        uiInteractor.UpdateScore();
    }    
}
