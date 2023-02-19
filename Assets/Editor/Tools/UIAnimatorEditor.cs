using System;
using Tools;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Editor.Tools
{
    [CustomEditor(typeof(Transition)), CanEditMultipleObjects]
    public class UIAnimatorEditor : UnityEditor.Editor
    {
        private VisualElement _inspector;
        private EnumField _onStartAnimMode, _onExitAnimMode;
        private VisualElement _onStartFadePanel, _onStartScalePanel, _onStartMovePanel;
        private VisualElement _onExitFadePanel, _onExitScalePanel, _onExitMovePanel;

        private Button _onStartRecordStartPositionButton,
            _onStartRecordEndPositionButton,
            _onExitRecordStartPositionButton,
            _onExitRecordEndPositionButton;

        private Vector3Field _onStartVector3Start, _onStartVector3End, _onExitVector3Start, _onExitVector3End;

        private SerializedProperty _entryMode, _exitMode;
        private GameObject _gameObject;

        public override VisualElement CreateInspectorGUI()
        {
            var targetScript = target as Transition;
            _gameObject = targetScript!.gameObject;

            var visualTree =
                AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/UIDocs/UIAnimatorInspector.uxml");
            _inspector = new VisualElement();
            visualTree.CloneTree(_inspector);

            _onStartAnimMode = _inspector.Q<EnumField>("EA-Mode");
            _onStartAnimMode.RegisterValueChangedCallback(OnStartAnimModeChanged);

            _onExitAnimMode = _inspector.Q<EnumField>("ExA-Mode");
            _onExitAnimMode.RegisterValueChangedCallback(OnExitAnimModeChanged);

            _onStartFadePanel = _inspector.Q<VisualElement>("EA-Fade");
            _onStartScalePanel = _inspector.Q<VisualElement>("EA-Scale");
            _onStartMovePanel = _inspector.Q<VisualElement>("EA-Move");

            _onExitFadePanel = _inspector.Q<VisualElement>("ExA-Fade");
            _onExitScalePanel = _inspector.Q<VisualElement>("ExA-Scale");
            _onExitMovePanel = _inspector.Q<VisualElement>("ExA-Move");

            _onStartVector3Start = _inspector.Q<Vector3Field>("EA-StartPosition");
            _onStartVector3End = _inspector.Q<Vector3Field>("EA-EndPosition");
            _onExitVector3Start = _inspector.Q<Vector3Field>("ExA-StartPosition");
            _onExitVector3End = _inspector.Q<Vector3Field>("ExA-EndPosition");

            _onStartRecordStartPositionButton = _inspector.Q<Button>("EA-RecordStartPosition");
            _onStartRecordEndPositionButton = _inspector.Q<Button>("EA-RecordEndPosition");
            _onExitRecordStartPositionButton = _inspector.Q<Button>("ExA-RecordStartPosition");
            _onExitRecordEndPositionButton = _inspector.Q<Button>("ExA-RecordEndPosition");

            _onStartRecordStartPositionButton.RegisterCallback<MouseUpEvent>(OnStartRecordStartPosition);
            _onStartRecordEndPositionButton.RegisterCallback<MouseUpEvent>(OnStartRecordEndPosition);
            _onExitRecordStartPositionButton.RegisterCallback<MouseUpEvent>(OnExitRecordStartPosition);
            _onExitRecordEndPositionButton.RegisterCallback<MouseUpEvent>(OnExitRecordEndPosition);

            _entryMode = serializedObject.FindProperty("entryAnimation.mode");
            _exitMode = serializedObject.FindProperty("exitAnimation.mode");

            if (_entryMode.enumValueIndex == 0) // If Entry mode is set to Fade (0).
            {
                _onStartFadePanel.style.display = DisplayStyle.Flex;
                _onStartScalePanel.style.display = DisplayStyle.None;
                _onStartMovePanel.style.display = DisplayStyle.None;
            }
            else if (_entryMode.enumValueIndex == 1) // If Entry mode is set to Scale (1).
            {
                _onStartFadePanel.style.display = DisplayStyle.None;
                _onStartScalePanel.style.display = DisplayStyle.Flex;
                _onStartMovePanel.style.display = DisplayStyle.None;
            }
            else if (_entryMode.enumValueIndex == 2) // If Entry mode is set to Move (2).
            {
                _onStartFadePanel.style.display = DisplayStyle.None;
                _onStartScalePanel.style.display = DisplayStyle.None;
                _onStartMovePanel.style.display = DisplayStyle.Flex;
            }

            if (_exitMode.enumValueIndex == 0) // If Exit mode is set to Fade (0).
            {
                _onExitFadePanel.style.display = DisplayStyle.Flex;
                _onExitScalePanel.style.display = DisplayStyle.None;
                _onExitMovePanel.style.display = DisplayStyle.None;
            }
            else if (_exitMode.enumValueIndex == 1) // If Entry mode is set to Scale (1).
            {
                _onExitFadePanel.style.display = DisplayStyle.None;
                _onExitScalePanel.style.display = DisplayStyle.Flex;
                _onExitMovePanel.style.display = DisplayStyle.None;
            }
            else if (_exitMode.enumValueIndex == 2) // If Entry mode is set to Move (2).
            {
                _onExitFadePanel.style.display = DisplayStyle.None;
                _onExitScalePanel.style.display = DisplayStyle.None;
                _onExitMovePanel.style.display = DisplayStyle.Flex;
            }

            return _inspector;
        }

        private void OnStartRecordStartPosition(MouseUpEvent evt)
        {
            var rect = _gameObject.GetComponent<RectTransform>();
            if (rect != null)
            {
                _onStartVector3Start.value = rect.anchoredPosition;
            }
            else
            {
                _onStartVector3Start.value = _gameObject.transform.localPosition;
            }
        }

        private void OnStartRecordEndPosition(MouseUpEvent evt)
        {
            var rect = _gameObject.GetComponent<RectTransform>();
            if (rect != null)
            {
                _onStartVector3End.value = rect.anchoredPosition;
            }
            else
            {
                _onStartVector3End.value = _gameObject.transform.localPosition;
            }
        }

        private void OnExitRecordStartPosition(MouseUpEvent evt)
        {
            var rect = _gameObject.GetComponent<RectTransform>();
            if (rect != null)
            {
                _onExitVector3Start.value = rect.anchoredPosition;
            }
            else
            {
                _onExitVector3Start.value = _gameObject.transform.localPosition;
            }
        }

        private void OnExitRecordEndPosition(MouseUpEvent evt)
        {
            var rect = _gameObject.GetComponent<RectTransform>();
            if (rect != null)
            {
                _onExitVector3End.value = rect.anchoredPosition;
            }
            else
            {
                _onExitVector3End.value = _gameObject.transform.localPosition;
            }
        }


        private void OnStartAnimModeChanged(ChangeEvent<Enum> evt)
        {
            switch (evt.newValue.ToString())
            {
                case "Fade":
                    _onStartFadePanel.style.display = DisplayStyle.Flex;
                    _onStartScalePanel.style.display = DisplayStyle.None;
                    _onStartMovePanel.style.display = DisplayStyle.None;
                    break;

                case "Scale":
                    _onStartFadePanel.style.display = DisplayStyle.None;
                    _onStartScalePanel.style.display = DisplayStyle.Flex;
                    _onStartMovePanel.style.display = DisplayStyle.None;
                    break;

                case "Move":
                    _onStartFadePanel.style.display = DisplayStyle.None;
                    _onStartScalePanel.style.display = DisplayStyle.None;
                    _onStartMovePanel.style.display = DisplayStyle.Flex;
                    break;
            }
        }

        private void OnExitAnimModeChanged(ChangeEvent<Enum> evt)
        {
            switch (evt.newValue.ToString())
            {
                case "Fade":
                    _onExitFadePanel.style.display = DisplayStyle.Flex;
                    _onExitScalePanel.style.display = DisplayStyle.None;
                    _onExitMovePanel.style.display = DisplayStyle.None;
                    break;

                case "Scale":
                    _onExitFadePanel.style.display = DisplayStyle.None;
                    _onExitScalePanel.style.display = DisplayStyle.Flex;
                    _onExitMovePanel.style.display = DisplayStyle.None;
                    break;

                case "Move":
                    _onExitFadePanel.style.display = DisplayStyle.None;
                    _onExitScalePanel.style.display = DisplayStyle.None;
                    _onExitMovePanel.style.display = DisplayStyle.Flex;
                    break;
            }
        }
    }
}
