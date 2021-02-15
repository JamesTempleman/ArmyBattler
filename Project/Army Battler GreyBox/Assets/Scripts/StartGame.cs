using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadMap : MonoBehaviour
{
    public ModelViewer SelectedItem;
    public TerrainManager terrainManager;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (SelectedItem.WhereAmI == 1)
            {

                switch (SelectedItem.WhatHasBeenSelected)
                {
                    case 1:
                        terrainManager.HorizontalTiles = 45;
                        terrainManager.VerticalTiles = 40;
                        terrainManager.key = 10;
                        terrainManager.ruinsChance = 0;
                        break;
                }

            }
            else
            {
                Debug.Log(SelectedItem.WhereAmI);
                Debug.Log(SelectedItem.WhatHasBeenSelected);
            }
        }
    }
}
