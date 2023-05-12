using UnityEngine;
using TMPro;

public class UIInteractor : Interactor
{
    TextMeshProUGUI scoreText;
    public int EnemyKilled { get; private set; }
    private FaderInteractor fader;
    private PlayerInteractor player;
    private SoundInteractor soundInteractor;
    private const string END_TEXT_PATH = "UI/CanvasEnd";
    private const string WIN_TEXT_PATH = "UI/CanvasWin";
    private const string AIM_PATH = "UI/AimCanvas";
    private Canvas endText;
    private bool win;
    


    public override void OnCreate()
    {
        var tmpObj = GameObject.FindGameObjectWithTag("UIEnemiesKilled");
        scoreText = tmpObj.GetComponent<TextMeshProUGUI>();
        EnemyKilled = 0;
        player = Game.GetInteractor<PlayerInteractor>();
        fader = Game.GetInteractor<FaderInteractor>();
        soundInteractor = Game.GetInteractor<SoundInteractor>();
        Instantiate(Resources.Load<Canvas>(AIM_PATH));
    }
    
    public void UpdateScore()
    {
        EnemyKilled++;
        scoreText.text = EnemyKilled.ToString();
    }

    public void GameOver(bool win)
    {
        this.win = win;
        player.GetPlayerInstance().die = true;
        soundInteractor.MusicOff();
        if (win)
        {
            soundInteractor.PlayVictory(); 
        }
        else
        {
            soundInteractor.PlayGameOver();
        }
        fader.OnFade();
    }

    // Print the final text after the fader
    public void OnEndText()
    {
        if(win == true) { endText = Resources.Load<Canvas>(WIN_TEXT_PATH); Instantiate(endText); }
        else { endText = Resources.Load<Canvas>(END_TEXT_PATH); Instantiate(endText); }
        
    }

    private void DestroyEndText()
    {
        GameObject.Destroy(endText);
    }


}