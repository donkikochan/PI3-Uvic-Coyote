using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LoginManager))]
public class LoginManagerEditorScript : Editor
{
    // Start is called before the first frame update

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        EditorGUILayout.HelpBox("This script is responsable for connecting to Photon Servers.", MessageType.Info);
        LoginManager loginManager = (LoginManager)target;
        if (GUILayout.Button("Connect Anonymously"))
        {
            loginManager.ConnectAnonymously();
        }

        if (GUILayout.Button("Connect with name Enric"))
        {
            loginManager.UIConnectWithName("Enric");
        }

    }
}
