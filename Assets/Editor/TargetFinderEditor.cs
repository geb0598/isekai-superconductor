using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TargetFinder), true)]
public class TargetFinderEditor : Editor
{
    protected static string FINDER_FACTORY_NAME = "finderFactory";

    protected TargetFinder _targetFinder;
    protected SerializedObject _serializedTargetFinder;
    protected SerializedProperty _serializedIFinder;

    private void OnEnable()
    {
        _targetFinder = (TargetFinder)target;
        _serializedTargetFinder = new SerializedObject(_targetFinder);
        _serializedIFinder = _serializedTargetFinder.FindProperty(FINDER_FACTORY_NAME);
    }

    public override void OnInspectorGUI()
    {
        _serializedTargetFinder.Update();

        DrawPropertiesExcluding(_serializedTargetFinder, new string[] { FINDER_FACTORY_NAME });
        DrawITarget();
        _serializedTargetFinder.ApplyModifiedProperties();
    }

    protected void DrawITarget()
    {
        EditorGUILayout.LabelField(FINDER_FACTORY_NAME, EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(_serializedIFinder.FindPropertyRelative("Type"));

        FinderFactory finderFactory = _targetFinder.finderFactory;
        System.Type typeOfFinder = finderFactory.GetClassType(finderFactory.Type);
        SerializedProperty specificFinder = _serializedIFinder.FindPropertyRelative(typeOfFinder.ToString());
        string parentPath = specificFinder.propertyPath;
        while (specificFinder.NextVisible(true) && specificFinder.propertyPath.StartsWith(parentPath))
        {
            EditorGUILayout.PropertyField(specificFinder);
        }
    }
}
