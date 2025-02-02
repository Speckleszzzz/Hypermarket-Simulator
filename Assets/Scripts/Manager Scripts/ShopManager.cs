using System.Collections;
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
    public EssentialList essentialItems;
    public string saveFile;
    public string essentialFile;

    public Transform dropPoint;
    public float itemSpacing = 0.5f;
    [SerializeField] List<Vector3> occupiedPositions = new List<Vector3>();

    public Transform contentParentForTier1;
    public GameObject ItemTemplateForTier1;

    public Transform contentParentforTier2;
    public GameObject ItemTemplateForTier2;

    public Transform contentParentforTier3;
    public GameObject ItemTemplateForTier3;

    public Transform cartContentParent;
    public GameObject textTemplate;

    [SerializeField] List<string> tier1ProductList;   // stores the available product in Tier 1
    [SerializeField] List<string> essentialProductList;
    [SerializeField] List<string> productInCart;
    [SerializeField] float productInCartPrice;
    [SerializeField] List<GameObject> itemPrefabs;

    [SerializeField] int item1Count = 0;
    [SerializeField] int essential1Count = 0;
    void Start()
    {
        contentParentForTier1.gameObject.SetActive(true);
        contentParentforTier2.gameObject.SetActive(false);
        contentParentforTier3.gameObject.SetActive(false);
        // saveFile = Path.Combine(Application.dataPath, "Resources", "list.json");
        //writeFile();
        // essentialFile = Path.Combine(Application.dataPath, "Resources", "essential.json");
        readFile();
    }

    // public void writeFile()
    // {
    //     // Serialize the object into JSON and save string.
    //     string jsonString = JsonUtility.ToJson(shopItems);

    //     // Write JSON to file.
    //     File.WriteAllText(saveFile, jsonString);
    // }


    public void readFile()
    {
        // Does the file exist?
        if (File.Exists(saveFile) && File.Exists(essentialFile))
        {
            // Read the entire file and save its contents.
            string fileContents = File.ReadAllText(saveFile);

            // Deserialize the JSON data 
            //  into a pattern matching the GameData class.
            shopItems = JsonUtility.FromJson<ItemList>(fileContents);


            string essentialContents = File.ReadAllText(essentialFile);
            essentialItems = JsonUtility.FromJson<EssentialList>(essentialContents);

            Debug.Log(essentialItems.essentialItem1.Count);

            PopulateItems();
        }
        else
        {
            Debug.Log("Files are missing lol");
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


        for (int i = 0; i < essentialItems.essentialItem1.Count; i++)
        {
            GameObject temp = Instantiate(ItemTemplateForTier2, contentParentforTier2);
            temp.name = essential1Count.ToString();
            essential1Count++;
            temp.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = essentialItems.essentialItem1[i].productName;
            temp.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = essentialItems.essentialItem1[i].productDescription;
            temp.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = essentialItems.essentialItem1[i].productCost.ToString();
            temp.SetActive(true);
            essentialProductList.Add(essentialItems.essentialItem1[i].productName);
        }
    }

    void PopulateItems1(List<ItemCard> items)
    {
        for (int i = 0; i < items.Count; i++)
        {
            GameObject temp = Instantiate(ItemTemplateForTier1, contentParentForTier1);
            temp.name = item1Count.ToString();
            item1Count++;
            temp.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = items[i].productName;
            temp.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = items[i].productDescription;
            temp.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = items[i].productCost.ToString();
            temp.SetActive(true);
            tier1ProductList.Add(items[i].productName);
            // Debug.Log("Product added - " + tier1ProductList[i]);
        }
    }


    public void BtnClicked(GameObject a)
    {
        // int numberOfItems = 0;
        // Debug.Log(shopItems.item1[Int32.Parse(a.name)].productName + "Clicked");
        // GameObject temp = Instantiate(textTemplate, cartContentParent);
        // temp.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = shopItems.item1[Int32.Parse(a.name)].productName;
        // temp.transform.GetChild(0).GetComponent<TextMeshProUGUI>().name = shopItems.item1[Int32.Parse(a.name)].productName;   // testing, this changes the component which has the text name to the product name
        // temp.name = shopItems.item1[Int32.Parse(a.name)].productName;  // this changes the entire component template name to the product name
        // temp.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = (numberOfItems + 1).ToString();
        // temp.SetActive(true);
        // productInCart.Add(shopItems.item1[Int32.Parse(a.name)].productName);

        Debug.Log(a.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text);
        string nameOfTheProduct = a.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text;
        string costOfTheProduct = a.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text.ToString();

        GameObject temp = Instantiate(textTemplate, cartContentParent);
        temp.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = nameOfTheProduct;
        temp.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = costOfTheProduct;
        temp.SetActive(true);
        productInCart.Add(nameOfTheProduct);
        productInCartPrice += int.Parse(costOfTheProduct);
    }

    public void OnPurchaseBtn()
    {
        if (productInCartPrice <= GameManager.instance.shards)
        {
            BoxCollider boxCollider = dropPoint.GetComponent<BoxCollider>();

            if (boxCollider == null)
            {
                Debug.Log("MataFker put a box collider in the drop location");
            }

            else
            {
                Vector3 boxSize = boxCollider.bounds.size;
                Vector3 boxMin = boxCollider.bounds.min;
                Vector3 boxMax = boxCollider.bounds.max;

                occupiedPositions.Clear();


                for (int i = 0; i < productInCart.Count; i++)
                {
                    string productName = productInCart[i];
                    GameObject matchingPrefab = itemPrefabs.Find(prefab => prefab.name == productName);

                    if (matchingPrefab != null)
                    {
                        Debug.Log("Purchased" + matchingPrefab.name);
                        Vector3 spawnPosition = FindValidPosition(boxMin, boxMax, matchingPrefab);

                        if (spawnPosition != Vector3.zero)
                        {
                            GameObject temp = Instantiate(matchingPrefab, spawnPosition, Quaternion.identity);
                            temp.AddComponent<Rigidbody>();
                            
                            if(tier1ProductList.Contains(productName))
                            {
                                temp.tag = "product";
                            }
                            else if (essentialProductList.Contains(productName))
                            {
                                temp.tag = "stall";
                            }


                            occupiedPositions.Add(spawnPosition);
                        }

                    }
                    else
                    {
                        Debug.Log("matching Prefab Missing for " + productName);
                    }
                }

                Debug.Log("got all Items. the amount deducted is : " + productInCartPrice);
                GameManager.instance.shards += -productInCartPrice;
                GameManager.instance.UpdateShards();
                productInCartPrice = 0;
                productInCart.Clear();
            }
        }
        else
        {
            Debug.Log("MataFker you are broke as hell");
        }

    }

    Vector3 FindValidPosition(Vector3 min, Vector3 max, GameObject prefab)
    {
        int maxAtempt = 100;

        for (int i = 0; i < maxAtempt; i++)
        {
            float randomX = UnityEngine.Random.Range(min.x, max.x);
            float randomZ = UnityEngine.Random.Range(min.z, max.z);

            Vector3 spawnPosition = new Vector3(randomX, min.y, randomZ);

            bool positionOccupied = false;

            foreach (Vector3 occupied in occupiedPositions)
            {
                if (Vector3.Distance(spawnPosition, occupied) < itemSpacing)
                {
                    positionOccupied = true;
                    break;
                }
            }

            if (!positionOccupied)
            {
                return spawnPosition;
            }
        }
        return Vector3.zero;  // this mean there is no valid position
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
            if (count > 1)
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


