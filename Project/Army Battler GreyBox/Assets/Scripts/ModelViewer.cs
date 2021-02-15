using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class ModelViewer : MonoBehaviour
{
    public int WhatHasBeenSelected = 1;
    public int WhereAmI;
    public Dropdown dropdownbox;
    public GameObject[] options;


    List<string> Factions = new List<string>()
    {   "Armies", "The Ardunite Imperium", "The Sainthood of Swith'mas",
        "The High Casts", "The High Elves", "The Dark Elves",
        "The Twin Heads", "Aboracrom's Legions", "The Raven Queens Court",
        "The Flaming Tribes", "The Raqari", "Lizard Folk", "Draconian Imperium", "The Slave Cults" };

    List<string> Scenarios = new List<string>()
    {   "Scenarios", "Scenario 1", "Open Play"};

    public void Start()
    {
        populateList();
    }

    void CloseList()
    {
        foreach(GameObject option in options)
        {
            option.SetActive(false);
        }

    }

    void populateList()
    {

        switch (WhereAmI)
        {

            case 0:
                dropdownbox.AddOptions(Factions);
                break;
            case 1:
                dropdownbox.AddOptions(Scenarios);
                break;
            case 2:

                break;
        }
    }

    public void Dropdown_IndexChanged(int index)
    {

        switch (WhereAmI)
        {
            case 0:

                switch (Factions[index])
                {
                    case "The Ardunite Imperium":
                        CloseList();
                        options[0].SetActive(true);
                        WhatHasBeenSelected = 1;
                        break;
                    case "The Sainthood of Swith'mas":
                        CloseList();
                        options[1].SetActive(true);
                        WhatHasBeenSelected = 2;
                        break;
                    case "The High Casts":
                        CloseList();
                        options[2].SetActive(true);
                        WhatHasBeenSelected = 3;
                        break;
                    case "The High Elves":
                        CloseList();
                        options[3].SetActive(true);
                        WhatHasBeenSelected = 4;
                        break;
                    case "The Dark Elves":
                        CloseList();
                        options[4].SetActive(true);
                        WhatHasBeenSelected = 5;
                        break;
                    case "The Twin Heads":
                        CloseList();
                        options[5].SetActive(true);
                        WhatHasBeenSelected = 6;
                        break;
                    case "Aboracrom's Legions":
                        CloseList();
                        options[6].SetActive(true);
                        WhatHasBeenSelected = 7;
                        break;
                    case "The Raven Queens Court":
                        CloseList();
                        options[7].SetActive(true);
                        WhatHasBeenSelected = 8;
                        break;
                    case "The Flaming Tribes":
                        CloseList();
                        options[9].SetActive(true);
                        WhatHasBeenSelected = 9;
                        break;
                    case "The Raqari":
                        CloseList();
                        options[8].SetActive(true);
                        WhatHasBeenSelected = 10;
                        break;
                    case "Lizard Folk":
                        CloseList();
                        options[10].SetActive(true);
                        WhatHasBeenSelected = 11;
                        break;
                    case "Draconian Imperium":
                        CloseList();
                        options[11].SetActive(true);
                        WhatHasBeenSelected = 12;
                        break;
                    case "The Slave Cults":
                        CloseList();
                        options[12].SetActive(true);
                        WhatHasBeenSelected = 13;
                        break;
                    default:
                        CloseList();
                        break;
                }

                break;

            case 1:

                switch (Scenarios[index])
                {
                    case "Scenario 1":
                        CloseList();
                        options[0].SetActive(true);
                        WhatHasBeenSelected = 1;
                        break;
                    case "Open Play":
                        CloseList();
                        options[1].SetActive(true);
                        WhatHasBeenSelected = 2;
                        break;
                    default:
                        CloseList();
                        break;
                }
                break;
        }
    }
}
