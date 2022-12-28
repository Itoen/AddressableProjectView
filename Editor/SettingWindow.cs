using UnityEditor;

sealed class SettingWindow : EditorWindow
{
    [MenuItem("Addressable Project View/Setting")]
    static void OpenWindow()
    {
        GetWindow<SettingWindow>();
    }
    
    private Setting _setting;

    private void OnEnable()
    {
        _setting = AddressableProjectView.LoadColorSetting();
    }

    private void OnGUI()
    {
        EditorGUI.BeginChangeCheck();
        
        EditorGUILayout.LabelField("Color");
        
        _setting.RemoteLoadColor = EditorGUILayout.ColorField("Remote Load Asset Color", _setting.RemoteLoadColor);
        _setting.LocalLoadColor = EditorGUILayout.ColorField("Local Load Asset Color", _setting.LocalLoadColor);
        _setting.ExcludeBuildColor = EditorGUILayout.ColorField("Disable Include In Build Asset Color", _setting.ExcludeBuildColor);
        _setting.OtherRegisteredColor = EditorGUILayout.ColorField("Other Registered Asset Color", _setting.OtherRegisteredColor);
        
        EditorGUILayout.LabelField("PathName");
        _setting.RemoteLoadPathName = EditorGUILayout.TextField("Remote Load Path Name", _setting.RemoteLoadPathName);
        _setting.LocalLoadPathName = EditorGUILayout.TextField("Local Load Path Name", _setting.LocalLoadPathName);

        if (EditorGUI.EndChangeCheck())
        {
            EditorUtility.SetDirty(_setting);
        }
    }
}
