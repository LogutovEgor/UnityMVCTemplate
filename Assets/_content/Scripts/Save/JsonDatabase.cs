///Yehor Lohutov 
///Ver 1.1 
///04.06.2020
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace JsonDB
{

    [System.Serializable]
    public class JsonDatabase
    {
        [SerializeField]
        private JsonDatabaseWrapper databaseWrapper;

        [SerializeField]
        private string name;

        public JsonDatabase(string name)
        {
            databaseWrapper = new JsonDatabaseWrapper();
            this.name = name;

            if (PlayerPrefs.HasKey(this.name))
            {
                string json = PlayerPrefs.GetString(this.name);
                databaseWrapper = JsonUtility.FromJson<JsonDatabaseWrapper>(json);
            }
            //else
            //throw new System.Exception($"Error read database {name} from player prefs");

            else
            {
                string json = JsonUtility.ToJson(databaseWrapper);
                PlayerPrefs.SetString(this.name, json);
            }
        }

        public void Save()
        {
            string json = JsonUtility.ToJson(databaseWrapper);
            PlayerPrefs.SetString(this.name, json);
        }

        public JsonCollection GetCollection(string name)
        {
            JsonCollection temp = databaseWrapper.collections.FirstOrDefault(collection => collection.Name == name);
            if (temp == null)
                temp = CreateCollection(name);
            Save();
            return temp;
        }

        protected JsonCollection CreateCollection(string name)
        {
            JsonCollection[] temp = databaseWrapper.collections;
            databaseWrapper.collections = new JsonCollection[temp.Length + 1];
            for (int i = 0; i < temp.Length; i++)
                databaseWrapper.collections[i] = temp[i];

            databaseWrapper.collections[databaseWrapper.collections.Length - 1] = new JsonCollection(name);
            Save();
            return databaseWrapper.collections[databaseWrapper.collections.Length - 1];
        }

        public int GenerateUniqueId()
            => databaseWrapper.lastGeneratedId++;


    }
    public class JsonDatabaseWrapper
    {
        [SerializeField]
        public JsonCollection[] collections;

        [SerializeField]
        public int lastGeneratedId;

        public JsonDatabaseWrapper()
        {
            collections = new JsonCollection[0];
            lastGeneratedId = 0;
        }
    }


    [System.Serializable]
    public class JsonCollection
    {
        [SerializeField]
        private string name;
        [SerializeField]
        public JsonDocument[] documents;

        public JsonCollection(string name)
        {
            this.name = name;
            documents = new JsonDocument[0];
        }

        public void Insert(JsonDocument document)
        {
            if (documents == null)
                documents = new JsonDocument[0];

            JsonDocument[] temp = documents;
            documents = new JsonDocument[temp.Length + 1];
            for (int i = 0; i < temp.Length; i++)
                documents[i] = temp[i];

            documents[documents.Length - 1] = document;
        }

        public void Remove(JsonDocument document)
            => Remove(document.Id);
        

        private void Remove(int documentId)
        {
            List<JsonDocument> temp = new List<JsonDocument>();
            foreach (JsonDocument doc in documents)
                if (doc.Id != documentId)
                    temp.Add(doc);
            documents = temp.ToArray();
        }

        public void Clear()
        {
            documents = new JsonDocument[0];
        }

        public string Name => name;
    }



    [System.Serializable]
    public class JsonDocument
    {
        [SerializeField]
        private int id;
        [SerializeField]
        private JsonKeyValuePair[] content;

        public JsonDocument(int id)
        {
            this.id = id;
            content = new JsonKeyValuePair[0];
        }

        protected void ExpandArray(int num = 1)
        {
            JsonKeyValuePair[] temp = content;
            content = new JsonKeyValuePair[temp.Length + num];
            for (int i = 0; i < temp.Length; i++)
                content[i] = temp[i];
        }

        public void Add(string key, string value)
        {
            ExpandArray();
            content[content.Length - 1] = new JsonKeyValuePair(key, value);
        }

        public void Add(string key, string[] value)
        {
            ExpandArray();
            content[content.Length - 1] = new JsonKeyValuePair(key, value);
        }

        public void Add(string key, int value)
        {
            ExpandArray();
            content[content.Length - 1] = new JsonKeyValuePair(key, value);
        }

        public void Add(string key, int[] value)
        {
            ExpandArray();
            content[content.Length - 1] = new JsonKeyValuePair(key, value);
        }

        public void Add(string key, float value)
        {
            ExpandArray();
            content[content.Length - 1] = new JsonKeyValuePair(key, value);
        }

        public void Add(string key, float[] value)
        {
            ExpandArray();
            content[content.Length - 1] = new JsonKeyValuePair(key, value);
        }

        public void Add(string key, bool value)
        {
            ExpandArray();
            content[content.Length - 1] = new JsonKeyValuePair(key, value);
        }

        public void Add(string key, bool[] value)
        {
            ExpandArray();
            content[content.Length - 1] = new JsonKeyValuePair(key, value);
        }

        public JsonValue this[string key]
        {
            get
            {
                return content.FirstOrDefault(elem => elem.Key.AsString == key).Value;
            }
            set
            {
                content.FirstOrDefault(elem => elem.Key.AsString == key).Value = value;
            }
        }

        public JsonValue Get(string key)
        {
            return content.FirstOrDefault(elem => elem.Key.AsString == key).Value;
        }

        public int Id => id;

        public override string ToString()
        {
            return JsonUtility.ToJson(this);
        }
    }



    [System.Serializable]
    public class JsonKeyValuePair
    {
        [SerializeField]
        JsonKey key;
        [SerializeField]
        JsonValue value;

        private JsonKeyValuePair(string key)
        {
            this.key = new JsonKey(key);
        }

        public JsonKeyValuePair(string key, string value) : this(key)
        {
            this.value = new JsonValue(value);
        }

        public JsonKeyValuePair(string key, string[] value) : this(key)
        {
            this.value = new JsonValue(value);
        }

        public JsonKeyValuePair(string key, int value) : this(key)
        {
            this.value = new JsonValue(value);
        }

        public JsonKeyValuePair(string key, int[] value) : this(key)
        {
            this.value = new JsonValue(value);
        }

        public JsonKeyValuePair(string key, float value) : this(key)
        {
            this.value = new JsonValue(value);
        }

        public JsonKeyValuePair(string key, float[] value) : this(key)
        {
            this.value = new JsonValue(value);
        }

        public JsonKeyValuePair(string key, bool value) : this(key)
        {
            this.value = new JsonValue(value);
        }

        public JsonKeyValuePair(string key, bool[] value) : this(key)
        {
            this.value = new JsonValue(value);
        }

        public JsonKey Key => key;

        public JsonValue Value
        {
            get { return this.value; }
            set { this.value = value; }
        }

        public override string ToString()
        {
            return JsonUtility.ToJson(this);
        }
    }

    [System.Serializable]
    public class JsonKey
    {
        [SerializeField]
        private string key;

        public JsonKey(string key) => this.key = key;

        public string AsString => key;

        //public JsonKey(int key) : this(key.ToString()) { }

        //public JsonKey(float key) : this(key.ToString()) { }

        public override string ToString()
        {
            return JsonUtility.ToJson(this);
        }

    }

    [System.Serializable]
    public class JsonValue
    {
        [SerializeField]
        private string value;

        public JsonValue(string value) => this.value = value;


        public JsonValue(int value) : this(value.ToString()) { }

        public JsonValue(float value) : this(value.ToString()) { }

        public JsonValue(bool value) : this(value.ToString()) { }

        public JsonValue(string[] value)
        {
            JsonArray<string> jsonArray = new JsonArray<string>(value);
            this.value = jsonArray.ToString();
        }

        public JsonValue(float[] value)
        {
            JsonArray<float> jsonArray = new JsonArray<float>(value);
            this.value = jsonArray.ToString();
        }

        public JsonValue(int[] value)
        {
            JsonArray<int> jsonArray = new JsonArray<int>(value);
            this.value = jsonArray.ToString();
        }

        public JsonValue(bool[] value)
        {
            JsonArray<bool> jsonArray = new JsonArray<bool>(value);
            this.value = jsonArray.ToString();
        }

        public string AsString => value;

        public int AsInt
        {
            get
            {
                int temp = default;
                if (!int.TryParse(value, out temp))
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

        //public bool[] AsBoolArray
        //{
        //    get
        //    {
        //        JsonArray<bool> temp = default;
        //        temp = JsonUtility.FromJson<JsonArray<bool>>(value);
        //        if (temp == null)
        //            throw new System.Exception($"Error convert {value} to bool[]");
        //        return temp.array;
        //    }
        //}


        //public string[] AsStringArray
        //{
        //    get
        //    {
        //        JsonArray<string> temp = default;
        //        temp = JsonUtility.FromJson<JsonArray<string>>(value);
        //        if (temp == null)
        //            throw new System.Exception($"Error convert {value} to bool[]");
        //        return temp.array;
        //    }
        //}

        public T[] AsArray<T>()
        {
            JsonArray<T> temp = default;
            temp = JsonUtility.FromJson<JsonArray<T>>(value);
            if (temp == null)
                throw new System.Exception($"Error convert {value} to bool[]");
            return temp.array;
        }

        public override string ToString()
        {
            return JsonUtility.ToJson(this);
        }
    }

    [System.Serializable]
    public class JsonArray<T>
    {
        [SerializeField]
        public T[] array;

        public JsonArray(T[] array)
        {
            this.array = array;
        }

        public override string ToString()
        {
            return JsonUtility.ToJson(this);
        }
    }

}
