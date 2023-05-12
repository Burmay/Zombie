using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneConfigurator : SceneConfig
{
    public const string SCENE_NAME = "MainScene";

    public override string sceneName => SCENE_NAME;

    public override Dictionary<Type, Interactor> CreateAllInteractors()
    {
        var interactorsRoll = new Dictionary<Type, Interactor>();
        this.CreateInteractor<PlayerInteractor>(interactorsRoll);
        this.CreateInteractor<EnemyInteractor>(interactorsRoll);
        this.CreateInteractor<ShootingInteractor>(interactorsRoll);
        this.CreateInteractor<WaveInteractor>(interactorsRoll);
        this.CreateInteractor<DamageInteractor>(interactorsRoll);
        this.CreateInteractor<UIInteractor>(interactorsRoll);
        this.CreateInteractor<FaderInteractor>(interactorsRoll);
        this.CreateInteractor<ItemInteractor>(interactorsRoll);
        this.CreateInteractor<SoundInteractor>(interactorsRoll);
        // Int

        return interactorsRoll;
    }

    public override Dictionary<Type, Repository> CreateAllRepositories()
    {
        var repositoriesRoll = new Dictionary<Type, Repository>();

        // Repo

        return repositoriesRoll;
    }
}
