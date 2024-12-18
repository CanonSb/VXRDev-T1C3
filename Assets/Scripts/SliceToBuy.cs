using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using TMPro;
using UnityEngine;

public class SliceToBuy : MonoBehaviour
{   
    public GameObject gameController;

    public int itemCost;
    public TMP_Text priceText;
    public PlayerCoins playerCoins;
    private int coinBalance;

    public GameObject tableItem;
    public GameObject itemToSpawn;

    private PlayerHealth hpController;

    public Shop shop;



    // Item name
    private string itemName;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize coinBalance
        coinBalance = 0;

        priceText.text = string.Format("{0}", itemCost);

        if (gameController == null) gameController = GameObject.FindWithTag("GameController");
        if (gameController != null) playerCoins = gameController.GetComponent<PlayerCoins>();

        hpController = GameObject.FindWithTag("GameController")?.GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerCoins != null) coinBalance = playerCoins.GetCoinBalance();

        if (coinBalance >= itemCost) gameObject.layer = LayerMask.NameToLayer("Sliceable");
        else gameObject.layer = LayerMask.NameToLayer("NotSliceable");
    }

    void OnDestroy()
    {
        playerCoins.SpendCoins(itemCost);

        if (itemToSpawn == null) 
        {
            hpController.restoreHealth(5);
            if (shop == null) shop = GameObject.FindGameObjectWithTag("Shop").GetComponent<Shop>();
            if (shop != null) shop.StartCoroutine(shop.SpawnPriceTag());
        }
        else
        {
            tableItem.SetActive(false);
            itemToSpawn.SetActive(true);
            itemToSpawn.transform.SetParent(null);
        }

    }
}

