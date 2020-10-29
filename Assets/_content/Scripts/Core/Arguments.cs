using System.Collections.Generic;
using System;
using Enums;
using UnityEngine;

public static partial class ArgumentKey
{
    public const string MODEL = "MODEL";
    public const string VIEW = "VIEW";
    public const string CONTROLLER = "CONTROLLER";
    public const string STATE_MACHINE = "STATE_MACHINE";
}


public class Arguments
{
    protected Dictionary<string, object> arguments;

    public Arguments()
    {
        arguments = new Dictionary<string, object>();
    }

    public static Arguments Create() => new Arguments();

    public Arguments Put(string key, object value)
    {
        arguments.Add(key, value);
        return this;
    }

    //public object Get(string key)
    //{
    //    if (arguments.ContainsKey(key))
    //        return arguments[key];
    //    return null;
    //}

    public T Get<T>(string key)
    {
        _ = !arguments.ContainsKey(key) ? throw new Exception($"Arguments does not contain {key}") : true;
        //if (!arguments.ContainsKey(key))
        //    throw new Exception($"Arguments does not contain {key}");
        return (T)arguments[key];
    }
}
