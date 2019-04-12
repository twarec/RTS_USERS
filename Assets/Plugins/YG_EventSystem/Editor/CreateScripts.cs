using System.IO;
using UnityEditor.ProjectWindowCallback;
using UnityEditor;
using UnityEngine;

public class CreateInputShablon : EndNameEditAction
{
    [MenuItem("Assets/Create/YG_Scripts/InputShablon", false, -100)]
    public static void CreateInputShablonI()
    {
        ProjectWindowUtil.StartNameEditingIfProjectWindowExists(0,
            CreateInstance<CreateInputShablon>(),
            "InputScripts",
            null,
            null);
    }

    public override void Action(int instanceId, string pathName, string resourceFile)
    {
        AssetDatabase.Refresh();
        int id = 0;
        pathName += ".cs";
        string rePathName = pathName;
        int index = pathName.LastIndexOf('.');

        while (File.Exists(rePathName))
        {
            rePathName = pathName.Insert(index, id == 0 ? "" : "_" + id.ToString());
            id++;
        }
        string name = rePathName.Split('/')[rePathName.Split('/').Length - 1].Split('.')[0];

        File.WriteAllText(rePathName, File.ReadAllText("Assets/Plugins/YG_EventSystem/Scripts/Shablons/InputShablon.cs").Replace("InputShablon", name));
        AssetDatabase.Refresh();
    }
}
public class CreateStandartShablon : EndNameEditAction
{
    [MenuItem("Assets/Create/YG_Scripts/StandartShablon", false, -100)]
    public static void CreateStandartShablonI()
    {
        ProjectWindowUtil.StartNameEditingIfProjectWindowExists(0,
            CreateInstance<CreateStandartShablon>(),
            "UpdateScript",
            null,
            null);
    }

    public override void Action(int instanceId, string pathName, string resourceFile)
    {
        AssetDatabase.Refresh();
        int id = 0;
        pathName += ".cs";
        string rePathName = pathName;
        int index = pathName.LastIndexOf('.');

        while (File.Exists(rePathName))
        {
            rePathName = pathName.Insert(index, id == 0 ? "" : "_" + id.ToString());
            id++;
        }
        string name = rePathName.Split('/')[rePathName.Split('/').Length - 1].Split('.')[0];

        File.WriteAllText(rePathName, File.ReadAllText("Assets/Plugins/YG_EventSystem/Scripts/Shablons/UpdateShablon.cs").Replace("UpdateShablon", name));
        AssetDatabase.Refresh();
    }
}
public class CreateTriggerShablon : EndNameEditAction
{
    [MenuItem("Assets/Create/YG_Scripts/TriggerShablon", false, -100)]
    public static void CreateStandartShablonI()
    {
        ProjectWindowUtil.StartNameEditingIfProjectWindowExists(0,
            CreateInstance<CreateTriggerShablon>(),
            "TriggerScript",
            null,
            null);
    }

    public override void Action(int instanceId, string pathName, string resourceFile)
    {
        AssetDatabase.Refresh();
        int id = 0;
        pathName += ".cs";
        string rePathName = pathName;
        int index = pathName.LastIndexOf('.');

        while (File.Exists(rePathName))
        {
            rePathName = pathName.Insert(index, id == 0 ? "" : "_" + id.ToString());
            id++;
        }
        string name = rePathName.Split('/')[rePathName.Split('/').Length - 1].Split('.')[0];

        File.WriteAllText(rePathName, File.ReadAllText("Assets/Plugins/YG_EventSystem/Scripts/Shablons/TriggerShablon.cs").Replace("TriggerShablon", name));
        AssetDatabase.Refresh();
    }
}


