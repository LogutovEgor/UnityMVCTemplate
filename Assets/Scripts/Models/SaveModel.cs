using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

public class SaveModel : Model
{
    private SaveType save;

    public SaveType Save
    {
        get
        {
            if (save == null)
                save = new SaveType();
                if (PlayerPrefs.HasKey("Save"))
                    JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString("Save"), save);
                else
                    Save = save;
            return save;
        }
        set
        {
            save = value;
            PlayerPrefs.SetString("Save", JsonUtility.ToJson(save));
        }
    }

    public override void Initialize()
    {
    }

    private void OnApplicationQuit()
    {
        Save = save;
    }
    private void OnApplicationPause(bool pause)
    {
        Save = save;
    }

    [System.Serializable]
    public class SaveType
    {
        public int test;

        public SaveType()
        {
            test = 0;
        }
    }
}
