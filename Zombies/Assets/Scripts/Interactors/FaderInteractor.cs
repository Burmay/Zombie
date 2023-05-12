using System.Collections;
using UnityEngine;

public class FaderInteractor : Interactor
{
    private bool waitFading;
    private UIInteractor uiInteractor; 
    private Coroutine coroutineLoadFade;

    public override void OnCreate()
    {
        uiInteractor = Game.GetInteractor<UIInteractor>();
    }

    public void OnFade()
    {
        coroutineLoadFade = Coroutines.StartRoutine(LoadFaderRoutine());
    }

    private IEnumerator LoadFaderRoutine()
    {
        bool waitFading = true;
        Fader.instance.FadeIn(() => waitFading = false);

        while (waitFading)
        {
            yield return null;
        }

        uiInteractor.OnEndText();
        StopCoroutine();

        //yield return new WaitForSeconds(5);
        //Fader.instance.FadeOut(() => waitFading = false);
        //
        //while (waitFading)
        //{
        //    yield return null;
        //}
    }

    private void StopCoroutine()
    {
        Coroutines.StopRoutine(coroutineLoadFade);
    }
}