using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public class EssentialList
{
    public List<EssentialCard> essentialItem1;
    public List<EssentialCard> essentialItem2;
}

[Serializable]
public class EssentialCard
{
    public string productName;
    public string productDescription;
    public int productCost;
}
