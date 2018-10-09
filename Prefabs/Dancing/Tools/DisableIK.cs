// Editor script for disabling IK on a given animation clip. 
// Set the clip you wish to disable IK on as the clip parameter.
// Set the DisableIK animation to the disable IK parameter.
// Click the disable IK button.
//
// Writen by CyanLaser, animation provided by Splinks
// 2018-10-09
// Version 1.0

#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

public class DisableIK : MonoBehaviour {
    public AnimationClip clip;
    public AnimationClip disableIK;

    public void DisableIKOnClip()
    {
        if (clip == null)
        {
            Debug.LogError("Clip is empty");
            return;
        }

        if (disableIK == null)
        {
            Debug.LogError("disableIK animation is empty");
            return;
        }

        bool go = EditorUtility.DisplayDialog(
            "Disable IK", 
            "This script will add parameters to disable IK on the given animation. " +
            "Please make sure you have a backup already. Do you wish to continue?", 
            "Yes", 
            "No"
        );

        if (!go)
        {
            return;
        }
        
        EditorCurveBinding[] bindings = AnimationUtility.GetCurveBindings(disableIK);
        foreach(EditorCurveBinding binding in bindings)
        {
            AnimationCurve curve = AnimationUtility.GetEditorCurve(disableIK, binding);
            clip.SetCurve(binding.path, binding.type, binding.propertyName, curve);
        }
    }
}

[CustomEditor(typeof(DisableIK))]
public class DisableIKEditor : Editor
{
    DisableIK obj;

    public void OnEnable()
    {
        obj = (DisableIK)target;
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("DisableIK"))
        {
            obj.DisableIKOnClip();
        }
    }
}
#endif