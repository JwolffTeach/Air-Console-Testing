using UnityEngine;
using System.Collections;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using System.Linq;

[Serializable]
public class AirConsoleInput : MonoBehaviour {
    public bool grab;
    public bool jump;
    public float joystickX;
    public float joystickY;

    public Text debugText;
    public string json;
    JObject jObject;

    void OnEnable() {
        AirConsole.instance.onMessage += OnMessage;
    }

    void OnDisable() {
        AirConsole.instance.onMessage -= OnMessage;
    }

    void OnMessage(int from, JToken data) {
        print(data);
        JObject jo = data.Value<JObject>();
        jump = (jo.Property("jump") != null);
        if (jump) {
            if ((bool)(data["jump"]["pressed"])){ // Make sure jump pressed is true
                // Do Jump Stuff
            }
        }

        grab = (jo.Property("grab") != null);
        if (grab) {
            if ((bool)(data["grab"]["pressed"])) { // Make sure grab pressed is true
                // Do Grab Stuff
            }
        }

        bool joyStickMoving = (jo.Property("joystick-left") != null);
        if (joyStickMoving) {
            if((bool)(data["joystick-left"]["pressed"])) { // Make sure joystick is pressed
                joystickX = data["joystick-left"]["message"]["x"].Value<float>();
                joystickY = data["joystick-left"]["message"]["y"].Value<float>();
            }
            else {
                joystickX = 0f;
                joystickY = 0f;
            }
        }

        /*
        print(data.ToString());
        JObject jo = data.Value<JObject>();
        List<string> keys = jo.Properties().Select(print => print.Name).ToList();
        if (keys.Contains("jump")) {
            JObject jumpJson = data["jump"].Value<JObject>();
            List<string> jumpKeys = jumpJson.Properties().Select(print => print.Name).ToList();
            if (jumpKeys.Contains("pressed")) {
                JObject pressedJson = data["jump"]["pressed"].Value<JObject>();
                List<string> pressedKeys = pressedJson.Properties().Select(print => print.Name).ToList();
                if (pressedKeys.Contains("true")) {
                    jump = !jump;
                }
            }
        }
        else if (keys.Contains("grab")) {
            print("Grabbed...");
        }
        */

        //JObject grab = data[""].Value<JObject>();
        print("Success.");
        //print(data["jump"].ToString());
        //print(data["jump"]["pressed"].ToString());
        /*if ((string)data["jump"] != null){
            print(data["jump"].Value<string>("pressed"));
        }*/
    }

    private void Update() {
        debugText.text = "Jumping: " + jump + " :: Grabbing: " + grab;
        debugText.text += "\nx: " + joystickX + " y: " + joystickY;
    }
}