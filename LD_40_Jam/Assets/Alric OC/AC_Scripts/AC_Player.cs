﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AC_PlayerController))]
public class AC_Player : AC_Living
{
    public enum State { Attack }
    public State currentState;

    public float mFL_moveSpeed = 5;
    public float range = 10f;

    Camera viewCamera;
    AC_PlayerController controller;
    Transform target;
    AC_Living targetEntity;

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        controller = GetComponent<AC_PlayerController>();

        viewCamera = Camera.main;

        if (GameObject.FindGameObjectWithTag("Enemy") != null)
        {
            target = GameObject.FindGameObjectWithTag("Enemy").transform;
            targetEntity = target.GetComponent<AC_Living>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Movement Input
        Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        Vector3 moveVelocity = moveInput.normalized * mFL_moveSpeed;
        controller.Move(moveVelocity);

        //Look Input
        Ray ray = viewCamera.ScreenPointToRay(Input.mousePosition);
        Plane grounPlane = new Plane(Vector3.up, Vector3.up);
        float rayDistance;

        if (grounPlane.Raycast(ray, out rayDistance))
        {
            Vector3 point = ray.GetPoint(rayDistance);
            //Debug.DrawLine(ray.origin,point, Color.red);
            controller.LookAt(point);
        }
    }
}
