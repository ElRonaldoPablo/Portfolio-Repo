using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CustomEditorScriptStudies : EditorWindow
{
    int _color;
    Color _color0;
    Color _color1;
    Color _color2;
    Color _color3;

    private string _testStringTextField = "Enter string here";

    [MenuItem("Custom Tools/Lighting/Illumina")]
    public static void ShowIlluminaWindow()
    {
        GetWindow<CustomEditorScriptStudies>("Illumina");
    }

    // Window Code
    private void OnGUI()
    {
        GUILayout.Label("This is a Help Box Label.", EditorStyles.helpBox);

        //_testStringTextField = EditorGUILayout.TextField("String", _testStringTextField);

        _color0 = EditorGUILayout.ColorField("Color 0", _color0);
        _color1 = EditorGUILayout.ColorField("Color 1", _color1);
        _color2 = EditorGUILayout.ColorField("Color 2", _color2);
        _color3 = EditorGUILayout.ColorField("Color 3", _color3);

        if (GUILayout.Button("Button 0"))
        {
            TestFunction();
            Debug.Log("NOTICE: Button 0 pressed.");
            Debug.Log(_color);
        }

        GUILayout.Label("This is a Centered Grey Mini Label.", EditorStyles.centeredGreyMiniLabel);
    }

    private void TestFunction()
    {
        foreach (GameObject obj in Selection.gameObjects)
        {
            Renderer _renderer = obj.GetComponent<Renderer>();
            _color = Random.Range(0, 4);

            if (_renderer != null)
            {
                if (_color == 0)
                    _renderer.material.color = _color0;
                else if (_color == 1)
                    _renderer.material.color = _color1;
                else if (_color == 2)
                    _renderer.material.color = _color2;
                else
                    _renderer.material.color = _color3;
            }
        }

    }
}