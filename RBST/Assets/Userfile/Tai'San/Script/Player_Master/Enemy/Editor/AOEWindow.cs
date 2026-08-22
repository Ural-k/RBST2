using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AOEWindow : EditorWindow
{
    AttackTimelineData data;
    AttackEvent selected;
    AttackEvent dragging;

    float pixelsPerSecond = 80f;
    Vector2 scroll;
    const float trackHeight = 40f;
    const float rulerHeight = 24f;
    const float snap = 0.5f;

    [MenuItem("Tools/Attack Timeline")]
    static void Open() => GetWindow<AOEWindow>("Attack Timeline");

    void OnSelectionChange()
    {
        if (Selection.activeObject is AttackTimelineData d)
        {
            data = d;
            selected = null;
            Repaint();
        }
    }

    void OnGUI()
    {
        data = (AttackTimelineData)EditorGUILayout.ObjectField("Timeline Data", data, typeof(AttackTimelineData), false);
        if (data == null)
        {
            EditorGUILayout.HelpBox("AttackTimelineData アセットを選択・アサインしてください", MessageType.Info);
            return;
        }

        EditorGUILayout.BeginHorizontal();
        data.duration = Mathf.Max(1f, EditorGUILayout.FloatField("Duration(s)", data.duration));
        pixelsPerSecond = EditorGUILayout.Slider("Zoom", pixelsPerSecond, 20f, 300f);
        EditorGUILayout.EndHorizontal();

        float timelineHeight = rulerHeight + trackHeight + 8;
        Rect timelineRect = GUILayoutUtility.GetRect(
            10, 4000, timelineHeight, timelineHeight, GUILayout.ExpandWidth(true));
        DrawTimeline(timelineRect);
        EditorGUILayout.HelpBox("ダブルクリック: イベント追加 / ドラッグ: 時間移動(0.5s刻み) / 右クリック: メニュー", MessageType.None);

        DrawInspector();
    }

    void DrawTimeline(Rect rect)
    {
        float width = Mathf.Max(rect.width, data.duration * pixelsPerSecond + 40);
        scroll = GUI.BeginScrollView(rect, scroll, new Rect(0, 0, width, rect.height));

        // ルーラー(0.5秒刻みの目盛り)
        EditorGUI.DrawRect(new Rect(0, 0, width, rulerHeight), new Color(0.15f, 0.15f, 0.15f));
        int steps = Mathf.CeilToInt(data.duration / snap);
        for (int i = 0; i <= steps; i++)
        {
            float t = i * snap;
            float x = t * pixelsPerSecond;
            bool isWholeSecond = Mathf.Approximately(t % 1f, 0f);
            Handles.color = isWholeSecond ? Color.gray : new Color(0.4f, 0.4f, 0.4f);
            Handles.DrawLine(new Vector3(x, 0), new Vector3(x, rect.height));
            if (isWholeSecond)
                GUI.Label(new Rect(x + 2, 2, 40, 16), t.ToString("0") + "s", EditorStyles.miniLabel);
        }

        // トラック背景
        Rect trackRect = new Rect(0, rulerHeight, width, trackHeight);
        EditorGUI.DrawRect(trackRect, new Color(0.2f, 0.2f, 0.2f));

        // 各イベントを描画 & 入力処理
        foreach (var ev in data.events)
        {
            float x = ev.time * pixelsPerSecond;
            Rect box = new Rect(x, rulerHeight + 4, 60, trackHeight - 8);
            EditorGUI.DrawRect(box, ev == selected ? Color.yellow : ev.trackColor);
            string label = ev.attack != null ? ev.attack.name : "None";
            GUI.Label(box, label, EditorStyles.whiteMiniLabel);

            HandleEventInput(box, ev);
        }

        HandleTrackInput(trackRect);

        GUI.EndScrollView();
    }

    void HandleEventInput(Rect box, AttackEvent ev)
    {
        Event e = Event.current;
        switch (e.type)
        {
            case EventType.MouseDown:
                if (box.Contains(e.mousePosition))
                {
                    selected = ev;
                    dragging = ev;
                    e.Use();
                    Repaint();
                }
                break;
            case EventType.MouseDrag:
                if (dragging == ev)
                {
                    ev.time = Mathf.Clamp(SnapTime(ev.time + e.delta.x / pixelsPerSecond), 0, data.duration);
                    EditorUtility.SetDirty(data);
                    e.Use();
                    Repaint();
                }
                break;
            case EventType.MouseUp:
                if (dragging == ev) { dragging = null; e.Use(); }
                break;
        }
    }

    void HandleTrackInput(Rect trackRect)
    {
        Event e = Event.current;
        if (!trackRect.Contains(e.mousePosition)) return;

        if (e.type == EventType.MouseDown && e.button == 0)
        {
            if (e.clickCount == 2)
            {
                AddEvent(e.mousePosition.x / pixelsPerSecond);
            }
            else
            {
                selected = null;
            }
            Repaint();
        }

        if (e.type == EventType.ContextClick)
        {
            float t = e.mousePosition.x / pixelsPerSecond;
            GenericMenu menu = new GenericMenu();
            menu.AddItem(new GUIContent("Add Event Here"), false, () => AddEvent(t));
            menu.ShowAsContext();
            e.Use();
        }
    }

    float SnapTime(float t) => Mathf.Round(t / snap) * snap;

    void AddEvent(float rawTime)
    {
        Undo.RecordObject(data, "Add Attack Event");
        var ev = new AttackEvent { time = Mathf.Clamp(SnapTime(rawTime), 0, data.duration) };
        data.events.Add(ev);
        selected = ev;
        EditorUtility.SetDirty(data);
    }

    void DrawInspector()
    {
        if (selected == null) return;

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("選択中のイベント", EditorStyles.boldLabel);
        EditorGUI.BeginChangeCheck();
        selected.time = Mathf.Clamp(SnapTime(EditorGUILayout.FloatField("Time", selected.time)), 0, data.duration);
        selected.attack = (ScriptableObject)EditorGUILayout.ObjectField("Attack", selected.attack, typeof(ScriptableObject), false);
        selected.offset = EditorGUILayout.Vector2Field("Offset", selected.offset);
        selected.trackColor = EditorGUILayout.ColorField("Color", selected.trackColor);
        if (EditorGUI.EndChangeCheck()) EditorUtility.SetDirty(data);

        if (GUILayout.Button("Delete Event"))
        {
            Undo.RecordObject(data, "Delete Attack Event");
            data.events.Remove(selected);
            selected = null;
            EditorUtility.SetDirty(data);
        }
    }
}

 
[CreateAssetMenu(fileName = "NewAttackTimeline", menuName = "Battle/Attack Timeline")]
public class AttackTimelineData : ScriptableObject
{
    public float duration = 10f;
    public List<AttackEvent> events = new List<AttackEvent>();
}

// タイムライン上の1イベント(1つの攻撃発生)
[System.Serializable]
public class AttackEvent
{
    public float time;                    // 発生時間(秒) 0.5秒単位でスナップされる
    public ScriptableObject attack;       // 既存の攻撃アセット(自分のAttackBase等に置き換えてOK)
    public Vector2 offset;                // 発生位置オフセット(自由に使ってください)
    public Color trackColor = new Color(0.3f, 0.6f, 1f);
}