using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TriggerEvent))]
public class TriggerEventEditor : Editor
{
    public override void OnInspectorGUI()
    {
        TriggerEvent te = (TriggerEvent)target;

        // ====== Collision Section ======
        EditorGUILayout.LabelField("Trigger", EditorStyles.boldLabel);
        EditorGUILayout.Space(10);

        EditorGUILayout.LabelField("Trigger Collider Settings", EditorStyles.boldLabel);
        te.isColliderTriggerActive = EditorGUILayout.Toggle("Is Collider Trigger Active", te.isColliderTriggerActive);
        if (te.isColliderTriggerActive)
        {
            te.triggerPosition = EditorGUILayout.Vector3Field("Collider Position", te.triggerPosition);
            te.triggerdistance = EditorGUILayout.FloatField("Collider Distance", te.triggerdistance);
        }
        EditorGUILayout.Space(10);

        EditorGUILayout.LabelField("Trigger Item Settings", EditorStyles.boldLabel);
        te.isItemTriggerActive = EditorGUILayout.Toggle("Is Item Trigger Active", te.isItemTriggerActive);
        if (te.isItemTriggerActive)
        {
            te.tiggerGetItem = EditorGUILayout.TextField("Trigger Get Item Name", te.tiggerGetItem);
        }
        EditorGUILayout.Space(10);

        // ====== Description ======
        EditorGUILayout.LabelField("Event", EditorStyles.boldLabel);
        EditorGUILayout.Space(10);


        EditorGUILayout.LabelField("Event Description");
        te.isDescription = EditorGUILayout.Toggle("Is Description Active", te.isDescription);
        if (te.isDescription)
        {
            te.eventdescription = EditorGUILayout.TextArea(te.eventdescription, GUILayout.Height(40));
        }

        EditorGUILayout.Space(10);

        // ====== Speech Section ======
        EditorGUILayout.LabelField("Event Speech", EditorStyles.boldLabel);
        te.isSpeech = EditorGUILayout.Toggle("Is Speech Active", te.isSpeech);
        if (te.isSpeech)
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("eventSpeeches"), new GUIContent("Event Speeches"), true);
        }
        EditorGUILayout.Space(10);

        // ====== Others ======
        EditorGUILayout.LabelField("Event Scene Change", EditorStyles.boldLabel);
        te.isSceneChange = EditorGUILayout.Toggle("Is Scene Change Active", te.isSceneChange);
        if (te.isSceneChange)
        {
            te.eventscenechange = EditorGUILayout.TextField("Event Scene Change", te.eventscenechange);
        }
        EditorGUILayout.Space(10);

        EditorGUILayout.LabelField("Event Sound", EditorStyles.boldLabel);
        te.isSound = EditorGUILayout.Toggle("Is Sound Active", te.isSound);
        if (te.isSound)
        {
            te.sound = (AudioClip)EditorGUILayout.ObjectField("Audio", te.sound, typeof(AudioClip), false);
        }
        EditorGUILayout.Space(10);

        EditorGUILayout.LabelField("Event Save", EditorStyles.boldLabel);
        te.isSave = EditorGUILayout.Toggle("Is Save Active", te.isSave);
        if (te.isSave)
        {
            te.save = EditorGUILayout.IntField("Save", te.save);
        }
        EditorGUILayout.Space(10);

        EditorGUILayout.LabelField("Event Fade", EditorStyles.boldLabel);
        te.isFadeOut = EditorGUILayout.Toggle("Is Fade Out Active", te.isFadeOut);
        te.isFadeIn = EditorGUILayout.Toggle("IsFade In Active", te.isFadeIn);
        EditorGUILayout.Space(10);

        EditorGUILayout.LabelField("Event Spawn", EditorStyles.boldLabel);
        te.isSpawnObject = EditorGUILayout.Toggle("Is Spawn Active", te.isSpawnObject);
        if (te.isSpawnObject)
        {
            te.SpawnPrefabName = EditorGUILayout.TextField("Spawn Object Prefab Name", te.SpawnPrefabName);
            te.SpawnNaming = EditorGUILayout.TextField("Spawn Naming", te.SpawnNaming);
            te.SpawnObjectPosition = EditorGUILayout.Vector3Field("Spawn Object Position", te.SpawnObjectPosition);
        }
        EditorGUILayout.Space(10);

        EditorGUILayout.LabelField("Event Player Follow Start", EditorStyles.boldLabel);
        te.isFollow = EditorGUILayout.Toggle("Is Player Follow Active", te.isFollow);
        if (te.isFollow)
        {
            te.eventfollower = EditorGUILayout.TextField("Event Follower", te.eventfollower);
        }
        EditorGUILayout.Space(10);

        EditorGUILayout.LabelField("Event Patrol Start", EditorStyles.boldLabel);
        te.isPatrol = EditorGUILayout.Toggle("Is Patrol Active", te.isPatrol);
        if (te.isPatrol)
        {
            te.PatrolObjectName = EditorGUILayout.TextField("Patrol Object Prefab Name", te.PatrolObjectName);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("eventPatrolPositions"), new GUIContent("Patrol Positions"), true);
        }

        if (GUI.changed)
        {
            EditorUtility.SetDirty(te);
        }
    }
}
