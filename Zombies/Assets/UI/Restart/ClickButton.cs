using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ClickButton : MonoBehaviour
{
    //[SerializeField] Button button;
    WaveInteractor waveInteractor;

    private void Start()
    {
        waveInteractor = Game.GetInteractor<WaveInteractor>();
        //button.onClick.AddListener(Reload);
    }

    public void Reload()
    {
        waveInteractor.StopSpawn();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
