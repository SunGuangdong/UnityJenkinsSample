//
//  Unity5.3.4p4
//  MyBuilder.cs
//
//  Created by yasuaki ohama on 2016/06/05.
//  http://yasuaki-ohama.com/
//
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

public class MyBuilder
{
    /// <summary>
    /// 識別名
    /// </summary>
    private const string BUNDLE_IDENTIFIER = "com.Company.Untitled";

    /// <summary>
    /// XcodeのDeviceプロジェクトが生成されるディレクトリ
    /// </summary>
    private const string IOS_DEVICE_LOCATION_PATH_NAME = "Xcode_Device";

    /// <summary>
    /// XcodeのSimulatorプロジェクトが生成されるディレクトリ
    /// </summary>
    private const string IOS_SIMULATOR_LOCATION_PATH_NAME = "Xcode_Simulator";

    /// <summary>
    /// 生成されるアプリケーション名
    /// </summary>
    private const string ANDROID_LOCATION_PATH_NAME = "Android.apk";

    /// <summary>
    /// Builds the project all scene android.
    /// </summary>
    [UnityEditor.MenuItem("Tools/Build Project AllScene Android")]
    public static void BuildProjectAllSceneAndroid()
    {
        EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTarget.Android);

        PlayerSettings.Android.keystorePass = "keystorePass";//キーストアのパスワード
        PlayerSettings.Android.keyaliasName = "keyaliasName";//エイリアス名
        PlayerSettings.Android.keyaliasPass = "keyaliasPass";//エイリアスパスワード

        CommonPlayerSettings ();

        string errorMessage = BuildPipeline.BuildPlayer (GetAllScene(), ANDROID_LOCATION_PATH_NAME, BuildTarget.Android, BuildOptions.None);
        if(string.IsNullOrEmpty(errorMessage))
        {
            Debug.Log("Android Build succeeded");
        } else {
            Debug.Log(errorMessage);
        }
    }

    /// <summary>
    /// Builds the project all scene iOS.
    /// </summary>
    [UnityEditor.MenuItem("Tools/Build Project AllScene iOS")]
    public static void BuildProjectAllSceneiOS()
    {
        EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTarget.iOS);

        BuildOptions opt = BuildOptions.SymlinkLibraries |
                           BuildOptions.AllowDebugging |
                           BuildOptions.ConnectWithProfiler |
                           BuildOptions.Development;

        PlayerSettings.iOS.sdkVersion = iOSSdkVersion.DeviceSDK;

        CommonPlayerSettings();

        string errorMessage = BuildPipeline.BuildPlayer(GetAllScene(), IOS_DEVICE_LOCATION_PATH_NAME, BuildTarget.iOS, opt);
        if(string.IsNullOrEmpty(errorMessage))
        {
            Debug.Log("iOS Device Build succeeded");
        }else{
            Debug.Log(errorMessage);
        }

        /*
        PlayerSettings.iOS.sdkVersion = iOSSdkVersion.SimulatorSDK;
        errorMessage = BuildPipeline.BuildPlayer(GetAllScene(), IOS_SIMULATOR_LOCATION_PATH_NAME, BuildTarget.iOS, opt);
        if(string.IsNullOrEmpty(errorMessage))
        {
            Debug.Log("iOS Simulator Build succeeded");
        }else{
            Debug.Log(errorMessage);
        }
        */
    }

    /// <summary>
    /// Buildを行うシーンファイルを取り出す
    /// </summary>
    /// <returns>The all scene.</returns>
    public static string[] GetAllScene()
    {
        List<string> allScene = new List<string> ();

        foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes) {
            if (scene.enabled) {
                allScene.Add (scene.path);
            }
        }
        return allScene.ToArray ();
    }

    /// <summary>
    /// ios, Androidの共通の設定
    /// </summary>
    public static void CommonPlayerSettings()
    {
        PlayerSettings.bundleIdentifier = BUNDLE_IDENTIFIER;
        PlayerSettings.statusBarHidden = true;
    }
}