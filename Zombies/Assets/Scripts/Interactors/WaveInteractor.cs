using System.Collections;
using UnityEngine;

public class WaveInteractor : Interactor
{
    // Creating waves of zombies

    private EnemyInteractor enemy;
    private ShootingInteractor shootingInteractor;
    public int WaveNumber { get; private set; }
    private int MaxWaveNumber = 3;
    public int[] countEnemyPerWave, addCountEnemyPerWave;
    public int[] countMegaEnemyPerWave;
    private Transform[] spawnPoint;
    private Transform[] addSpawnPoint;
    private int countSpawnPoint, countAddSpawnPoint;
    private UIInteractor uiInteractor;
    private ItemInteractor itemInteractor;
    private SoundInteractor soundInteractor;

    private Coroutine coroutineAddWave;

    public override void OnStart()
    {
        enemy = Game.GetInteractor<EnemyInteractor>();
        uiInteractor = Game.GetInteractor<UIInteractor>();
        shootingInteractor = Game.GetInteractor<ShootingInteractor>();
        itemInteractor = Game.GetInteractor<ItemInteractor>();
        soundInteractor = Game.GetInteractor<SoundInteractor>();
        SetSettingsWave();
        StartWave();
    }

    private void SetSettingsWave()
    {
        countMegaEnemyPerWave = new int[10];
        var pointObj = GameObject.FindGameObjectsWithTag("SpawnPoint");
        var addPointObj = GameObject.FindGameObjectsWithTag("AddSP");
        spawnPoint = new Transform[10];
        addSpawnPoint = new Transform[10];
        for (int i = 0; i < pointObj.Length; i++)
        {
            spawnPoint[i] = pointObj[i].transform;
        }
        for(int i = 0;i < addPointObj.Length; i++)
        {
            addSpawnPoint[i] = addPointObj[i].transform;
        }

        countSpawnPoint = pointObj.Length;
        countAddSpawnPoint = addPointObj.Length;
        WaveNumber = 0;
        countEnemyPerWave = new int[10];
        addCountEnemyPerWave = new int[10];
        for (int i = 1; i < 4; i++)
        {
            WaveNumber++;
            countEnemyPerWave[i] = (int)System.Math.Pow(5, (double)WaveNumber);
            addCountEnemyPerWave[i] = WaveNumber * 2;
            countMegaEnemyPerWave[i] = WaveNumber;
        }
        WaveNumber = 0;
    }

    private void SpawnEnemy()
    {
        enemy.SpawnEnemy(spawnPoint, countSpawnPoint, countEnemyPerWave);
    }

    private void AddSpawnEnemy()
    {
        enemy.SpawnEnemy(addSpawnPoint, countAddSpawnPoint, addCountEnemyPerWave);
    }

    public void EndWave()
    {
        Debug.Log("Win");
        if(coroutineAddWave != null) {Coroutines.StopRoutine(coroutineAddWave); }
        StartWave();
    }

    private void StartWave()
    {
        WaveNumber++;
        if (WaveNumber == MaxWaveNumber + 1)
        {
            uiInteractor.GameOver(true);
            return;
        }

        SpawnEnemy();
        enemy.SpawnMegaEnemy(spawnPoint, countSpawnPoint);
        soundInteractor.PlaySiren();
        //shootingInteractor.UpgradePerWave();
        coroutineAddWave = Coroutines.StartRoutine(AddSpawn());
        itemInteractor.SpawnItem();
    }

    public void StopSpawn()
    {
        Coroutines.StopRoutine(coroutineAddWave);
    }

    private IEnumerator AddSpawn()
    {
        for(int i = 0; i < 10 * WaveNumber; i++)
        {
            yield return new WaitForSeconds(1);
            AddSpawnEnemy();
        }
    }
}
