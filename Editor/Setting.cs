using UnityEditor.AddressableAssets.Settings;
using UnityEngine;

//[CreateAssetMenu(menuName = "AddressableProjectView/Create Setting", fileName = "Setting")]
sealed class Setting : ScriptableObject
{
    internal Color RemoteLoadColor = Color.blue;

    internal Color LocalLoadColor = Color.green;

    internal Color ExcludeBuildColor = Color.red;
    
    internal Color OtherRegisteredColor = Color.clear;

    [SerializeField]
    internal string RemoteLoadPathName = AddressableAssetSettings.kRemoteLoadPath;
    
    [SerializeField]
    internal string LocalLoadPathName = AddressableAssetSettings.kLocalLoadPath;
}
