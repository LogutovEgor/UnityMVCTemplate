using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LiteDB;
using System.IO;
using System.Linq;
using System.Text;

public class SaveModel : Model
{
    [SerializeField]
    public int currentVersion;
    protected LiteDatabase database;
    
    protected enum AttributeName
    {
        //Info
        Version,
        //Save
        Coins, 
        Name,
        Param1,
        Param2
    }

    public override void Initialize()
    {
        if (!File.Exists(GetDatabasePath()))
            CreateDatabase(currentVersion);

        database = new LiteDatabase(GetDatabasePath());

        int databaseVersion = GetDatabaseVersion();

        if (databaseVersion != currentVersion)
            ConvertDatabase(currentVersion);
    }

    protected string GetDatabasePath()
    {
        if (UnityEngine.Application.platform == RuntimePlatform.WindowsEditor)
            return UnityEngine.Application.dataPath + "/Save.db";
        else
            return UnityEngine.Application.persistentDataPath + "/Save.db";
    }

    protected BsonDocument GetInfo() =>
        database.GetCollection("Info").FindAll().First();

    public int GetDatabaseVersion() =>
        GetInfo()["Version"];

    protected BsonDocument GetSave() =>
        database.GetCollection("Save").FindAll().First();

    public void CreateDatabase(int version)
    {
        switch(version)
        {
            case 1:
                CreateDatabaseVer1();
                break;
            case 2:
                CreateDatabaseVer2();
                break;
            case 3:
                CreateDatabaseVer3();
                break;
        }
    }

    protected void CreateDatabaseVer1()
    {
        using (LiteDatabase database = new LiteDatabase(GetDatabasePath()))
        {
            BsonDocument info = new BsonDocument
            {
                { "Version", 1 }
            };
            //
            BsonDocument save = new BsonDocument
            {
                { "Coins", 0 },
                { "Name", "<none>" }
            };
            //
            LiteCollection<BsonDocument> collectionInfo = database.GetCollection<BsonDocument>("Info");
            collectionInfo.Insert(info);
            //
            LiteCollection<BsonDocument> collectionSave = database.GetCollection<BsonDocument>("Save");
            collectionSave.Insert(save);
        }
    }

    protected void CreateDatabaseVer2()
    {
        using (LiteDatabase database = new LiteDatabase(GetDatabasePath()))
        {
            BsonDocument info = new BsonDocument
            {
                { "Version", 2 }
            };
            //
            BsonDocument save = new BsonDocument
            {
                { "Coins", 0 },
                { "Name", "<none>" },
                { "Param1", 100 }
            };
            //
            LiteCollection<BsonDocument> collectionInfo = database.GetCollection<BsonDocument>("Info");
            collectionInfo.Insert(info);
            //
            LiteCollection<BsonDocument> collectionSave = database.GetCollection<BsonDocument>("Save");
            collectionSave.Insert(save);
        }
    }

    protected void CreateDatabaseVer3()
    {
        using (LiteDatabase database = new LiteDatabase(GetDatabasePath()))
        {
            BsonDocument info = new BsonDocument
            {
                { "Version", 3 }
            };
            //
            BsonDocument save = new BsonDocument
            {
                { "Coins", 0 },
                { "Name", "<none>" },
                { "Param1", 100 },
                { "Param2", 99.9f }
            };
            //
            LiteCollection<BsonDocument> collectionInfo = database.GetCollection<BsonDocument>("Info");
            collectionInfo.Insert(info);
            //
            LiteCollection<BsonDocument> collectionSave = database.GetCollection<BsonDocument>("Save");
            collectionSave.Insert(save);
        }
    }

    public void DeleteDatabase()
    {
        database.Dispose();
        if (File.Exists(GetDatabasePath()))
            File.Delete(GetDatabasePath());
    }
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
        if (GetDatabaseVersion() == 1 && version == 2)
            ConvertDatabaseFrom1To2();
        else if (GetDatabaseVersion() == 2 && version == 3)
            ConvertDatabaseFrom2To3();
    }

    protected void ConvertDatabaseFrom1To2()
    {
        LiteCollection<BsonDocument> collectionInfo = database.GetCollection<BsonDocument>("Info");
        BsonDocument infoDoc = collectionInfo.FindAll().First();
        infoDoc["Version"] = 2;
        collectionInfo.Update(infoDoc);
        //
        LiteCollection<BsonDocument> collectionSave = database.GetCollection<BsonDocument>("Save");
        BsonDocument doc = collectionSave.FindAll().First();
        doc.Add("Param1", 1.1f);
        collectionSave.Update(doc);
    }

    protected void ConvertDatabaseFrom2To3()
    {
        LiteCollection<BsonDocument> collectionInfo = database.GetCollection<BsonDocument>("Info");
        BsonDocument infoDoc = collectionInfo.FindAll().First();
        infoDoc["Version"] = 3;
        collectionInfo.Update(infoDoc);
        //
        LiteCollection<BsonDocument> collectionSave = database.GetCollection<BsonDocument>("Save");
        BsonDocument doc = collectionSave.FindAll().First();
        doc.Add("Param2", 1.2f);
        collectionSave.Update(doc);
    }

    public void OnApplicationQuit()
    {
        database.Dispose();
    }
}
