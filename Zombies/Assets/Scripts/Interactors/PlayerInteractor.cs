using UnityEngine;

public class PlayerInteractor : Interactor
{
    private const string PLAYER_PATH = "Prefabs/Player";
    private Player instance;
    private UIInteractor uiInteractor;
    

    public override void OnCreate()
    {
        uiInteractor = Game.GetInteractor<UIInteractor>();
        SpawnPlayer();
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
