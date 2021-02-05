﻿using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LaunchPlatform : MonoBehaviour
{
    public GameObject powerBarGO;
    public Image PowerBarMask;
    public float barChangeSpeed = 1;
    float maxPowerBarValue = 50;
    float currentPowerBarValue;
    bool powerIsIncreasing;
    bool PowerBarON;
    // Start is called before the first frame update
    void Start()
    {
        currentPowerBarValue = 0;
        powerIsIncreasing = true;
        PowerBarON = true;
        StartCoroutine(UpdatePowerBar());
    }

    IEnumerator UpdatePowerBar()
    {
        while (PowerBarON)
        {
            if (!powerIsIncreasing)
            {
                currentPowerBarValue -= barChangeSpeed;
                if (currentPowerBarValue <= 0)
                {
                    powerIsIncreasing = true;
                }
            }
            if (powerIsIncreasing)
            {
                currentPowerBarValue += barChangeSpeed;
                if (currentPowerBarValue >= maxPowerBarValue)
                {
                    powerIsIncreasing = false;
                }
            }

            float fill = currentPowerBarValue / maxPowerBarValue;
            PowerBarMask.fillAmount = fill;
            yield return new WaitForSeconds(0.02f);

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                PowerBarON = false;
                LaunchRocket();
                StartCoroutine(TurnOffPowerBar());
                print(fill);
            }
        }
        yield return null;
    }
    IEnumerator TurnOffPowerBar()
    {
        yield return new WaitForSeconds(3f);
        powerBarGO.transform.DOScale(1.1f, .1f);
        yield return new WaitForSeconds(.1f);
        powerBarGO.transform.DOScale(0,.3f);
        yield return new WaitForSeconds(.4f);
        powerBarGO.SetActive(false);
    }

    public void LaunchRocket()
    {
        Debug.Log("RocketLaunched");
    }
    // Update is called once per frame
    void Update()
    {

    }
}