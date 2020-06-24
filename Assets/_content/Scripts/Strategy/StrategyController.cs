using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class StrategyController: Controller
{
    [SerializeField]
    protected List<StrategyController> subStrategies;
    [SerializeField]
    protected bool complete;
    [SerializeField]
    public bool updating; 

    public override void Initialize(Arguments arguments = null)
    {
        subStrategies = new List<StrategyController>();
        complete = false;
        //
        if (UnityEngine.Application.platform == RuntimePlatform.LinuxEditor ||
            UnityEngine.Application.platform == RuntimePlatform.OSXEditor ||
            UnityEngine.Application.platform == RuntimePlatform.WindowsEditor)
            StartCoroutine(DisplayStrategyState())
                ;
        //
        updating = false;
    }

    public void UpdateStrategy()
    {
        if (complete)
            return;
        if (!updating)
            return;
        UpdateInternal();
        if (UncompletedSubStrategiesCount() > 0)
        {
            StrategyController first = GetFirstUncompletedSubStrategy();
            if (!first.updating)
                first.updating = true;
            first.UpdateStrategy();
            if (first.complete)
            {
                first.updating = false;
                //subStrategies.RemoveAt(0);
            }
        }
    }

    protected int UncompletedSubStrategiesCount()
    {
        int sum = 0;
        for (int i = 0; i < subStrategies.Count; i++)
            if (!subStrategies[i].Complete)
                sum++;
        return sum;
    }

    protected StrategyController GetFirstUncompletedSubStrategy()
    {
        for (int i = 0; i < subStrategies.Count; i++)
            if (!subStrategies[i].Complete)
                return subStrategies[i];
        throw new System.Exception($"{nameof(subStrategies)} do not contains any uncomplete sub strategy.");
    }

    public void CompleteStrategy()
    {
        this.complete = true;
        this.updating = false;

        CompleteSubStrategies();
    }

    public void CompleteSubStrategies()
    {
        for (int i = 0; i < subStrategies.Count; i++)
            if (!subStrategies[i].Complete)
                subStrategies[i].CompleteStrategy();
    }


    protected abstract void UpdateInternal();

    protected IEnumerator DisplayStrategyState()
    {
        gameObject.name = $"{this.GetType().Name} \\";
        do
        {
            yield return new WaitForSeconds(0.2f);
            if(!updating)
                gameObject.name = $"{this.GetType().Name} □";
            else if(updating && gameObject.name == $"{this.GetType().Name} □")
                gameObject.name = $"{this.GetType().Name} \\";
            else if (gameObject.name == $"{this.GetType().Name} \\")
                gameObject.name = $"{this.GetType().Name} /";
            else if (gameObject.name == $"{this.GetType().Name} /")
                gameObject.name = $"{this.GetType().Name} -";
            else if (gameObject.name == $"{this.GetType().Name} -")
                gameObject.name = $"{this.GetType().Name} \\";

        } while (!Complete);
        gameObject.name = $"{this.GetType().Name} ✓";
    }

    public void DestroyStrategy()
    {
        if (UnityEngine.Application.platform == RuntimePlatform.LinuxEditor ||
            UnityEngine.Application.platform == RuntimePlatform.OSXEditor ||
            UnityEngine.Application.platform == RuntimePlatform.WindowsEditor)
            StopCoroutine(DisplayStrategyState())
                ;

        DestroySubStrategies();
        //
        Destroy(this.gameObject);
    }

    protected void DestroySubStrategies()
    {
        for (int i = 0; i < subStrategies.Count; i++)
            if (subStrategies[i] != null)
            {
                StrategyController temp = subStrategies[i];
                subStrategies.Remove(temp);

                temp.DestroyStrategy();
                i = 0;
            }
        
    }

    protected bool SubStrategiesComplete
    {
        get
        {
            if (subStrategies.Count == 0)
                return true;
            for (int i = 0; i < subStrategies.Count; i++)
                if (!subStrategies[i].Complete)
                    return false;
            return true;
        }
    }

    public bool Complete => complete && SubStrategiesComplete;
}
