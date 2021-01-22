using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    public ModelViewer SelectedItem;
    public TerrainManager terrainManager;

    public void onClick()
    {
        if (SelectedItem.WhereAmI == 1)
        {
            switch (SelectedItem.WhatHasBeenSelected)
            {
                case 1:
                    Debug.Log("This is working");
                    terrainManager.HorizontalTiles = 90;
                    terrainManager.VerticalTiles = 65;
                    terrainManager.key = 1;
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
