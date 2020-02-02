using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

public class Content : MonoBehaviour
{
    public UITreeNode content;

    public void Start()
    {
        content = new UITreeNode(gameObject);
    }

    


}
[System.Serializable]
public class UITreeNode
{
    public GameObject data;
    public List<UITreeNode> children;

    public UITreeNode(GameObject data)
    {
        this.data = data;
        children = new List<UITreeNode>();
        foreach (Transform children in data.transform)
            AddChild(children.gameObject);
    }

    public void AddChild(GameObject data)
    {
        children.Add(new UITreeNode(data));
    }

    public UITreeNode GetChild(int i)
    {
        foreach (UITreeNode n in children)
            if (--i == 0)
                return n;
        return null;
    }

    //public void Traverse(NTree<T> node, TreeVisitor<T> visitor)
    //{
    //    visitor(node.data);
    //    foreach (NTree<T> kid in node.children)
    //        Traverse(kid, visitor);
    //}

}
