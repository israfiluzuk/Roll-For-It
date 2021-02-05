using DG.Tweening;

public class Player : Human
{
    public static Player Instance;

    protected override void Awake()
    {
        Instance = this;
        base.Awake();
        PlayAnim(AnimationType.StandingMan, .3f);
    }

    internal void Pushing()
    {
        PlayAnim(AnimationType.PushingTheCan, .3f, true);
    }
}
