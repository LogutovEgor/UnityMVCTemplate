using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using System.IO;

public class SaveModel : Model
{
    private SaveType save;

    public Enums.SaveType saveType;
    [Header("Cryptography")]
    public bool cryptography;
    public string key;
    public string initializationVector;

    public SaveType Save
    {
        get
        {
            if (save == null)
            {
                save = new SaveType();
                string jsonStr = default;
                switch (saveType)
                {
                    case Enums.SaveType.PlayerPrefs:
                        if (PlayerPrefs.HasKey("Save"))
                        {
                            if (cryptography)
                                jsonStr = DecryptSave(PlayerPrefs.GetString("Save"));
                            else
                                jsonStr = PlayerPrefs.GetString("Save");

                            JsonUtility.FromJsonOverwrite(jsonStr, save);
                        }
                        else
                            Save = save;
                        break;
                    case Enums.SaveType.File:
                        if (File.Exists(GetFilePath()))
                        {
                            StreamReader streamReader = new StreamReader(GetFilePath());
                            if (cryptography)
                                jsonStr = DecryptSave(streamReader.ReadToEnd());
                            else
                                jsonStr = streamReader.ReadToEnd();
                            streamReader.Dispose();
                            streamReader.Close();

                            JsonUtility.FromJsonOverwrite(jsonStr, save);
                        }
                        else
                        {
                            Save = save;
                        }
                        break;
                }
            }
            return save;
        }
        set
        {
            save = value;
            string jsonStr = default;
            switch (saveType)
            {
                case Enums.SaveType.PlayerPrefs:
                    if (cryptography)
                        jsonStr = EncryptSave(JsonUtility.ToJson(Save));
                    else
                        jsonStr = JsonUtility.ToJson(Save);

                    PlayerPrefs.SetString("Save", jsonStr);
                    break;
                case Enums.SaveType.File:
                    if (cryptography)
                        jsonStr = EncryptSave(JsonUtility.ToJson(Save));
                    else
                        jsonStr = JsonUtility.ToJson(Save);

                    StreamWriter streamWriter = new StreamWriter(GetFilePath());
                    streamWriter.Write(jsonStr);
                    streamWriter.Dispose();
                    streamWriter.Close();
                    break;
            }
        }
    }

    public override void Initialize()
    {
        Save.test++;
        Debug.Log(Save.test);
        return;
        //RijndaelManaged
        //        Legal min key size = 128
        //        Legal max key size = 256
        //        Legal min block size = 128
        //        Legal max block size = 256
        // Unicode 1 char - 2 byte
        // iv size byte = block size / 8 byte
        // block size - bits
        //key size  - bits
        //Debug.Log("key byte size: " + System.Text.Encoding.ASCII.GetByteCount(key));
        //Debug.Log("iv byte size: " + System.Text.Encoding.ASCII.GetByteCount(initializationVector));
        string original = JsonUtility.ToJson(Save);

        byte[] iv = System.Text.Encoding.Unicode.GetBytes(initializationVector);
        byte[] keyBytes = System.Text.Encoding.Unicode.GetBytes(key);
        // Create a new instance of the Rijndael
        // class.  This generates a new key and initialization 
        // vector (IV).
        using (Rijndael myRijndael = Rijndael.Create())
        {
            myRijndael.Key = keyBytes;
            myRijndael.IV = iv;
            // Encrypt the string to an array of bytes.
            byte[] encrypted = RijndaelCryptography.EncryptStringToBytes(original, myRijndael.Key, myRijndael.IV);

            // Decrypt the bytes to a string.
            string roundtrip = RijndaelCryptography.DecryptStringFromBytes(encrypted, myRijndael.Key, myRijndael.IV);
            Debug.Log("Original: " + original);
            Debug.Log("roundtrip: " + roundtrip);
            Debug.Log("Encoded str: " + System.Text.Encoding.Unicode.GetString(encrypted));
        }
    }

    private string GetFilePath()
    {
        if ((UnityEngine.Application.platform == RuntimePlatform.WindowsEditor))
            return UnityEngine.Application.dataPath + "/Save.sav";
        else
            return UnityEngine.Application.persistentDataPath + "/Save.sav";
    }
    private string EncryptSave(string decryptedSave)
    {
        string encryptedStr = default;

        using (Rijndael myRijndael = Rijndael.Create())
        {
            myRijndael.Key = System.Text.Encoding.Unicode.GetBytes(key);
            myRijndael.IV = System.Text.Encoding.Unicode.GetBytes(initializationVector); ;
            // Encrypt the string to an array of bytes.
            byte[] encrypted
                = RijndaelCryptography.EncryptStringToBytes(decryptedSave, myRijndael.Key, myRijndael.IV);

            encryptedStr = System.Text.Encoding.Unicode.GetString(encrypted);
        }
        return encryptedStr;
    }
    private string DecryptSave(string encryptedSave)
    {
        string decryptedStr = default;

        using (Rijndael myRijndael = Rijndael.Create())
        {
            myRijndael.Key
                = System.Text.Encoding.Unicode.GetBytes(key);
            myRijndael.IV
                = System.Text.Encoding.Unicode.GetBytes(initializationVector);
            // Encrypt the string to an array of bytes.

            byte[] encrypted = System.Text.Encoding.Unicode.GetBytes(encryptedSave);

            decryptedStr = RijndaelCryptography.DecryptStringFromBytes(encrypted, myRijndael.Key, myRijndael.IV);
        }
        return decryptedStr;
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
