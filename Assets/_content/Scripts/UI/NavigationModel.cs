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
        public List<OrientationVariant> orientationVariants;
    }

    [System.Serializable]
    protected class OrientationVariant
    {
        public PageOrientation pageOrientation;
        public GameObject pagePrefab;
    }

    [SerializeField]
    protected List<PagePrefab> pagePrefabs = default;


    public Stack<GameObject> NavigationStack;

    public override void Initialize(Arguments arguments = default)
    {
        NavigationStack = new Stack<GameObject>();
    }

    protected PagePrefab GetPagePrefab(PageName pageName) =>
        pagePrefabs
        .First(page => page.pageName == pageName);

    protected OrientationVariant GetOrientationVariant(PageName pageName, PageOrientation pageOrientation) =>
        GetPagePrefab(pageName)
        .orientationVariants
        .First(orientationVariant => orientationVariant.pageOrientation == pageOrientation);

    public GameObject GetPage(PageName pageName, PageOrientation pageOrientation) =>
        GetOrientationVariant(pageName, pageOrientation)
        .pagePrefab;


}
