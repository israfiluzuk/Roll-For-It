using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LaunchPlatform : MonoBehaviour
{
    public GameObject powerBarGO;
    public GameObject powerBarGOBG;
    public Image PowerBarMask;
    public float barChangeSpeed = 1;
    float maxPowerBarValue = 50;
    float currentPowerBarValue;
    bool powerIsIncreasing;
    public bool PowerBarON;
    public float Fill;
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

            Fill = currentPowerBarValue / maxPowerBarValue;
            PowerBarMask.fillAmount = Fill;
            yield return new WaitForSeconds(0.02f);

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                PowerBarON = false;
                LaunchRocket();
                StartCoroutine(TurnOffPowerBar());
                print(Fill);
            }
        }
        yield return null;
    }
    public IEnumerator TurnOffPowerBar()
    {
        yield return new WaitForSeconds(3f);
        powerBarGO.transform.DOScale(1.1f, .1f);
        powerBarGOBG.transform.DOScale(1.1f, .1f);
        yield return new WaitForSeconds(.1f);
        powerBarGO.transform.DOScale(0,.3f);
        powerBarGOBG.transform.DOScale(0,.3f);
        powerBarGO.SetActive(false);
        powerBarGOBG.SetActive(false);
        yield return new WaitForSeconds(.4f);
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