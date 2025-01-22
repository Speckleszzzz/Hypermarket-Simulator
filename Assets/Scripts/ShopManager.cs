using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using System;
using Unity.VisualScripting;
using Unity.Mathematics;
using NUnit.Framework;

public class ShopManager : MonoBehaviour
{

    public ItemList shopItems;
    public string saveFile;

    public Transform dropTest;
    public GameObject dropTestObject;

    public Transform contentParentForTier1;
    public GameObject ItemTemplateForTier1;

    public Transform contentParentforTier2;
    public GameObject ItemTemplateForTier2;

    public Transform contentParentforTier3;
    public GameObject ItemTemplateForTier3;

    public Transform cartContentParent;
    public GameObject textTemplate;

    [SerializeField] List<string> tier1ProductList;   // stores the available product in Tier 1
    [SerializeField] List<string> productInCart;

    [SerializeField] int item1Count = 0;
    void Start()
    {
        contentParentForTier1.gameObject.SetActive(true);
        contentParentforTier2.gameObject.SetActive(false);
        contentParentforTier3.gameObject.SetActive(false);
        // saveFile = Path.Combine(Application.dataPath, "Resources", "list.json");
        //writeFile();
        readFile();
    }


    public void readFile()
    {
        // Does the file exist?
        if (File.Exists(saveFile))
        {
            // Read the entire file and save its contents.
            string fileContents = File.ReadAllText(saveFile);

            // Deserialize the JSON data 
            //  into a pattern matching the GameData class.
            shopItems = JsonUtility.FromJson<ItemList>(fileContents);

            PopulateItems();
        }
    }

    void PopulateItems()
    {

        for (int i = 0; i < shopItems.item1.Count; i++)
        {
            GameObject temp = Instantiate(ItemTemplateForTier1, contentParentForTier1);
            temp.name = item1Count.ToString();
            item1Count++;
            temp.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = shopItems.item1[i].productName;
            temp.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = shopItems.item1[i].productDescription;
            temp.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = shopItems.item1[i].productCost.ToString();
            temp.SetActive(true);
            tier1ProductList.Add(shopItems.item1[i].productName);
            // Debug.Log("Product added - " + tier1ProductList[i]);
        }
    }

    void PopulateItems1(List<ItemCard> items)
    {
        for (int i = 0; i < items.Count; i++)
        {
            GameObject temp = Instantiate(ItemTemplateForTier1, contentParentForTier1);
            temp.name = item1Count.ToString();
            item1Count++;
            temp.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = shopItems.item1[i].productName;
            temp.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = shopItems.item1[i].productDescription;
            temp.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = shopItems.item1[i].productCost.ToString();
            temp.SetActive(true);
            tier1ProductList.Add(shopItems.item1[i].productName);
            // Debug.Log("Product added - " + tier1ProductList[i]);
        }
    }


    public void BtnClicked(GameObject a)
    {
        int numberOfItems = 0;
        Debug.Log(shopItems.item1[Int32.Parse(a.name)].productName + "Clicked");
        GameObject temp = Instantiate(textTemplate, cartContentParent);
        temp.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = shopItems.item1[Int32.Parse(a.name)].productName;
        temp.transform.GetChild(0).GetComponent<TextMeshProUGUI>().name = shopItems.item1[Int32.Parse(a.name)].productName;   // testing, this changes the component which has the text name to the product name
        temp.name = shopItems.item1[Int32.Parse(a.name)].productName;  // this changes the entire component template name to the product name
        temp.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = (numberOfItems + 1).ToString();
        temp.SetActive(true);
        productInCart.Add(shopItems.item1[Int32.Parse(a.name)].productName);
    }

    public void RemoveItemFromCart(GameObject a)
    {
        Debug.Log(a.name + "Clicked ez");
        string removeItem = a.name;

        Destroy(a);
        productInCart.Remove(removeItem);
    }

    public void Increment(GameObject a)
    {

        TextMeshProUGUI increment = a.transform.GetChild(1).GetComponent<TextMeshProUGUI>();

        int count;
        if (int.TryParse(increment.text, out count))
        {
            increment.text = (count + 1).ToString();
            productInCart.Add(a.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text);
        }
    }

    public void Decrement(GameObject a)
    {
        TextMeshProUGUI increment = a.transform.GetChild(1).GetComponent<TextMeshProUGUI>();

        int count;
        if (int.TryParse(increment.text, out count))
        {
            if(count > 1)
            {
                increment.text = (count - 1).ToString();
                productInCart.Add(a.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text);
            }
            else
            {
                Debug.Log("No items left lol");
                Destroy(a);
            }
        }
    }

    public void writeFile()
    {
        // Serialize the object into JSON and save string.
        string jsonString = JsonUtility.ToJson(shopItems);

        // Write JSON to file.
        File.WriteAllText(saveFile, jsonString);
    }

    public void Tier1ButtonClick()
    {
        contentParentForTier1.gameObject.SetActive(true);
        contentParentforTier2.gameObject.SetActive(false);
        contentParentforTier3.gameObject.SetActive(false);
    }

    public void Tier2ButtonClick()
    {
        contentParentForTier1.gameObject.SetActive(false);
        contentParentforTier2.gameObject.SetActive(true);
        contentParentforTier3.gameObject.SetActive(false);
    }

    public void Tier3ButtonClick()
    {
        contentParentForTier1.gameObject.SetActive(false);
        contentParentforTier2.gameObject.SetActive(false);
        contentParentforTier3.gameObject.SetActive(true);
    }

    public void Testing()
    {
        PopulateItems1(shopItems.item2);
    }
}


