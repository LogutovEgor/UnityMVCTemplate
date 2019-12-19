using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugSavePanel : PanelViewController, ISelfInitialize
{
    public GameObject databaseVersionText, currentVersionText, saveText;
    public Text databaseVersionTextComponent, currentVersionTextComponent, saveTextComponent;

    public InputField inputField;

    public override void Initialize()
    {
        base.Initialize();
        //
        databaseVersionTextComponent = databaseVersionText.GetComponent<Text>();
        currentVersionTextComponent = currentVersionText.GetComponent<Text>();
        saveTextComponent = saveText.GetComponent<Text>();
        //
        inputField = GetComponentInChildren<InputField>();
    }


    public void ShowSave()
    {
        saveTextComponent.text = App.AppModel.SaveModel.SaveToString();
        UpdatePanel();
    }

    public void ChangeSaveVer()
    {
        //Debug.Log(inputField.text);
        App.AppModel.SaveModel.currentVersion = int.Parse(inputField.text);
        UpdatePanel();
    }
    public void InitializeSave()
    {
        App.AppModel.SaveModel.Initialize();
        UpdatePanel();
    }
    public void DeleteSave()
    {
        App.AppModel.SaveModel.DeleteDatabase();
        //UpdatePanel();
    }


    public void UpdatePanel()
    {
        currentVersionTextComponent.text = "Cur ver: " + App.AppModel.SaveModel.currentVersion;
        databaseVersionTextComponent.text = "DB ver: " + App.AppModel.SaveModel.GetDatabaseVersion();
        saveTextComponent.text = App.AppModel.SaveModel.SaveToString();
    }
}
