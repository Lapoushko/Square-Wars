using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    Camera mainCam;
    public Transform player;
    public float zoomVel;
    public float zoomSpeed;

    [Header("Controller")]
    GameObject controller;
    ControllerInGame controllerInGame;

    private void Start()
    {
        controller = GameObject.Find("Controller");
        controllerInGame = controller.GetComponent<ControllerInGame>();
        mainCam = Camera.main;
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
    }

    private void Update()
    {
        if (controllerInGame.PlayerDeath == false)
        {
            float target = Mathf.Abs(player.transform.position.x);
            this.mainCam.orthographicSize = Mathf.SmoothDamp(this.mainCam.orthographicSize, target, ref this.zoomVel, this.zoomSpeed);
        }
    }
}
