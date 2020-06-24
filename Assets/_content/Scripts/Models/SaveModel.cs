using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using System.Text;
using System;
using JsonDB;
using Enums;

public class SaveModel : Model
{
    [SerializeField]
    protected int currentVersion;

    [SerializeField]
    protected string databaseName = "JsonDatabase";

    private JsonDatabase database;

    protected enum CollectionName
    {
        Info,
        Save,
        //
        //AxeSaves
    }

    protected enum AttributeName
    {
        //Info
        Version,
        //Save
        //Coins,
        //Level,
        //DamagePowerUpCount,
        //FreezePowerUpCount,
        //HatProtectionCount,
        //CurrentAxeId,  
        //Sound,
        //Music,
        //Vibration
        //,NoAds
    }

    public override void Initialize(Arguments arguments = default)
    {
        if (!DatabaseExists(databaseName))
            CreateDatabase(currentVersion);

        database = new JsonDatabase(databaseName);
        int databaseVersion = GetDatabaseVersion();

        if (databaseVersion != currentVersion)
            ConvertDatabase(currentVersion);
    }

    public void OnApplicationQuit()
    {
        RewriteDatabase();
        database.Save();
    }

    public void OnApplicationPause(bool pause)
    {
        if (database != null)
        {
            RewriteDatabase();
            database.Save();
        }
    }

    protected bool DatabaseExists(string name)
        => PlayerPrefs.HasKey(name);

    protected int GetDatabaseVersion()
    {
        return GetInfo().Get(AttributeName.Version.ToString()).AsInt;
    }

    private JsonCollection GetDatabaseCollection(CollectionName name)
    {
        return database.GetCollection(name.ToString());
    }

    private JsonDocument GetInfo()
    {
        JsonCollection infoCollection = GetDatabaseCollection(CollectionName.Info);
        JsonDocument info = infoCollection.documents.First();
        return info;
    }

    private void SetInfo(AttributeName attribute, JsonValue value)
    {
        JsonDocument info = GetInfo();
        info[attribute.ToString()] = value;
        //GetDatabaseCollection(CollectionName.Info).Update(info);
    }

    private JsonDocument GetSave() =>
    GetDatabaseCollection(CollectionName.Save).documents.First();

    private void SetSave(AttributeName attribute, JsonValue value)
    {
        JsonDocument save = GetSave();
        save[attribute.ToString()] = value;
        //GetDatabaseCollection(CollectionName.Save).Update(save);
    }



    public void CreateDatabase(int version)
    {
        switch (version)
        {
            case 1:
                CreateDatabaseVer1();
                break;
                //case 2:
                //    CreateDatabaseVer2();
                //    break;
                //case 3:
                //    CreateDatabaseVer3();
                //    break;
        }
    }

    protected void CreateDatabaseVer1()
    {
        JsonDatabase database = new JsonDatabase(databaseName);

        JsonDocument info = new JsonDocument(database.GenerateUniqueId());
        info.Add(AttributeName.Version.ToString(), 1);
        //
        JsonDocument save = new JsonDocument(database.GenerateUniqueId());
        //save.Add(AttributeName.Coins.ToString(), 0);
        //save.Add(AttributeName.Level.ToString(), 0);
        //save.Add(AttributeName.DamagePowerUpCount.ToString(), 1);
        //save.Add(AttributeName.FreezePowerUpCount.ToString(), 1);
        //save.Add(AttributeName.CurrentAxeId.ToString(), 0);
        //save.Add(AttributeName.HatProtectionCount.ToString(), 1);
        //save.Add(AttributeName.Sound.ToString(), true);
        //save.Add(AttributeName.Music.ToString(), true);
        //save.Add(AttributeName.Vibration.ToString(), true);
        //save.Add(AttributeName.NoAds.ToString(), false);
        //
        //JsonDocument playerStartArea = WorldAreaInfoToJsonDocument(new WorldAreaInfo(Vector3Int.zero, WorldAreaType.PlayerStart));
        //
        //
        JsonCollection collectionInfo = database.GetCollection(CollectionName.Info.ToString());
        collectionInfo.Insert(info);
        //
        JsonCollection collectionSave = database.GetCollection(CollectionName.Save.ToString());
        collectionSave.Insert(save);
        //
        database.Save();
    }


    //protected void CreateDatabaseVer2()
    //{
    //    using (LiteDatabase database = new LiteDatabase(GetDatabasePath()))
    //    {
    //        BsonDocument info = new BsonDocument
    //        {
    //            { "Version", 2 }
    //        };
    //        //
    //        BsonDocument save = new BsonDocument
    //        {
    //            { "Coins", 0 },
    //            { "Name", "<none>" },
    //            { "Param1", 100 }
    //        };
    //        //
    //        LiteCollection<BsonDocument> collectionInfo = database.GetCollection<BsonDocument>("Info");
    //        collectionInfo.Insert(info);
    //        //
    //        LiteCollection<BsonDocument> collectionSave = database.GetCollection<BsonDocument>("Save");
    //        collectionSave.Insert(save);
    //    }
    //}

    //protected void CreateDatabaseVer3()
    //{
    //    using (LiteDatabase database = new LiteDatabase(GetDatabasePath()))
    //    {
    //        BsonDocument info = new BsonDocument
    //        {
    //            { "Version", 3 }
    //        };
    //        //
    //        BsonDocument save = new BsonDocument
    //        {
    //            { "Coins", 0 },
    //            { "Name", "<none>" },
    //            { "Param1", 100 },
    //            { "Param2", 99.9f }
    //        };
    //        //
    //        LiteCollection<BsonDocument> collectionInfo = database.GetCollection<BsonDocument>("Info");
    //        collectionInfo.Insert(info);
    //        //
    //        LiteCollection<BsonDocument> collectionSave = database.GetCollection<BsonDocument>("Save");
    //        collectionSave.Insert(save);
    //    }
    //}
    //public Save GetSave()
    //{
    //    return database.GetCollection<Save>("save").FindAll().First();
    //}

    //public Info GetInfo()
    //{
    //    return database.GetCollection<Info>("info").FindAll().First();
    //}

    public string SaveToString()
    {
        return GetSave().ToString();
    }

    public string InfoToString()
    {
        //StringBuilder stringBuilder = new StringBuilder().Append("info =>").AppendLine();
        //BsonDocument doc = GetInfo();
        //foreach (var keyValue in doc)
        //    stringBuilder.AppendLine($"Key: {keyValue.Key} Value: {keyValue.Value }");
        return GetInfo().ToString();//stringBuilder.ToString();
    }

    public void ConvertDatabase(int version)
    {
        if (GetDatabaseVersion() != version - 1)
            ConvertDatabase(version - 1);
        //
        //if (GetDatabaseVersion() == 1 && version == 2)
        //    ConvertDatabaseFrom1To2();
        //else if (GetDatabaseVersion() == 2 && version == 3)
        //    ConvertDatabaseFrom2To3();
    }

    //protected void ConvertDatabaseFrom1To2()
    //{
    //    LiteCollection<BsonDocument> collectionInfo = database.GetCollection<BsonDocument>("Info");
    //    BsonDocument infoDoc = collectionInfo.FindAll().First();
    //    infoDoc["Version"] = 2;
    //    collectionInfo.Update(infoDoc);
    //    //
    //    LiteCollection<BsonDocument> collectionSave = database.GetCollection<BsonDocument>("Save");
    //    BsonDocument doc = collectionSave.FindAll().First();
    //    doc.Add("Param1", 1.1f);
    //    collectionSave.Update(doc);
    //}

    //protected void ConvertDatabaseFrom2To3()
    //{
    //    LiteCollection<BsonDocument> collectionInfo = database.GetCollection<BsonDocument>("Info");
    //    BsonDocument infoDoc = collectionInfo.FindAll().First();
    //    infoDoc["Version"] = 3;
    //    collectionInfo.Update(infoDoc);
    //    //
    //    LiteCollection<BsonDocument> collectionSave = database.GetCollection<BsonDocument>("Save");
    //    BsonDocument doc = collectionSave.FindAll().First();
    //    doc.Add("Param2", 1.2f);
    //    collectionSave.Update(doc);
    //}

    public void RewriteDatabase()
    {
        ///////
        //JsonCollection jsonCollection = GetDatabaseCollection(CollectionName.WorldAreas);
        //jsonCollection.Clear();
        //foreach (WorldAreaInfo worldAreaInfo in WorldAreas)
        //    jsonCollection.Insert(WorldAreaInfoToJsonDocument(worldAreaInfo));
        ///////
    }

    //private WorldAreaInfo[,] worldAreas;
    //private List<WorldAreaInfo> worldAreas = default;

    //private void ExpandWorldAreas(int diffX, int diffY)
    //{
    //    WorldAreaInfo[,] temp = new WorldAreaInfo[WorldSize.x + diffX, WorldSize.y + diffY];
    //    for(int x = 0; x < WorldSize.x; x++)
    //        for(int y = 0; y )
    //}

    //private WorldAreaInfo[,] WorldAreas
    //{
    //    get
    //    {
    //        if(worldAreas is null)
    //        {
    //            worldAreas = new WorldAreaInfo[WorldSize.x, WorldSize.y];
    //            foreach (JsonDocument doc in GetDatabaseCollection(CollectionName.WorldAreas).documents)
    //            {
    //                WorldAreaInfo worldAreaInfo = JsonDocumentToWorldAreaInfo(doc);
    //                worldAreas[worldAreaInfo.Index.x, worldAreaInfo.Index.y] = worldAreaInfo;
    //            }
    //        }
    //        return worldAreas;
    //    }
    //}

    //public int Coins
    //{
    //    get => GetSave().Get(AttributeName.Coins.ToString()).Value.AsInt;
    //    set => SetSave(AttributeName.Coins, new JsonValue(value));
    //}

    //public int Level
    //{
    //    get => GetSave().Get(AttributeName.Level.ToString()).Value.AsInt;
    //    set => SetSave(AttributeName.Level, new JsonValue(value));
    //}

    //public int DamagePowerUpCount
    //{
    //    get => GetSave().Get(AttributeName.DamagePowerUpCount.ToString()).Value.AsInt;
    //    set => SetSave(AttributeName.DamagePowerUpCount, new JsonValue(value));
    //}

    //public int FreezePowerUpCount
    //{
    //    get => GetSave().Get(AttributeName.FreezePowerUpCount.ToString()).Value.AsInt;
    //    set => SetSave(AttributeName.FreezePowerUpCount,new JsonValue(value));
    //}

    //public int HatProtectionCount
    //{
    //    get => GetSave().Get(AttributeName.HatProtectionCount.ToString()).Value.AsInt;
    //    set => SetSave(AttributeName.HatProtectionCount, new JsonValue(value));
    //}

    //public int CurrentAxeId
    //{
    //    get => GetSave().Get(AttributeName.CurrentAxeId.ToString()).Value.AsInt;
    //    set => SetSave(AttributeName.CurrentAxeId,new JsonValue(value));
    //}

    //public List<AxeSave> AxeSaves { get; set; }

    //public bool Sound
    //{
    //    get => GetSave().Get(AttributeName.Sound.ToString()).Value.AsBool;
    //    set => SetSave(AttributeName.Sound, new JsonValue(value));
    //}

    //public bool Music
    //{
    //    get => GetSave().Get(AttributeName.Music.ToString()).Value.AsBool;
    //    set => SetSave(AttributeName.Music, new JsonValue(value));
    //}

    //public bool Vibration
    //{
    //    get => GetSave().Get(AttributeName.Vibration.ToString()).Value.AsBool;
    //    set => SetSave(AttributeName.Vibration, new JsonValue(value));
    //}

    //public bool NoAds
    //{
    //    get => GetSave().Get(AttributeName.NoAds.ToString()).Value.AsBool;
    //    set => SetSave(AttributeName.NoAds, new JsonValue(value));
    //}
}
