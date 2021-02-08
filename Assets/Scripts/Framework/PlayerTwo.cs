using DG.Tweening;
using System.Collections;
using UnityEngine;

public class PlayerTwo : Human
{
    public static PlayerTwo Instance;

    protected override void Awake()
    {
        Instance = this;
        base.Awake();
        PlayAnim(AnimationType.StandingWoman, .3f);
    }

    public IEnumerator Pushing()
    {
        yield return new WaitForSeconds(.1f);
        PlayAnim(AnimationType.PushingTheCan, .3f, true);
        yield return new WaitForSeconds(2);
        PlayAnim(AnimationType.StandingWoman,0.5f);
    }

}
