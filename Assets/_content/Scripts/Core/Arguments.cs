using System.Collections.Generic;
using System;
using Enums;

namespace Enums
{
    public enum ArgumentKey
    {
        Model, View, Controller, InstanceID, Direction, Speed, Text, Collider2D, AirplaneBotController,
        StartPosition, TargetPosition, Vector3List, StateMachine,
        TeamNumber, MachineGunInfo, MotorInfo, AirplaneInfo, WorldAreaInfo, Vector3Int, Damage
    }
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

    public Arguments Put(ArgumentKey key, object value)
    {
        arguments.Add(key.ToString(), value);
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

    public T Get<T>(ArgumentKey key)
    {
        string keyStr = key.ToString();
        _ = !arguments.ContainsKey(keyStr) ? throw new Exception($"Arguments does not contain {key}") : true;
        //if (!arguments.ContainsKey(key))
        //    throw new Exception($"Arguments does not contain {key}");
        return (T)arguments[keyStr];
    }
}
