using UnityEngine;
using System.Collections;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityStandardAssets._2D;

[Serializable]
public class AirConsoleInput : MonoBehaviour {

    public float m_Horizontal;

    [SerializeField] Platformer2DUserControl[] characterController;



    void OnEnable() {
        AirConsole.instance.onMessage += OnMessage;
    }

    void OnDisable() {
        AirConsole.instance.onMessage -= OnMessage;
    }

    void OnMessage(int from, JToken data) {
        // Parse the message data.
        string element = (string)data["element"];
        string direction = (string)data["data"]["direction"];
        bool pressed = (bool)data["data"]["pressed"];
        HorizontalMovement(from, direction, pressed);
    }

    private void HorizontalMovement(int from, string direction, bool pressed) {
        // Decide how to move horizontally.
        if (pressed == true) {
            if (direction == "right") {
                m_Horizontal = 1;
            }
            else if (direction == "left") {
                m_Horizontal = -1;
            }
            characterController[from - 1].SetHorizontal(m_Horizontal);
        }
        else {
            characterController[from - 1].SlowDown();
        }
    }

    private void Update() {

    }

    private void FixedUpdate() {

    }
}