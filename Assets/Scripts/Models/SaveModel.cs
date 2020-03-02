using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LiteDB;
using System.IO;
using System.Linq;
using System.Text;

public class SaveModel : Model
{

    //[SerializeField]
    //protected int currentVersion;
    //protected LiteDatabase database;

    //protected enum CollectionName
    //{
    //    Info,
    //    Save
    //}

    //protected enum AttributeName
    //{
    //    //Info
    //    Version,
    //    //Save
    //    Coins,
    //    Level,
    //    DamagePowerUpCount,
    //    FreezePowerUpCount
    //}

    //public override void Initialize()
    //{
    //    if (!File.Exists(GetDatabasePath()))
    //        CreateDatabase(currentVersion);

    //    database = new LiteDatabase(GetDatabasePath());

    //    int databaseVersion = GetDatabaseVersion();

    //    if (databaseVersion != currentVersion)
    //        ConvertDatabase(currentVersion);
    //}

    //protected string GetDatabasePath()
    //{
    //    if (UnityEngine.Application.platform == RuntimePlatform.WindowsEditor)
    //        return /*UnityEngine.Application.dataPath*/ "E:" + "/Save.db";
    //    else
    //        return UnityEngine.Application.persistentDataPath + "/Save.db";
    //}

    //protected int GetDatabaseVersion() =>
    //    GetInfo()[AttributeName.Version.ToString()];

    //protected LiteCollection<BsonDocument> GetDatabaseCollection(CollectionName name)
    //    => database.GetCollection(name.ToString());

    //protected BsonDocument GetInfo() =>
    //    GetDatabaseCollection(CollectionName.Info).FindAll().First();

    //protected void SetInfo(AttributeName attribute, BsonValue value)
    //{
    //    BsonDocument info = GetInfo();
    //    info[attribute.ToString()] = value;
    //    GetDatabaseCollection(CollectionName.Info).Update(info);
    //}

    //protected BsonDocument GetSave() =>
    //    GetDatabaseCollection(CollectionName.Save).FindAll().First();

    //protected void SetSave(AttributeName attribute, BsonValue value)
    //{
    //    BsonDocument save = GetSave();
    //    save[attribute.ToString()] = value;
    //    GetDatabaseCollection(CollectionName.Save).Update(save);
    //}

    //public void CreateDatabase(int version)
    //{
    //    switch (version)
    //    {
    //        case 1:
    //            CreateDatabaseVer1();
    //            break;
    //            //case 2:
    //            //    CreateDatabaseVer2();
    //            //    break;
    //            //case 3:
    //            //    CreateDatabaseVer3();
    //            //    break;
    //    }
    //}

    //protected void CreateDatabaseVer1()
    //{
    //    using (LiteDatabase database = new LiteDatabase(GetDatabasePath()))
    //    {
    //        BsonDocument info = new BsonDocument
    //        {
    //            { AttributeName.Version.ToString() , 1 }
    //        };
    //        //
    //        BsonDocument save = new BsonDocument
    //        {
    //            { AttributeName.Coins.ToString(), 0 },
    //            { AttributeName.Level.ToString(), 0 },
    //            { AttributeName.DamagePowerUpCount.ToString(), 100 },
    //            { AttributeName.FreezePowerUpCount.ToString(), 100 }
    //        };

    //        //
    //        LiteCollection<BsonDocument> collectionInfo = database.GetCollection<BsonDocument>(CollectionName.Info.ToString());
    //        collectionInfo.Insert(info);
    //        //
    //        LiteCollection<BsonDocument> collectionSave = database.GetCollection<BsonDocument>(CollectionName.Save.ToString());
    //        collectionSave.Insert(save);
    //    }
    //}


    //public void DeleteDatabase()
    //{
    //    database.Dispose();
    //    if (File.Exists(GetDatabasePath()))
    //        File.Delete(GetDatabasePath());
    //}
    ////public Save GetSave()
    ////{
    ////    return database.GetCollection<Save>("save").FindAll().First();
    ////}

    ////public Info GetInfo()
    ////{
    ////    return database.GetCollection<Info>("info").FindAll().First();
    ////}

    //public string SaveToString()
    //{
    //    return GetSave().ToString();
    //}

    //public string InfoToString()
    //{
    //    //StringBuilder stringBuilder = new StringBuilder().Append("info =>").AppendLine();
    //    //BsonDocument doc = GetInfo();
    //    //foreach (var keyValue in doc)
    //    //    stringBuilder.AppendLine($"Key: {keyValue.Key} Value: {keyValue.Value }");
    //    return GetInfo().ToString();//stringBuilder.ToString();
    //}

    //public void ConvertDatabase(int version)
    //{
    //    if (GetDatabaseVersion() != version - 1)
    //        ConvertDatabase(version - 1);
    //    //
    //    //if (GetDatabaseVersion() == 1 && version == 2)
    //    //    ConvertDatabaseFrom1To2();
    //    //else if (GetDatabaseVersion() == 2 && version == 3)
    //    //    ConvertDatabaseFrom2To3();
    //}

    ////protected void ConvertDatabaseFrom1To2()
    ////{
    ////    LiteCollection<BsonDocument> collectionInfo = database.GetCollection<BsonDocument>("Info");
    ////    BsonDocument infoDoc = collectionInfo.FindAll().First();
    ////    infoDoc["Version"] = 2;
    ////    collectionInfo.Update(infoDoc);
    ////    //
    ////    LiteCollection<BsonDocument> collectionSave = database.GetCollection<BsonDocument>("Save");
    ////    BsonDocument doc = collectionSave.FindAll().First();
    ////    doc.Add("Param1", 1.1f);
    ////    collectionSave.Update(doc);
    ////}

    //public void OnApplicationQuit()
    //{
    //    database.Dispose();
    //}
    ///// <summary>

    ///// </summary>
    //public int Coins
    //{
    //    get => GetSave()[AttributeName.Coins.ToString()];
    //    set => SetSave(AttributeName.Coins, value);
    //}

    //public int Level
    //{
    //    get => GetSave()[AttributeName.Level.ToString()];
    //    set => SetSave(AttributeName.Level, value);
    //}

    //public int DamagePowerUpCount
    //{
    //    get => GetSave()[AttributeName.DamagePowerUpCount.ToString()];
    //    set => SetSave(AttributeName.DamagePowerUpCount, value);
    //}

    //public int FreezePowerUpCount
    //{
    //    get => GetSave()[AttributeName.FreezePowerUpCount.ToString()];
    //    set => SetSave(AttributeName.FreezePowerUpCount, value);
    //}

    public override void Initialize()
    {
        //Dictionary<string, object> content = new Dictionary<string, object>();
        JsonDocument temp = new JsonDocument();
        temp.Add("k1", "value1");
        temp.Add("k2", 10);
        temp.Add("k3", true);
        //temp.content.Add(10);
        //temp.content.Add(true);

        string json = JsonUtility.ToJson(temp);
        PlayerPrefs.SetString("content", json);
        
        json = PlayerPrefs.GetString("content");
        temp = JsonUtility.FromJson<JsonDocument>(json);

        string k3 = temp.Get("k3");
    }
}

[System.Serializable]
class JsonDocument
{
    [SerializeField]
    public JsonKeyValuePair[] content;

    public JsonDocument()
    {
        content = new JsonKeyValuePair[0];
    }

    public void Add(string key, string value)
    {
        JsonKeyValuePair[] temp = content;
        content = new JsonKeyValuePair[temp.Length + 1];
        for (int i = 0; i < temp.Length; i++)
            content[i] = temp[i];

        content[content.Length - 1] = new JsonKeyValuePair(key, value);
    }

    public void Add(string key, int value)
        => this.Add(key, value.ToString());

    public void Add(string key, float value)
        => this.Add(key, value.ToString());

    public void Add(string key, bool value)
        => this.Add(key, value.ToString());


    public string Get(string key)
    {
        return content.FirstOrDefault(elem => elem.Key.AsString  == key).Value.AsString;
    }
}

class JsonCollection
{
    [SerializeField]
    private JsonDocument[] documents;

    public JsonCollection()
    {
        documents = new JsonDocument[0];
    }

    public void Insert(JsonDocument document)
    {

    }
}


class JsonDatabase
{


}

[System.Serializable]
class JsonKeyValuePair
{
    [SerializeField]
    JsonKey key;
    [SerializeField]
    JsonValue value;

    private JsonKeyValuePair(string key)
    {
        this.key =  new JsonKey(key);
    }

    public JsonKeyValuePair(string key, string value):this(key)
    {
        this.value = new JsonValue(value);
    }

    public JsonKeyValuePair(string key, int value) : this(key)
    {
        this.value = new JsonValue(value);
    }

    public JsonKeyValuePair(string key, float value) : this(key)
    {
        this.value = new JsonValue(value);
    }

    public JsonKeyValuePair(string key, bool value) : this(key)
    {
        this.value = new JsonValue(value);
    }

    public JsonKey Key => key;

    public JsonValue Value => value;

    public override string ToString()
    {
        return $"<key: {key}; value: {value}>";
    }
}

[System.Serializable]
class JsonKey
{
    [SerializeField]
    private string key;

    public JsonKey(string key) => this.key = key;

    public string AsString => key;

    //public JsonKey(int key) : this(key.ToString()) { }

    //public JsonKey(float key) : this(key.ToString()) { }

}

[System.Serializable]
class JsonValue
{
    [SerializeField]
    private string value;

    public JsonValue(string value) => this.value = value;
    

    public JsonValue(int value) : this(value.ToString()) { }

    public JsonValue(float value) : this(value.ToString()) { }

    public JsonValue(bool value) : this(value.ToString()) { }

    public string AsString => value;

    public int AsInt
    {
        get
        {
            int temp = default;
            if(!int.TryParse(value, out temp))
                throw new System.Exception($"Error convert {value} to int");
            return temp;
        }
    }

    public float AsFloat
    {
        get
        {
            float temp = default;
            if (!float.TryParse(value, out temp))
                throw new System.Exception($"Error convert {value} to float");
            return temp;
        }
    }

    public bool AsBool
    {
        get
        {
            bool temp = default;
            if (!bool.TryParse(value, out temp))
                throw new System.Exception($"Error convert {value} to bool");
            return temp;
        }
    }
}
