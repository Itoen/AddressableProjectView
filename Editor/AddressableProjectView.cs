using UnityEditor;
using UnityEngine;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Settings;
using UnityEditor.AddressableAssets.Settings.GroupSchemas;

[InitializeOnLoad]
public static class AddressableProjectView
{
    private static ColorSetting _colorSetting;
    
    static AddressableProjectView()
    {
        EditorApplication.projectWindowItemOnGUI += OnGUIProjectView;
        _colorSetting = LoadColorSetting();
    }

    internal static ColorSetting LoadColorSetting()
    {
        var guids = AssetDatabase.FindAssets ("t:ColorSetting");
        if (guids.Length == 0)
        {
            Debug.LogError("[AddressableProjectView]ColorSetting does not found.");
            return null;
        }

        var path = AssetDatabase.GUIDToAssetPath(guids[0]);
        return AssetDatabase.LoadAssetAtPath<ColorSetting>(path);
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
        if (_colorSetting == null || AddressableAssetSettingsDefaultObject.Settings == null)
        {
            return Color.clear;
        }
        
        Color color;

        var pathName = groupSchema.LoadPath.GetName(AddressableAssetSettingsDefaultObject.Settings);
        if (!groupSchema.IncludeInBuild)
        {
            color = _colorSetting.ExcludeBuildColor;
        }
        else if( pathName == AddressableAssetSettings.kRemoteLoadPath)
        {
            color = _colorSetting.RemoteLoadColor;
        }
        else if (pathName == AddressableAssetSettings.kLocalLoadPath)
        {
            color = _colorSetting.LocalLoadColor;
        }
        else
        {
            color = _colorSetting.OtherRegisteredColor;
        }

        // projectWindowItemOnGUIのタイミングの関係上アイコン・ラベルの上に描画する為、透過する
        color.a *= 0.3f;
        return color;
    }
}
