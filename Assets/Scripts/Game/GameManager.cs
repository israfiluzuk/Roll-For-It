using Animancer;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    [SerializeField] GameObject cokeCan;
    [SerializeField]
    Transform manPlayer, womanPlayer, playingPosition,
        stayingPosition, camZoomPos1, plateTransform, plateToPlayerPos, plateToPlayerPos2, plateFinalPos;
    [SerializeField] Transform canPos0, canPos1, canPos2, canPos3, canPos4, canPos5, canPos6;
    [SerializeField] Button smashButton;
    [SerializeField] LaunchPlatform launchPlatform;
    private Rigidbody plateRigidbody;
    public bool isHumanWoman = false;
    [SerializeField] List<ParticleSystem> moneyAnims;
    [SerializeField] AnimancerComponent cokeAnim;
    [SerializeField] AnimationClip cokeRotationClip;

    Quaternion vector = Quaternion.identity;


    private void Awake()
    {
        foreach (ParticleSystem item in moneyAnims)
        {
            item.Stop();
        }
    }

    private void Start()
    {
        smashButton.onClick.AddListener(() => StartCoroutine(SmashFunction()));
        plateRigidbody = plateTransform.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(Pushing(1));
            launchPlatform.PowerBarON = false;
            launchPlatform.LaunchRocket();
            launchPlatform.StartCoroutine(launchPlatform.TurnOffPowerBar());
            print(launchPlatform.Fill);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            SceneManager.LoadScene("Main");
        }
    }
    IEnumerator CameraZoom(Transform transform)
    {
        yield return new WaitForSeconds(.5f);
        mainCamera.transform.DOMove(transform.position, .5f);
        mainCamera.transform.DORotate(new Vector3(15, 90, 0), .5f);
        yield return ButtonScale(1, true);
        StartCoroutine(MoveObjectToPlayer(plateTransform, plateToPlayerPos));
    }
    private IEnumerator SmashFunction()
    {
        //plateTransform.GetComponent<MeshCollider>().enabled = true;
        StartCoroutine(ButtonScale(0, false));
        StartCoroutine(MoveObjectToPlayer(plateTransform, plateToPlayerPos2));
        yield return new WaitForSeconds(.25f);
        StartCoroutine(MoveObjectToPlayer(plateTransform, plateFinalPos));
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(PlayerReaction(isHumanWoman));
        yield return new WaitForSeconds(.16f);
        plateTransform.DOJump(new Vector3(-.8f, 1.6f, 4.1f), 1, 1, 1f);
        //plateRigidbody.constraints = RigidbodyConstraints.None;

    }

    IEnumerator ButtonScale(float value, bool enable)
    {
        yield return new WaitForSeconds(.1f);
        smashButton.transform.DOScale(value, .5f);
        if (smashButton.transform.localScale.x == 0)
        {
            smashButton.gameObject.SetActive(enable);
        }
    }

    private IEnumerator RollingCan(Transform canFinalPosition, float time)
    {
        yield return new WaitForSeconds(.5f);
        Turning();
        iTween.MoveTo(cokeCan, iTween.Hash(
            "x", canFinalPosition.position.x,
            "y", canFinalPosition.position.y,
            "z", canFinalPosition.position.z,
            "time", time,
            "eastype", iTween.EaseType.easeInExpo
            ));
        yield return new WaitForSeconds(2f);
        StartCoroutine(PositionCheck(canFinalPosition));
    }

    private IEnumerator MoveObjectToPlayer(Transform objectTransform, Transform finalPosition)
    {
        yield return new WaitForSeconds(.1f);
        objectTransform.DOMove(finalPosition.position, .25f);
        objectTransform.DORotate(finalPosition.transform.rotation.eulerAngles, 1);
    }

    IEnumerator PlayerReaction(bool value)
    {
        yield return new WaitForSeconds(.1f);
        if (value)
            StartCoroutine(PlayerTwo.Instance.Cry());
        else
            StartCoroutine(Player.Instance.Cry());
    }

    IEnumerator PositionCheck(Transform transform)
    {
        yield return new WaitForSeconds(.1f);
        if (transform.position == canPos1.position)
        {
            StartCoroutine(PlayerTwo.Instance.Sad());
            StartCoroutine(Player.Instance.Victory());
            StartCoroutine(CameraZoom(camZoomPos1));
        }
        else if (transform.position == canPos2.position)
        {
            StartCoroutine(PlayerTwo.Instance.Victory());
            StartCoroutine(Player.Instance.Sad());
            PlayAnimation();
        }
        else if (transform.position == canPos3.position)
        {
            StartCoroutine(PlayerTwo.Instance.Sad());
            StartCoroutine(Player.Instance.Victory());
            StartCoroutine(CameraZoom(camZoomPos1));
        }
        else if (transform.position == canPos4.position)
        {
            StartCoroutine(PlayerTwo.Instance.Sad());
            StartCoroutine(Player.Instance.Victory());
        }
        else if (transform.position == canPos5.position)
        {
            StartCoroutine(PlayerTwo.Instance.Sad());
            StartCoroutine(Player.Instance.Victory());
            StartCoroutine(CameraZoom(camZoomPos1));
        }
        else
        {
            StartCoroutine(PlayerTwo.Instance.Sad());
            StartCoroutine(Player.Instance.Victory());
        }
    }

    private void PlayAnimation()
    {
        foreach (ParticleSystem item in moneyAnims)
        {
            item.Play();
        }
    }

    float lastRotationX = 0;
    private void Turning(float speed = 1)
    {
        var state = cokeAnim.Play(cokeRotationClip);
        state.Speed = speed;
    }


    private IEnumerator Pushing(int numberOfPlayer)
    {
        if (numberOfPlayer == 1)
        {
            StartCoroutine(PlayerTwo.Instance.Pushing());
            yield return new WaitForSeconds(1);
            if (launchPlatform.Fill <= .2f)
                StartCoroutine(RollingCan(canPos1, 2));
            else if (launchPlatform.Fill <= .4f)
                StartCoroutine(RollingCan(canPos2, 3));
            else if (launchPlatform.Fill <= .6f)
                StartCoroutine(RollingCan(canPos3, 4));
            else if (launchPlatform.Fill <= .8f)
                StartCoroutine(RollingCan(canPos4, 5));
            else if (launchPlatform.Fill <= .9f)
                StartCoroutine(RollingCan(canPos5, 6));
            else
            {
                cokeCan.AddComponent<Rigidbody>();
                StartCoroutine(RollingCan(canPos6, 2));
            }
        }

        yield return new WaitForSeconds(.1f);
    }

}
