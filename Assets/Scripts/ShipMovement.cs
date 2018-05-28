﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour {
    public Transform ship;
    [Range (0.01f, .5f)]
    public float snapSpeed = .1f;
    public TouchAxisCtrl touchAxisControl;
    public float xRange = 4f;
    public float yRange = 4f;

    private Vector2 m_InitialXY;
    Vector2 m_TargetXY;
    
    void Start()
    {
        float range = Mathf.Min(Screen.width, Screen.height) / 4;
    }

	// Update is called once per frame
	void Update () {
        if (touchAxisControl.IsTouching())
        {
            m_TargetXY = new Vector3(Mathf.Clamp(ship.localPosition.x + touchAxisControl.GetAxis("Horizontal") , -xRange, xRange),
                                     Mathf.Clamp(ship.localPosition.y + touchAxisControl.GetAxis("Vertical") , -yRange, yRange));
        }
        Vector2 lerpPos = Vector2.Lerp(new Vector2(ship.localPosition.x, ship.localPosition.y), m_TargetXY, snapSpeed);
        ship.localEulerAngles = new Vector3(Remap(ship.localPosition.y - m_TargetXY.y, -yRange, yRange, -30, 30), 
                                            ship.localEulerAngles.y, 
                                            Remap(ship.localPosition.x - m_TargetXY.x, -xRange, xRange, -90, 90));
        ship.localPosition = new Vector3(lerpPos.x, lerpPos.y, ship.localPosition.z);
    }

    float Remap(float val, float srcMin, float srcMax, float destMin, float destMax)
    {
        return Mathf.Lerp(destMin, destMax, Mathf.InverseLerp(srcMin, srcMax, val));
    }
}
