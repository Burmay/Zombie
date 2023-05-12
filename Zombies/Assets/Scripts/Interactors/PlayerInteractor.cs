using UnityEngine;

public class PlayerInteractor : Interactor
{
    private const string PLAYER_PATH = "Prefabs/Player";
    private Player instance;
    private UIInteractor uiInteractor;
    

    public int Ammo { get; private set; }
    public int Clip { get; private set; }
    public int Xp { get; private set; }

    public override void OnCreate()
    {
        uiInteractor = Game.GetInteractor<UIInteractor>();
        SpawnPlayer();
        SetAmmunition();
    }

    private void SetAmmunition()
    {
        Ammo = 100000;
    }

    public void SpendAmmo()
    {
        if (Ammo > 0)
        {
            Ammo--;
        }
    }

    private void SpawnPlayer()
    {
        var prefab = Resources.Load<Player>(PLAYER_PATH);
        instance = Instantiate(prefab);
    }

    public Transform GetTransformInstance()
    {
        return instance.transform;
    }

    public Player GetPlayerInstance()
    {
        return instance;
    }

    public void GameOver()
    {
        uiInteractor.GameOver(false);
    }

}
