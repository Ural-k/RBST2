using System.Collections.Generic;
using System;
using UnityEditor;
using UnityEngine;

public class ButtleTimeLineWindow : EditorWindow
{
    // ---- 定数 ----
    const float TOOLBAR_H = 28f;
    const float TIMELINE_H = 28f;
    const float LABEL_W = 60f;
    const float MIN_ZOOM = 0.02f, MAX_ZOOM = 20f;

    // Unity Scene 座標系と一致させる
    // 16:9 で高さ10unit → 幅 17.777...unit
    const float SCENE_H = 10f;
    const float SCENE_W = SCENE_H * 16f / 9f;   // ≈ 17.778
    const float GRID_BASE = 1f;                    // 1unit = 1グリッド

    // ピクセル換算係数: キャンバス上で何px/unit にするか（zoom=1 時）
    // 仮想空間は SCENE_H=10 unit が画面の適切なサイズに見えるよう調整
    const float UNIT_PX = 50f;  // zoom=1 で 1unit = 50px

    // ---- ビュー状態 ----
    Vector2 _offset = Vector2.zero;   // 仮想原点のキャンバスpx位置
    float _zoom = 1f;            // 1unit = UNIT_PX * _zoom px
    bool _panning;
    Vector2 _lastMouse;

    // ---- データ ----
    AttackTimelineData _data;

    // ---- UI状態 ----
    ShapeType _selectedShapeType = ShapeType.Circle;
    Vector2 _timelineScroll;
    float _previewTime = 0f;

    // ---- 右クリック長押し移動 ----
    const float LONG_PRESS_SEC = 0.25f;   // 長押し判定秒数
    AttackShape _rmbDownShape;            // 右クリック押下時のヒット図形
    double _rmbDownTime;
    Vector2 _rmbDownMouse;
    bool _isDraggingShape;         // 長押し移動中フラグ
    Vector2 _dragShapeOffset;         // 図形中心とマウスのずれ（仮想座標）
    bool _rmbMoved;               // 押下後マウスが動いたか

    // ---- コンテキストメニュー遅延実行 ----
    AttackShape _contextShape;
    bool _showContextMenu;

    // ---- 再生 ----
    bool _isPlaying;
    double _playStartEditorTime;
    float _playStartPreviewTime;

    // ---- 色 ----
    static readonly Color CBg = new Color(0.15f, 0.15f, 0.15f);
    static readonly Color CMinor = new Color(0.25f, 0.25f, 0.25f);
    static readonly Color CMajor = new Color(0.35f, 0.35f, 0.35f);
    static readonly Color COrigin = new Color(0.50f, 0.50f, 0.50f, 0.8f);
    static readonly Color CBorder = new Color(0.20f, 0.75f, 1.00f);
    static readonly Color CFill = new Color(0.20f, 0.75f, 1.00f, 0.04f);
    static readonly Color CCorner = new Color(1f, 1f, 1f, 0.9f);

    [MenuItem("Window/Virtual Space Editor")]
    static void Open()
    {
        var w = GetWindow<ButtleTimeLineWindow>("Virtual Space");
        w.minSize = new Vector2(600, 500);
    }

    void OnEnable() => EditorApplication.update += OnEditorUpdate;
    void OnDisable() => EditorApplication.update -= OnEditorUpdate;

    // EditorApplication.update で再生時刻を進める
    void OnEditorUpdate()
    {
        if (!_isPlaying || _data == null) return;
        float elapsed = (float)(EditorApplication.timeSinceStartup - _playStartEditorTime);
        _previewTime = _playStartPreviewTime + elapsed;
        if (_previewTime >= _data.totalDuration)
        {
            _previewTime = _data.totalDuration;
            _isPlaying = false;
        }
        Repaint();
    }

    void StartPlay()
    {
        _isPlaying = true;
        _playStartEditorTime = EditorApplication.timeSinceStartup;
        _playStartPreviewTime = _previewTime;
    }
    void PausePlay() => _isPlaying = false;
    void StopPlay() { _isPlaying = false; _previewTime = 0f; }

    // ==========================================================
    //  OnGUI
    // ==========================================================
    void OnGUI()
    {
        EnsureData();
        float ch = position.height * 2f / 3f;
        var canvas = new Rect(0, 0, position.width, ch);
        if (_offset == Vector2.zero) ResetView(canvas);

        HandleCanvasInput(canvas);
        DrawCanvas(canvas);
        DrawTopToolbar(ch);
        DrawBottomArea(ch);

        if (_showContextMenu && _contextShape != null)
        { _showContextMenu = false; ShowShapeContextMenu(_contextShape); }
    }

    // ==========================================================
    //  データ
    // ==========================================================
    void EnsureData()
    {
        if (_data != null) return;
        var guids = AssetDatabase.FindAssets("t:AttackTimelineData");
        _data = guids.Length > 0
            ? AssetDatabase.LoadAssetAtPath<AttackTimelineData>(AssetDatabase.GUIDToAssetPath(guids[0]))
            : CreateNewData();
    }

    AttackTimelineData CreateNewData()
    {
        var d = CreateInstance<AttackTimelineData>();
        AssetDatabase.CreateAsset(d, "Assets/AttackTimelineData.asset");
        AssetDatabase.SaveAssets();
        return d;
    }

    void SaveData() { EditorUtility.SetDirty(_data); AssetDatabase.SaveAssets(); }

    // zoom=1 でキャンバス中央に 16:9 枠が収まるビューにリセット
    void ResetView(Rect r)
    {
        // 16:9 枠の高さ(SCENE_H unit)がキャンバス高さの 80% になるズームを選ぶ
        _zoom = (r.height * 0.8f) / (SCENE_H * UNIT_PX);
        _offset = new Vector2(r.width * .5f, r.height * .5f);
    }

    // ==========================================================
    //  座標変換
    //  仮想座標(unit) → キャンバスpx:  v * (UNIT_PX * zoom) + offset
    // ==========================================================
    float Scale => UNIT_PX * _zoom;                         // px/unit
    Vector2 ToCanvas(Vector2 v) => v * Scale + _offset;
    Vector2 ToVirtual(Vector2 s) => (s - _offset) / Scale;

    // ==========================================================
    //  キャンバス入力
    // ==========================================================
    void HandleCanvasInput(Rect canvas)
    {
        var e = Event.current;
        if (e == null) return;
        bool inside = canvas.Contains(e.mousePosition);

        // ---- ホイール ズーム ----
        if (e.type == EventType.ScrollWheel && inside)
        {
            var wv = ToVirtual(e.mousePosition);
            _zoom = Mathf.Clamp(_zoom - e.delta.y * 0.05f * _zoom, MIN_ZOOM, MAX_ZOOM);
            _offset = e.mousePosition - wv * Scale;
            e.Use(); Repaint(); return;
        }

        // ---- 中ボタン / Alt+左 パン ----
        if (e.type == EventType.MouseDown && inside &&
            (e.button == 2 || (e.button == 0 && e.alt)))
        { _panning = true; _lastMouse = e.mousePosition; e.Use(); return; }

        if (e.type == EventType.MouseDrag && _panning)
        { _offset += e.mousePosition - _lastMouse; _lastMouse = e.mousePosition; e.Use(); Repaint(); return; }

        if (e.type == EventType.MouseUp && _panning)
        { _panning = false; e.Use(); return; }

        // ---- 左クリック 図形設置 ----
        if (e.type == EventType.MouseDown && e.button == 0 && inside && !e.alt && !_panning)
        {
            if (HitTestShape(e.mousePosition) == null)
            { PlaceShape(ToVirtual(e.mousePosition)); e.Use(); Repaint(); }
            return;
        }

        // ---- 右クリック 長押し判定 ----
        if (e.type == EventType.MouseDown && e.button == 1 && inside)
        {
            var hit = HitTestShape(e.mousePosition);
            _rmbDownShape = hit;
            _rmbDownTime = EditorApplication.timeSinceStartup;
            _rmbDownMouse = e.mousePosition;
            _rmbMoved = false;
            _isDraggingShape = false;
            if (hit != null) e.Use();
            return;
        }

        if (e.type == EventType.MouseDrag && e.button == 1 && _rmbDownShape != null)
        {
            float elapsed = (float)(EditorApplication.timeSinceStartup - _rmbDownTime);
            float moved = Vector2.Distance(e.mousePosition, _rmbDownMouse);
            if (moved > 3f) _rmbMoved = true;

            // 長押し確定 → 移動モード開始
            if (!_isDraggingShape && (elapsed >= LONG_PRESS_SEC || moved > 8f))
            {
                _isDraggingShape = true;
                _dragShapeOffset = _rmbDownShape.position - ToVirtual(_rmbDownMouse);
            }

            if (_isDraggingShape)
            {
                Undo.RecordObject(_data, "Move Shape");
                _rmbDownShape.position = ToVirtual(e.mousePosition) + _dragShapeOffset;
                e.Use(); Repaint();
            }
            return;
        }

        if (e.type == EventType.MouseUp && e.button == 1)
        {
            if (_isDraggingShape)
            {
                // 移動確定
                SaveData();
                _isDraggingShape = false;
                _rmbDownShape = null;
                e.Use();
            }
            else if (_rmbDownShape != null && !_rmbMoved)
            {
                // 短押し → コンテキストメニュー
                _contextShape = _rmbDownShape;
                _showContextMenu = true;
                _rmbDownShape = null;
                e.Use(); Repaint();
            }
            else
            {
                _rmbDownShape = null;
            }
            return;
        }

        // ドラッグ中はカーソルを変更してフィードバック
        if (_isDraggingShape && e.type == EventType.Repaint)
            EditorGUIUtility.AddCursorRect(canvas, MouseCursor.MoveArrow);
    }

    AttackShape HitTestShape(Vector2 mouseCanvas)
    {
        if (_data == null) return null;
        foreach (var s in _data.shapes)
        {
            var cp = ToCanvas(s.position);
            float r = Mathf.Max(s.width, s.height) * 0.5f * Scale + 6f;
            if (s.shapeType == ShapeType.Donut)
                r = s.outerRadius * Scale + 6f;
            if (Vector2.Distance(mouseCanvas, cp) < r) return s;
        }
        return null;
    }

    void PlaceShape(Vector2 virtualPos)
    {
        var shape = new AttackShape
        {
            shapeType = _selectedShapeType,
            position = virtualPos,
            timeline = 0,
            startTime = _previewTime,
        };
        Undo.RecordObject(_data, "Place Shape");
        _data.shapes.Add(shape);
        SaveData();
    }

    // ==========================================================
    //  キャンバス描画
    // ==========================================================
    void DrawCanvas(Rect canvas)
    {
        EditorGUI.DrawRect(canvas, CBg);
        GUI.BeginClip(canvas);
        DrawGrid(canvas);
        DrawOrigin(canvas);
        DrawRect169();
        DrawShapes();
        DrawZoomLabel();
        GUI.EndClip();
        Handles.DrawSolidRectangleWithOutline(canvas, Color.clear, new Color(0.08f, 0.08f, 0.08f));
    }

    void DrawGrid(Rect r)
    {
        // GRID_BASE unit 間隔。ズームにより動的調整
        float step = GRID_BASE * Scale;
        while (step < 10f) step *= 5f;
        while (step > 100f) step /= 5f;
        float major = step * 5f;

        for (float wx = Mathf.Floor(-_offset.x / step) * step; wx < r.width - _offset.x + step; wx += step)
        {
            float sx = wx + _offset.x;
            if (sx < 0 || sx > r.width) continue;
            Handles.color = Mathf.Abs(wx % major) < .5f ? CMajor : CMinor;
            Handles.DrawLine(new Vector3(sx, 0), new Vector3(sx, r.height));
        }
        for (float wy = Mathf.Floor(-_offset.y / step) * step; wy < r.height - _offset.y + step; wy += step)
        {
            float sy = wy + _offset.y;
            if (sy < 0 || sy > r.height) continue;
            Handles.color = Mathf.Abs(wy % major) < .5f ? CMajor : CMinor;
            Handles.DrawLine(new Vector3(0, sy), new Vector3(r.width, sy));
        }
    }

    void DrawOrigin(Rect r)
    {
        Handles.color = COrigin;
        var o = ToCanvas(Vector2.zero);
        if (o.y >= 0 && o.y <= r.height) Handles.DrawLine(new Vector3(0, o.y), new Vector3(r.width, o.y));
        if (o.x >= 0 && o.x <= r.width) Handles.DrawLine(new Vector3(o.x, 0), new Vector3(o.x, r.height));
    }

    void DrawRect169()
    {
        // Scene座標: X: -SCENE_W/2 〜 SCENE_W/2, Y: -SCENE_H/2 〜 SCENE_H/2
        float hw = SCENE_W * .5f, hh = SCENE_H * .5f;
        var TL = ToCanvas(new Vector2(-hw, -hh)); var TR = ToCanvas(new Vector2(hw, -hh));
        var BR = ToCanvas(new Vector2(hw, hh)); var BL = ToCanvas(new Vector2(-hw, hh));

        Handles.DrawSolidRectangleWithOutline(new Vector3[] { TL, TR, BR, BL }, CFill, Color.clear);
        Handles.color = CBorder;
        Handles.DrawLine(TL, TR); Handles.DrawLine(TR, BR); Handles.DrawLine(BR, BL); Handles.DrawLine(BL, TL);

        float cs = Mathf.Clamp(8f * _zoom, 4f, 14f);
        void Corner(Vector2 p, float dx, float dy)
        {
            Handles.color = CCorner;
            Handles.DrawLine(p, p + new Vector2(dx * cs, 0)); Handles.DrawLine(p, p + new Vector2(0, dy * cs));
        }
        Corner(TL, 1, 1); Corner(TR, -1, 1); Corner(BR, -1, -1); Corner(BL, 1, -1);

        float fs = Mathf.Clamp(_zoom * UNIT_PX * .28f, 9f, 22f);
        GUI.Label(new Rect(TL.x + (TR.x - TL.x) * .5f - 40f, TL.y - fs - 4f, 80f, fs + 4f), "16 : 9",
            new GUIStyle(EditorStyles.label)
            {
                fontSize = (int)fs,
                fontStyle = FontStyle.Bold,
                alignment = TextAnchor.LowerCenter,
                normal = { textColor = CBorder }
            });
    }

    void DrawShapes()
    {
        if (_data == null) return;
        foreach (var s in _data.shapes)
        {
            float elapsed = _previewTime - s.startTime;
            bool isWarning = elapsed >= 0 && elapsed < s.warningDuration;
            bool isActive = elapsed >= s.warningDuration && elapsed < s.warningDuration + s.activeDuration;
            bool inRange = elapsed >= 0 && elapsed < s.warningDuration + s.activeDuration;

            // 未到達は薄く常時表示、範囲内は通常表示、終了後は非表示
            if (elapsed >= s.warningDuration + s.activeDuration) continue;

            var cp = ToCanvas(s.position);

            // ドラッグ中はハイライト
            if (s == _rmbDownShape && _isDraggingShape)
                DrawDragHighlight(cp, s);

            AttackShapeRegistry.Draw(s, cp, Scale, isWarning || !isActive);
        }
    }

    void DrawDragHighlight(Vector2 cp, AttackShape s)
    {
        float r = Mathf.Max(s.width, s.height) * 0.5f * Scale + 10f;
        if (s.shapeType == ShapeType.Donut) r = s.outerRadius * Scale + 10f;
        Handles.color = new Color(1f, 1f, 1f, 0.25f);
        Handles.DrawWireDisc(cp, Vector3.forward, r);
    }

    void DrawZoomLabel()
    {
        var vMouse = ToVirtual(Event.current?.mousePosition ?? Vector2.zero);
        GUI.Label(new Rect(6, 4, 300, 18),
            $"zoom:{_zoom:F2}x  scene:({-_offset.x / Scale:F2}, {_offset.y / Scale:F2})u  cursor:({vMouse.x:F2}, {-vMouse.y:F2})u",
            new GUIStyle(EditorStyles.label) { fontSize = 10, normal = { textColor = new Color(.6f, .6f, .6f) } });
    }

    // ==========================================================
    //  上部ツールバー
    // ==========================================================
    void DrawTopToolbar(float ch)
    {
        var tb = new Rect(0, ch, position.width, TOOLBAR_H);
        EditorGUI.DrawRect(tb, new Color(0.2f, 0.2f, 0.2f));
        GUILayout.BeginArea(tb);
        GUILayout.BeginHorizontal();
        GUILayout.Space(6);

        if (GUILayout.Button("Reset View", GUILayout.Width(80), GUILayout.Height(22)))
        { ResetView(new Rect(0, 0, position.width, ch)); Repaint(); }

        GUILayout.Space(6);
        GUILayout.Label("Zoom:", GUILayout.Width(36));
        float nz = GUILayout.HorizontalSlider(_zoom, MIN_ZOOM, MAX_ZOOM, GUILayout.Width(90));
        if (!Mathf.Approximately(nz, _zoom))
        {
            var c = new Vector2(position.width * .5f, ch * .5f);
            var wv = ToVirtual(c); _zoom = nz; _offset = c - wv * Scale; Repaint();
        }
        GUILayout.Label($"{_zoom:F2}x", GUILayout.Width(40));

        GUILayout.Space(8);
        GUILayout.Label("図形:", GUILayout.Width(28));
        foreach (ShapeType st in System.Enum.GetValues(typeof(ShapeType)))
        {
            bool sel = _selectedShapeType == st;
            if (GUILayout.Toggle(sel, st.ToString(), "Button",
                GUILayout.Width(70), GUILayout.Height(22)) && !sel)
                _selectedShapeType = st;
        }

        GUILayout.FlexibleSpace();
        if (GUILayout.Button("Save", GUILayout.Width(54), GUILayout.Height(22))) SaveData();
        GUILayout.Space(6);
        GUILayout.EndHorizontal();
        GUILayout.EndArea();
    }

    // ==========================================================
    //  下部エリア（再生バー + タイムライン）
    // ==========================================================
    void DrawBottomArea(float ch)
    {
        float y = ch + TOOLBAR_H;
        float h = position.height - y;
        if (h <= 0) return;

        var area = new Rect(0, y, position.width, h);
        EditorGUI.DrawRect(area, new Color(0.18f, 0.18f, 0.18f));
        GUILayout.BeginArea(area);

        // ---- 再生コントロール行 ----
        GUILayout.BeginHorizontal();
        GUILayout.Space(6);

        // タイムライン設定
        GUILayout.Label("Lanes:", GUILayout.Width(44));
        _data.timelineCount = Mathf.Clamp(EditorGUILayout.IntField(_data.timelineCount, GUILayout.Width(32)), 1, 16);
        GUILayout.Label("Dur:", GUILayout.Width(28));
        _data.totalDuration = Mathf.Max(1f, EditorGUILayout.FloatField(_data.totalDuration, GUILayout.Width(40)));
        GUILayout.Label("s", GUILayout.Width(12));

        GUILayout.Space(8);

        // 再生ボタン
        GUI.enabled = !_isPlaying;
        if (GUILayout.Button("▶ Play", GUILayout.Width(62), GUILayout.Height(22))) StartPlay();
        GUI.enabled = _isPlaying;
        if (GUILayout.Button("⏸ Pause", GUILayout.Width(68), GUILayout.Height(22))) PausePlay();
        GUI.enabled = true;
        if (GUILayout.Button("⏹ Stop", GUILayout.Width(62), GUILayout.Height(22))) { StopPlay(); Repaint(); }

        GUILayout.Space(6);

        // プレビュースライダー
        float newT = GUILayout.HorizontalSlider(_previewTime, 0f, _data.totalDuration, GUILayout.ExpandWidth(true));
        if (!Mathf.Approximately(newT, _previewTime)) { _previewTime = newT; if (_isPlaying) PausePlay(); }
        GUILayout.Label($"{_previewTime:F2}s", GUILayout.Width(48));
        GUILayout.Space(4);
        GUILayout.EndHorizontal();

        // ---- タイムライン本体 ----
        _timelineScroll = GUILayout.BeginScrollView(_timelineScroll);
        for (int tl = 0; tl < _data.timelineCount; tl++)
            DrawTimelineRow(tl, position.width - 16f);
        GUILayout.EndScrollView();

        GUILayout.EndArea();
    }

    void DrawTimelineRow(int tl, float totalW)
    {
        float rowW = totalW - LABEL_W;
        var rowRect = GUILayoutUtility.GetRect(totalW, TIMELINE_H);
        EditorGUI.DrawRect(rowRect, tl % 2 == 0 ? new Color(.20f, .20f, .20f) : new Color(.22f, .22f, .22f));

        GUI.Label(new Rect(rowRect.x + 2, rowRect.y + 6, LABEL_W - 4, 16), $"Lane {tl}",
            new GUIStyle(EditorStyles.miniLabel) { normal = { textColor = new Color(.8f, .8f, .8f) } });

        var barRect = new Rect(rowRect.x + LABEL_W, rowRect.y + 4, rowW - 4, TIMELINE_H - 8);
        EditorGUI.DrawRect(barRect, new Color(.13f, .13f, .13f));

        foreach (var s in _data.GetShapesOnTimeline(tl))
        {
            float x0 = barRect.x + (s.startTime / _data.totalDuration) * barRect.width;
            float warnW = (s.warningDuration / _data.totalDuration) * barRect.width;
            float actvW = (s.activeDuration / _data.totalDuration) * barRect.width;

            EditorGUI.DrawRect(new Rect(x0, barRect.y, warnW, barRect.height), new Color(1f, 0.85f, 0.1f, 0.55f));
            EditorGUI.DrawRect(new Rect(x0 + warnW, barRect.y, actvW, barRect.height), new Color(1f, 0.25f, 0.25f, 0.75f));
            GUI.Label(new Rect(x0 + 2, barRect.y + 1, warnW + actvW, barRect.height - 2),
                s.shapeType.ToString(),
                new GUIStyle(EditorStyles.miniLabel) { normal = { textColor = Color.white } });
        }

        // 再生ヘッド
        float px = barRect.x + (_previewTime / _data.totalDuration) * barRect.width;
        EditorGUI.DrawRect(new Rect(px - 1, rowRect.y, 2, TIMELINE_H), new Color(0.3f, 1f, 0.3f, 0.9f));
    }

    // ==========================================================
    //  右クリック コンテキストメニュー
    // ==========================================================
    void ShowShapeContextMenu(AttackShape shape)
    {
        var menu = new GenericMenu();
        menu.AddItem(new GUIContent("設定を編集"), false, () => OpenShapeEditor(shape));
        menu.AddItem(new GUIContent("削除"), false, () =>
        {
            Undo.RecordObject(_data, "Delete Shape");
            _data.shapes.Remove(shape);
            SaveData(); Repaint();
        });
        menu.ShowAsContext();
    }

    void OpenShapeEditor(AttackShape shape)
        => ShapeEditorPopup.Open(shape, _data, () => { SaveData(); Repaint(); });
}

// ==========================================================
//  図形設定ポップアップ
// ==========================================================
public class ShapeEditorPopup : EditorWindow
{
    AttackShape _shape;
    AttackTimelineData _data;
    System.Action _onChanged;

    public static void Open(AttackShape shape, AttackTimelineData data, System.Action onChanged)
    {
        var w = CreateInstance<ShapeEditorPopup>();
        w.titleContent = new GUIContent($"Shape: {shape.shapeType}");
        w._shape = shape; w._data = data; w._onChanged = onChanged;
        w.minSize = w.maxSize = new Vector2(300, 360);
        w.ShowUtility();
    }

    void OnGUI()
    {
        if (_shape == null) { Close(); return; }
        EditorGUI.BeginChangeCheck();

        GUILayout.Label("図形設定", EditorStyles.boldLabel);
        _shape.shapeType = (ShapeType)EditorGUILayout.EnumPopup("種類", _shape.shapeType);

        GUILayout.Label("位置 (Scene座標 unit)", EditorStyles.boldLabel);
        // Y軸: Unity=上が+、エディタ内部=下が+ なので表示時に反転
        var display = new Vector2(_shape.position.x, -_shape.position.y);
        var edited = EditorGUILayout.Vector2Field("X / Y", display);
        _shape.position = new Vector2(edited.x, -edited.y);

        _shape.rotation = EditorGUILayout.FloatField("回転 (度)", _shape.rotation);

        GUILayout.Label("タイムライン", EditorStyles.boldLabel);
        _shape.timeline = Mathf.Clamp(EditorGUILayout.IntField("レーン番号", _shape.timeline), 0, _data.timelineCount - 1);
        _shape.startTime = Mathf.Clamp(EditorGUILayout.FloatField("開始時間 (s)", _shape.startTime), 0, _data.totalDuration);
        _shape.warningDuration = Mathf.Max(0, EditorGUILayout.FloatField("予兆時間 (s)", _shape.warningDuration));
        _shape.activeDuration = Mathf.Max(0, EditorGUILayout.FloatField("発動時間 (s)", _shape.activeDuration));

        GUILayout.Label("サイズ (unit)", EditorStyles.boldLabel);
        switch (_shape.shapeType)
        {
            case ShapeType.Circle:
                _shape.width = Mathf.Max(0.01f, EditorGUILayout.FloatField("半径", _shape.width));
                break;
            case ShapeType.Rectangle:
                _shape.width = Mathf.Max(0.01f, EditorGUILayout.FloatField("幅", _shape.width));
                _shape.height = Mathf.Max(0.01f, EditorGUILayout.FloatField("高さ", _shape.height));
                _shape.scaleX = Mathf.Max(0.01f, EditorGUILayout.FloatField("スケールX", _shape.scaleX));
                _shape.scaleY = Mathf.Max(0.01f, EditorGUILayout.FloatField("スケールY", _shape.scaleY));
                break;
            case ShapeType.Donut:
                _shape.outerRadius = Mathf.Max(0.01f, EditorGUILayout.FloatField("外側半径", _shape.outerRadius));
                _shape.innerRadius = Mathf.Clamp(EditorGUILayout.FloatField("内側半径", _shape.innerRadius), 0, _shape.outerRadius - 0.01f);
                break;
        }

        if (EditorGUI.EndChangeCheck()) _onChanged?.Invoke();
        EditorGUILayout.Space(8);
        if (GUILayout.Button("閉じる")) Close();
    }
}

public enum ShapeType { Circle, Rectangle, Donut }

[Serializable]
public class AttackShape
{
    public string id = Guid.NewGuid().ToString();
    public ShapeType shapeType = ShapeType.Circle;

    // Scene座標 (unit) ※ エディタ内部はY反転して保持
    public Vector2 position = Vector2.zero;
    public float rotation = 0f;    // 度（Unity座標系: 反時計回り正）
    public float scaleX = 1f;
    public float scaleY = 1f;

    // タイムライン
    public int timeline = 0;
    public float startTime = 0f;
    public float warningDuration = 1f;
    public float activeDuration = 0.5f;

    // サイズ (unit)
    public float width = 1f;    // Circle: 半径, Rectangle: 幅
    public float height = 1f;    // Rectangle: 高さ
    public float innerRadius = 0.3f;  // Donut
    public float outerRadius = 0.8f;  // Donut
}

[CreateAssetMenu(fileName = "AttackTimelineData", menuName = "AttackEditor/Timeline Data")]
public class AttackTimelineData : ScriptableObject
{
    public int timelineCount = 3;
    public float totalDuration = 10f;
    public List<AttackShape> shapes = new List<AttackShape>();

    public IReadOnlyList<AttackShape> Shapes => shapes;
    public int TimelineCount => timelineCount;
    public float TotalDuration => totalDuration;

    public List<AttackShape> GetShapesOnTimeline(int timeline)
        => shapes.FindAll(s => s.timeline == timeline);

    public List<AttackShape> GetActiveShapesAt(float time)
        => shapes.FindAll(s => time >= s.startTime &&
                               time < s.startTime + s.warningDuration + s.activeDuration);
}

public static class AttackShapeRegistry
{
    // scale はピクセル/unit (UNIT_PX * zoom)
    public delegate void DrawShapeDelegate(AttackShape shape, Vector2 canvasPos, float scale, bool isWarning);

    static readonly Dictionary<ShapeType, DrawShapeDelegate> _drawers = new();

    [InitializeOnLoadMethod]
    static void RegisterAll()
    {
        Register(ShapeType.Circle, DrawCircle);
        Register(ShapeType.Rectangle, DrawRectangle);
        Register(ShapeType.Donut, DrawDonut);
    }

    public static void Register(ShapeType type, DrawShapeDelegate drawer) => _drawers[type] = drawer;

    public static void Draw(AttackShape shape, Vector2 canvasPos, float scale, bool isWarning)
    {
        if (_drawers.TryGetValue(shape.shapeType, out var d)) d(shape, canvasPos, scale, isWarning);
    }

    // ---- 色 ----
    static Color FillColor(bool warn) => warn ? new Color(1f, 0.85f, 0.1f, 0.25f) : new Color(1f, 0.25f, 0.25f, 0.25f);
    static Color BorderColor(bool warn) => warn ? new Color(1f, 0.85f, 0.1f, 0.90f) : new Color(1f, 0.25f, 0.25f, 0.90f);

    // ---- Circle ----
    static void DrawCircle(AttackShape s, Vector2 cp, float scale, bool warn)
    {
        float r = s.width * scale;
        Handles.color = FillColor(warn);
        Handles.DrawSolidDisc(cp, Vector3.forward, r);
        Handles.color = BorderColor(warn);
        Handles.DrawWireDisc(cp, Vector3.forward, r);
    }

    // ---- Rectangle (回転・スケール対応) ----
    static void DrawRectangle(AttackShape s, Vector2 cp, float scale, bool warn)
    {
        float hw = s.width * s.scaleX * 0.5f * scale;
        float hh = s.height * s.scaleY * 0.5f * scale;
        float rad = -s.rotation * Mathf.Deg2Rad; // エディタY反転分を打ち消し
        Vector2 Rot(float x, float y)
        {
            return cp + new Vector2(x * Mathf.Cos(rad) - y * Mathf.Sin(rad),
                                    x * Mathf.Sin(rad) + y * Mathf.Cos(rad));
        }
        var verts = new Vector3[] { Rot(-hw, -hh), Rot(hw, -hh), Rot(hw, hh), Rot(-hw, hh) };
        Handles.DrawSolidRectangleWithOutline(verts, FillColor(warn), BorderColor(warn));
    }

    // ---- Donut ----
    static void DrawDonut(AttackShape s, Vector2 cp, float scale, bool warn)
    {
        float outer = s.outerRadius * scale;
        float inner = s.innerRadius * scale;
        Handles.color = FillColor(warn);
        Handles.DrawSolidDisc(cp, Vector3.forward, outer);
        // 内側を背景色で塗り直してドーナツに見せる
        Handles.color = new Color(0.15f, 0.15f, 0.15f, 1f);
        Handles.DrawSolidDisc(cp, Vector3.forward, inner);
        Handles.color = BorderColor(warn);
        Handles.DrawWireDisc(cp, Vector3.forward, outer);
        Handles.DrawWireDisc(cp, Vector3.forward, inner);
    }
}