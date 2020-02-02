using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
using System.Linq;

public class NavigationModel : Model
{
    [System.Serializable]
    protected class PagePrefab
    {
        public PageName pageName;
        public GameObject pagePrefab;
        public List<ContentPrefab> contentPrefabs;
    }

    [System.Serializable]
    protected class ContentPrefab
    {
        public PageOrientation pageOrientation;
        public GameObject contentPrefab;
    }

    [SerializeField]
    protected List<PagePrefab> pagePrefabs = default;


    public Stack<GameObject> NavigationStack;

    public override void Initialize()
    {
        NavigationStack = new Stack<GameObject>();
    }

    protected PagePrefab GetPagePrefab(PageName pageName) =>
        pagePrefabs
        .First(page => page.pageName == pageName);

    protected ContentPrefab GetContentPrefab(PageName pageName, PageOrientation pageOrientation) =>
        GetPagePrefab(pageName)
        .contentPrefabs
        .First(content => content.pageOrientation == pageOrientation);

    public GameObject GetPage(PageName pageName) =>
        GetPagePrefab(pageName)
        .pagePrefab;

    public GameObject GetContent(PageName pageName, PageOrientation pageOrientation) =>
        GetContentPrefab(pageName, pageOrientation)
        .contentPrefab;
    

}
