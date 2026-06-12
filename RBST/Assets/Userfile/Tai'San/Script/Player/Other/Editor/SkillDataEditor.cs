using UnityEditor;

//[CustomEditor(typeof(JobData))]
//public class SkillDataEditor : Editor
//{
//    private SerializedProperty motion_;

//    void OnEnable()
//    {
//        motion_ = serializedObject.FindProperty("motion_");
//    }

//    public override void OnInspectorGUI()
//    {
//        var job = (JobData)target;

//        serializedObject.Update();

//        EditorGUILayout.PropertyField(motion_, true);

//        serializedObject.ApplyModifiedProperties();

//        DrawDefaultInspector(); // 元のインスペクター表示

//        foreach (SkillData skill in job.GetSkill1())
//        {
//            if (EditorGUILayout.Toggle("Motion", skill.useMotion_))
//            {
//                EditorGUILayout.PropertyField(motion_, true);
//            }
//        }
//    }
//}
