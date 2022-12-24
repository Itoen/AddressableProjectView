using UnityEngine;

//[CreateAssetMenu(menuName = "AddressableProjectView/Create ColorSetting", fileName = "ColorSetting")]
sealed class ColorSetting : ScriptableObject
{
    internal Color RemoteLoadColor = Color.blue;

    internal Color LocalLoadColor = Color.green;

    internal Color ExcludeBuildColor = Color.red;
    
    internal Color OtherRegisteredColor = Color.clear;
}
