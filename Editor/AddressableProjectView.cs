using UnityEditor;
using UnityEngine;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Settings;
using UnityEditor.AddressableAssets.Settings.GroupSchemas;

[InitializeOnLoad]
public static class AddressableProjectView
{
    private static Setting _setting;
    
    
    
    static AddressableProjectView()
    {
        EditorApplication.projectWindowItemOnGUI += OnGUIProjectView;
        _setting = LoadColorSetting();
    }

    internal static Setting LoadColorSetting()
    {
        if (EditorApplication.isCompiling)
        {
            return null;
        }
        
        var guids = AssetDatabase.FindAssets ("t:ColorSetting");
        if (guids.Length == 0)
        {
            Debug.LogError("[AddressableProjectView]ColorSetting does not found.");
            return null;
        }

        var path = AssetDatabase.GUIDToAssetPath(guids[0]);
        return AssetDatabase.LoadAssetAtPath<Setting>(path);
    }

    private static void OnGUIProjectView(string guid, Rect rect)
    {
        var assetEntry = AddressableAssetSettingsDefaultObject.Settings.FindAssetEntry(guid, true);
        if (assetEntry == null)
        {
            return;
        }
        
        var groupSchema = assetEntry.parentGroup.GetSchema<BundledAssetGroupSchema>();
        if (groupSchema == null)
        {
            return;
        }

        EditorGUI.DrawRect(rect, GetDrawColor(groupSchema));
    }

    private static Color GetDrawColor(BundledAssetGroupSchema groupSchema)
    {
        if (_setting == null || AddressableAssetSettingsDefaultObject.Settings == null)
        {
            return Color.clear;
        }
        
        Color color;

        var a = AddressableAssetSettingsDefaultObject.Settings.profileSettings.GetAllProfileNames();

        var pathName = groupSchema.LoadPath.GetName(AddressableAssetSettingsDefaultObject.Settings);
        if (!groupSchema.IncludeInBuild)
        {
            color = _setting.ExcludeBuildColor;
        }
        else if( pathName == _setting.RemoteLoadPathName)
        {
            color = _setting.RemoteLoadColor;
        }
        else if (pathName == _setting.LocalLoadPathName)
        {
            color = _setting.LocalLoadColor;
        }
        else
        {
            color = _setting.OtherRegisteredColor;
        }

        // projectWindowItemOnGUIのタイミングの関係上アイコン・ラベルの上に描画する為、透過する
        color.a *= 0.3f;
        return color;
    }
}
