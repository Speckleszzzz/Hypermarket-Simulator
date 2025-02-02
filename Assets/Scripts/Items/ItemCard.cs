using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public class ItemList 
{
    public List<ItemCard> item1;
    public List<ItemCard> item2;
}

[Serializable]
public class ItemCard 
{
    public string productName;
    public string productDescription;
    public int productCost;
}