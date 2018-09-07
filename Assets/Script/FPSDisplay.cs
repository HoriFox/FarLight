﻿using UnityEngine;

public class FPSDisplay : MonoBehaviour
{
    float deltaTime = 0.0f;

    void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
    }

    void OnGUI()
    {
        int w = Screen.width, h = Screen.height;

        GUIStyle style = new GUIStyle();

        int heightGui = h * 2 / 100;

        Rect rect = new Rect(0, h - heightGui, w, heightGui);
        style.alignment = TextAnchor.MiddleLeft;
        style.fontSize = heightGui;
        style.normal.textColor = new Color(255.0f, 255.0f, 255.5f, 1.0f);
        float msec = deltaTime * 1000.0f;
        float fps = 1.0f / deltaTime;
        string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
        GUI.Label(rect, text, style);
    }
}
