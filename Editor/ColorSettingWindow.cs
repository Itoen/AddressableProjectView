using UnityEditor;

sealed class ColorSettingWindow : EditorWindow
{
    [MenuItem("Addressable Project View/Color Setting")]
    static void OpenWindow()
    {
        GetWindow<ColorSettingWindow>();
    }
    
    private ColorSetting _colorSetting;

    private void OnEnable()
    {
        _colorSetting = AddressableProjectView.LoadColorSetting();
    }

    private void OnGUI()
    {
        _colorSetting.RemoteLoadColor = EditorGUILayout.ColorField("Remote Load Asset Color", _colorSetting.RemoteLoadColor);
        _colorSetting.LocalLoadColor = EditorGUILayout.ColorField("Local Load Asset Color", _colorSetting.LocalLoadColor);
        _colorSetting.ExcludeBuildColor = EditorGUILayout.ColorField("Disable Include In Build Asset Color", _colorSetting.ExcludeBuildColor);
        _colorSetting.OtherRegisteredColor = EditorGUILayout.ColorField("Other Registered Asset Color", _colorSetting.OtherRegisteredColor);
    }
}
