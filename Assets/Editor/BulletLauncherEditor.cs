using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BulletLauncher), true)]
public class BulletLauncherEditor : Editor 
{
    protected static string LAUNCH_PATTERN_FACTORY_NAME = "launchPatternFactory";

    protected BulletLauncher _bulletLauncher;
    protected SerializedObject _serializedBulletLauncher;
    protected SerializedProperty _serializedILaunchPattern;

    private void OnEnable()
    {
        _bulletLauncher = (BulletLauncher)target;
        _serializedBulletLauncher = new SerializedObject( _bulletLauncher );
        _serializedILaunchPattern = _serializedBulletLauncher.FindProperty(LAUNCH_PATTERN_FACTORY_NAME);
    }

    public override void OnInspectorGUI()
    {
        _serializedBulletLauncher.Update();

        DrawPropertiesExcluding(_serializedBulletLauncher, new string[] { LAUNCH_PATTERN_FACTORY_NAME });
        DrawILaunchPattern();
        _serializedBulletLauncher.ApplyModifiedProperties();
    }

    protected void DrawILaunchPattern()
    {
        EditorGUILayout.LabelField(LAUNCH_PATTERN_FACTORY_NAME, EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(_serializedILaunchPattern.FindPropertyRelative("Type"));

        LaunchPatternFactory launchPatternFactory = _bulletLauncher.launchPatternFactory;
        System.Type typeOfLaunchPattern = launchPatternFactory.GetClassType(launchPatternFactory.Type);
        SerializedProperty specificLaunchPattern = _serializedILaunchPattern.FindPropertyRelative(typeOfLaunchPattern.ToString());
        string parentPath = specificLaunchPattern.propertyPath;
        while (specificLaunchPattern.NextVisible(true) && specificLaunchPattern.propertyPath.StartsWith(parentPath))
        {
            EditorGUILayout.PropertyField(specificLaunchPattern);
        }
    }
}
