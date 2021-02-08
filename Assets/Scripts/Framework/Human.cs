using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Human : Animations
{
    private Rigidbody _rb;
    protected Rigidbody rb
    {
        get
        {
            if (_rb == null)
            {
                _rb = GetComponent<Rigidbody>();
            }
            return _rb;
        }
    }

    internal void Happy()
    {
        PlayAnim(AnimationType.Happy);
    }
    
    internal IEnumerator Victory()
    {
        PlayAnim(AnimationType.Victory,.3f,true);
        yield return new WaitForSeconds(1.5f);
        PlayAnim(AnimationType.Happy);
    }

    internal IEnumerator Sad()
    {
        PlayAnim(AnimationType.Sad);
        yield return new WaitForSeconds(.5f);
    }
    internal IEnumerator Cry()
    {
        PlayAnim(AnimationType.Cry);
        yield return new WaitForSeconds(.5f);
    }
}
