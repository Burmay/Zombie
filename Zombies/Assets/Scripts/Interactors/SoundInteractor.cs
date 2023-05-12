using UnityEngine;

public class SoundInteractor : Interactor
{
    AudioClip shot, gameOver, music, pickUp, siren, victory, zombieDied;
    AudioSource playerSource;
    PlayerInteractor playerInteractor;
    AudioSource cameraSourse;

    public override void OnCreate()
    {

        // test version
        playerInteractor = Game.GetInteractor<PlayerInteractor>();
        playerSource = playerInteractor.GetPlayerInstance().GetComponent<AudioSource>();
        cameraSourse = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>();
        
        shot = Resources.Load<AudioClip>("Sound/_shot");
        music = Resources.Load<AudioClip>("Sound/_music");

        gameOver = Resources.Load<AudioClip>("Sound/_gameOver");
        pickUp = Resources.Load<AudioClip>("Sound/_pickUp");
        siren = Resources.Load<AudioClip>("Sound/_siren");
        victory = Resources.Load<AudioClip>("Sound/_victory");
        zombieDied = Resources.Load<AudioClip>("Sound/_zombieDied");


        MusicOn();
    }

    private void MusicOn()
    {
        cameraSourse.loop = true;
    }

    public void MusicOff()
    {
        cameraSourse.Stop();
    }

    public void PlayShot()
    {
        playerSource.PlayOneShot(shot);
    }

    public void PlayGameOver()
    {
        playerSource.PlayOneShot(gameOver);
    }

    public void PlayVictory()
    {
        playerSource.PlayOneShot(victory);
    }

    public void PlayPickUp()
    {
        playerSource.PlayOneShot(pickUp);
    }

    public void PlaySiren()
    {
        playerSource.PlayOneShot(siren);
    }

    public void PlayZombieDied()
    {
        playerSource.PlayOneShot(zombieDied);
    }
}
