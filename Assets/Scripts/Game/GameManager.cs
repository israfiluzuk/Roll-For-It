using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    [SerializeField] GameObject cokeCan;
    [SerializeField] Transform manPlayer, womanPlayer,playingPosition,stayingPosition,camZoomPos1;
    [SerializeField] Transform canPos0, canPos1, canPos2, canPos3, canPos4, canPos5;
    [SerializeField] Button smashButton;
    [SerializeField] LaunchPlatform launchPlatform;

    Quaternion vector = Quaternion.identity;


    IEnumerator CameraZoom(Transform transform)
    {
        yield return new WaitForSeconds(.5f);
        mainCamera.transform.DOMove(transform.position, .5f);
        mainCamera.transform.DORotate(new Vector3(15,90,0),.5f);
        yield return ButtonScale(1,true);
    }

    private void Start()
    {
        smashButton.onClick.AddListener(()=>SmashFunction());
    }

    private void SmashFunction()
    {
        StartCoroutine(ButtonScale(0,false));
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
        StartCoroutine(Turning());
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

    IEnumerator PositionCheck(Transform transform)
    {
        yield return new WaitForSeconds(.1f);
        if (cokeCan.transform.position == transform.position)
        {
            PlayerTwo.Instance.Sad();
            StartCoroutine(Player.Instance.Victory());
            StartCoroutine(CameraZoom(camZoomPos1));
        }
    }

    private IEnumerator Turning()
    {
        vector.eulerAngles = new Vector3(500, -90, -90);
        cokeCan.transform.DORotateQuaternion(vector, 1f);
        yield return new WaitForSeconds(.1f);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            RollingCan(canPos1, 2);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(Pushing(1));
            launchPlatform.PowerBarON = false;
            launchPlatform.LaunchRocket();
            launchPlatform.StartCoroutine(launchPlatform.TurnOffPowerBar());
            print(launchPlatform.Fill);
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            Pushing(2);
        }

    }

    private IEnumerator Pushing(int numberOfPlayer)
    {
        if (numberOfPlayer == 1)
        {
            StartCoroutine(PlayerTwo.Instance.Pushing());
            yield return new WaitForSeconds(1);
            StartCoroutine(RollingCan(canPos1, 2));
        }

        yield return new WaitForSeconds(.1f);
    }

}
