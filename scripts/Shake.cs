using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour
{

    private Transform cameraStart;
    private Vector3 cameraBeginPos;
    private float shakeAmountX;
    private float shakeAmountY;
    private float time = 0;
    private bool reset = true;

    public void Start()
    {
        cameraStart = Camera.main.gameObject.transform;
        cameraBeginPos = cameraStart.position;

        InvokeRepeating("CameraShake", 0, .01f);

    }

    public void ScreenShake(float amount, float _time)
    {
        cameraBeginPos = cameraStart.position;

        if (amount > shakeAmountX && amount > shakeAmountY)
        {
            shakeAmountX = amount;
            shakeAmountY = amount;
        }
        if (_time > time)
        {
            time = _time;
        }
        reset = false;
    }

    void CameraShake()
    {
        if (shakeAmountX > 0 && shakeAmountY > 0 && time > 0)
        {
            time -= .01f;
            float quakeAmtX = UnityEngine.Random.value * Mathf.Sin(shakeAmountX) * 2 - shakeAmountX;
            float quakeAmtY = UnityEngine.Random.value * Mathf.Sin(shakeAmountY) * 2 - shakeAmountY;
            Vector3 pp = cameraBeginPos;
            pp.y += quakeAmtY;
            pp.x += quakeAmtX;
            Camera.main.transform.position = pp;
        }
        else if (!reset)
        {
            Camera.main.transform.position = cameraBeginPos;
            reset = true;
        }
    }

}