using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum AnimationType
{
    StandingMan,
    StandingWoman,
    StandingWoman2,
    PushingTheCan,
    WomanHappy,
    Jumping,
    Sad,
    Happy,
    Victory,
    Cry
}

public class AnimationReferencer : LocalSingleton<AnimationReferencer>
{

    [System.Serializable]
    public class MyAnimation
    {
        public AnimationType animationType;
        public AnimationClip animation;
    }

    public MyAnimation[] animations;
    public Avatar human;

}
