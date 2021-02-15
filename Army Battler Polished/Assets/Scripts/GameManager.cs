using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //static UI elements
    public Button[] unitButtons;
    public Toggle zoomToggle;
    public int buttonCheck = 0;

    //Moving UI elements
    public Camera cam;
    public GameObject playerView;
    public GameObject RangeCircle;
    public GameObject p1UI;
    public GameObject p2UI;
    public GameObject P1Win;
    public GameObject p2Win;
    public GameObject draw;

    //Player Effected members
    public int[] originalStats;
    public Unit[] p1Units;
    public Unit[] p2Units;
    public int team;
    public int currentTurn;
    public int p1TotalUnits;
    public int p2TotalUnits;
    public int p1Points=0;
    public int p2Points=0;
    
    //These change in combat
    public Collider2D[] hitEnemies;
    public Unit[] Combatants;

    //these change in both combat and movement
    public GameObject selectedUnit;
    public int selectedUnitSpeed;
    public Vector3 orignalPosition;

    //these change when moving
    public GameObject[] deploymentZone;
    public Vector3 pos;
    public GameObject statPanel;
    public Text[] statText;
    public Text CombatLog;
    public Text[] PointTrackers;
    public Text woundCounter;

    //The layers the units are on
    public LayerMask[] unitLayers;

    //left click
    private KeyCode LeftClick = KeyCode.Mouse0;

    //movement
    public void move()
    {

        int index = buttonCheck - 1;
            switch (team)
            {
                case 1:
                    pos = p1Units[index].unit.transform.position;
                    RangeCircle.gameObject.transform.localScale = new Vector3(p1Units[index].Stats[8], p1Units[index].Stats[8], 0);
                    selectedUnitSpeed = p1Units[index].Stats[8];
                    selectedUnit = p1Units[index].unit;
                    break;

                case 2:
                    pos = p2Units[index].unit.transform.position;
                    RangeCircle.gameObject.transform.localScale = new Vector3(p2Units[index].Stats[8], p2Units[index].Stats[8], 0);
                    selectedUnitSpeed = p2Units[index].Stats[8];
                    selectedUnit = p2Units[index].unit;
                    break;
            }

            orignalPosition = pos;
            zoomToggle.isOn = false;
            playerView.transform.position = pos;
            RangeCircle.transform.position = pos;

        Debug.Log(orignalPosition);
    }

    //checking what button has been pushed
    public void setButtonCheck(int i)
    {
        buttonCheck = i;
    }

    //melee attack
    public void mAttack()
    {
        if (buttonCheck > 0)
        {

            int index = buttonCheck - 1;

            switch (team)
            {
                case 1:
                    pos = p1Units[index].unit.transform.position;
                    selectedUnit = p1Units[index].unit;
                    Combatants[0] = p1Units[index];
                    break;

                case 2:
                    pos = p2Units[index].unit.transform.position;
                    selectedUnit = p2Units[index].unit;
                    team = 1;
                    Combatants[0] = p2Units[index];
                    break;

            }

            Debug.Log(Combatants[0].Name);
            CombatLog.text +=  Environment.NewLine + Combatants[0].Name + " is attacking";

            orignalPosition = pos;

            zoomToggle.isOn = false;

            playerView.transform.position = pos;
            RangeCircle.transform.position = pos;
            RangeCircle.gameObject.transform.localScale = new Vector3(8, 8, 0);
        }
    }

    //when u end the turn
    public void endTurn()
    {
        currentTurn++;
        buttonCheck = 0;

        foreach (Button button in unitButtons)
        {
            button.interactable = true;
        }

        if (currentTurn % 2 == 0)
        {
            p1UI.SetActive(true);
            p2UI.SetActive(false);
        }
        else
        {
            p1UI.SetActive(false);
            p2UI.SetActive(true);
        }


        CombatLog.text += Environment.NewLine + "Turn " + currentTurn.ToString();
        Debug.Log(currentTurn);
    }
    
    //a pop up to show a units stats
    public void showUnitStats(Unit unit)
    {

        statPanel.SetActive(true);

        int i = 0;
        foreach (Text t in statText)
        {
            if (i == 13)
            {
                statText[i].text = unit.keywords;
            }

            else if (i == 9)
            {
                statText[i].text = unit.wName;
            }
            else if (i == 10)
            { 
                statText[i].text = unit.wType;
            }
            else if (i == 11)
            {
                statText[i].text = "+" + unit.wStats[i-11].ToString();
            }
            else if (i == 12)
            {
                statText[i].text = unit.wStats[i - 11].ToString();
            }
            else
            {
                statText[i].text = unit.Stats[i].ToString();
            }
            i++;
        }
    }

    //when u want to reset and play again
    public void resetMap()
    {
        int i = 0;
        foreach (Unit unit in p1Units)
        {
            p1Units[i].Stats[0] = p1Units[i].totalWounds;
            p1Units[i].unit.transform.localScale = new Vector3(1, 1, 1);
            p1Units[i].unit.transform.position = deploymentZone[i].transform.position;
            p1Units[i].unitSelector.gameObject.SetActive(true);
            i++;
        }
        i = 0;
        foreach (Unit unit in p2Units)
        {
            p2Units[i].Stats[0] = p2Units[i].totalWounds;
            p2Units[i].unit.transform.localScale = new Vector3(1,1,1);
            p2Units[i].unit.transform.position = deploymentZone[i+5].transform.position;
            p2Units[i].unitSelector.gameObject.SetActive(true);
            i++;
        }
        P1Win.SetActive(false);
        p2Win.SetActive(false);
        draw.SetActive(false);
        p1Points = 0;
        p2Points = 0;

        p1TotalUnits = p1Units.Length;
        p2TotalUnits = p2Units.Length;
        CombatLog.text += "Turn " + currentTurn.ToString();
        //hitEnemies[0] = null;
        Debug.Log(p1Units[0].Name + " " + p1Units[1].Name + " " + p1Units[2].Name + " " + p1Units[3].Name);
        Debug.Log(p2Units[0].Name + " " + p2Units[1].Name + " " + p2Units[2].Name + " " + p2Units[3].Name);

    }

        //update
        void Update()
    {

        var clickPosScreen = Input.mousePosition;
        var clickPosWorld = cam.ScreenToWorldPoint(new Vector3(clickPosScreen.x, clickPosScreen.y, cam.nearClipPlane + 1));

        woundCounter.transform.position = Input.mousePosition;

        PointTrackers[0].text = "Player 1 Points :" + p1Points.ToString();
        PointTrackers[1].text = "Player 2 Points :" + p2Points.ToString();
        PointTrackers[2].text = "Player 1 Points :" + p1Points.ToString();
        PointTrackers[3].text = "Player 2 Points :" + p2Points.ToString();


        //if click pos = pos of a unit, display wounds

        Collider2D[] onHover = Physics2D.OverlapCircleAll(clickPosWorld, 0, unitLayers[2]);

        if(onHover.Length >=1)
        {

            woundCounter.gameObject.SetActive(true);

            if (onHover[0] == p2Units[0].unitCollider)
            {
                woundCounter.text = p2Units[0].Stats[0].ToString() + " Wounds remaining";
            }
            else if (onHover[0] == p2Units[1].unitCollider)
            {
                woundCounter.text = p2Units[1].Stats[0].ToString() + " Wounds remaining";
            }
            else if (onHover[0] == p2Units[2].unitCollider)
            {
                woundCounter.text = p2Units[2].Stats[0].ToString() + " Wounds remaining";
            }
            else if (onHover[0] == p2Units[3].unitCollider)
            {
                woundCounter.text = p2Units[3].Stats[0].ToString() + " Wounds remaining";
            }
            else if (onHover[0] == p2Units[4].unitCollider)
            {
                woundCounter.text = p2Units[4].Stats[0].ToString() + " Wounds remaining";
            }
            else if (onHover[0] == p1Units[0].unitCollider)
            {
                woundCounter.text = p1Units[0].Stats[0].ToString() + " Wounds remaining";
            }
            else if (onHover[0] == p1Units[1].unitCollider)
            {
                woundCounter.text = p1Units[1].Stats[0].ToString() + " Wounds remaining";
            }
            else if (onHover[0] == p1Units[2].unitCollider)
            {
                woundCounter.text = p1Units[2].Stats[0].ToString() + " Wounds remaining";
            }
            else if (onHover[0] == p1Units[3].unitCollider)
            {
                woundCounter.text = p1Units[3].Stats[0].ToString() + " Wounds remaining";
            }
            else if (onHover[0] == p1Units[4].unitCollider)
            {
                woundCounter.text = p1Units[4].Stats[0].ToString() + " Wounds remaining";
            }
        }

        else
        {
            woundCounter.gameObject.SetActive(false);
        }
        

        if (currentTurn == 10) {
            if (p1Points > p2Points)
            {
                P1Win.SetActive(true);
            }
            else if (p2Points > p1Points)
            {
                p2Win.SetActive(true);
            }
            else
            {
                draw.SetActive(true);
            }
        }

        else if(p2TotalUnits <= 0)
        {
            P1Win.SetActive(true);
        }
        else if (p1TotalUnits <= 0)
        {
            p2Win.SetActive(true);
        }

        if (currentTurn % 2 == 0)
        {
            team = 1;
        }
        else
        {
            team = 2;
        }


        if (selectedUnit != null)
        {
            if (selectedUnitSpeed != 0)
            {
                if (clickPosWorld.x <= orignalPosition.x + selectedUnitSpeed / 2 &&
                    clickPosWorld.x >= orignalPosition.x - selectedUnitSpeed / 2 &&
                    clickPosWorld.y <= orignalPosition.y + selectedUnitSpeed / 2 &&
                    clickPosWorld.y >= orignalPosition.y - selectedUnitSpeed / 2)
                {
                    selectedUnit.transform.position = clickPosWorld;
                    if (Input.GetKey(LeftClick))
                    {
                        RangeCircle.gameObject.transform.localScale = new Vector3(0, 0, 0);
                        CombatLog.text += Environment.NewLine + "Unit Moved";
                        selectedUnit = null;
                        selectedUnitSpeed = 0;
                    }
                }
            }

            else
            {
                if (clickPosWorld.x <= orignalPosition.x + 4 &&
                    clickPosWorld.x >= orignalPosition.x - 4 &&
                    clickPosWorld.y <= orignalPosition.y + 4 &&
                    clickPosWorld.y >= orignalPosition.y - 4)
                {
                    if (Input.GetKeyDown(LeftClick))
                    {
                        Debug.Log("Mouse is in right place");

                        switch (team)
                        {
                            case 1:
                                Debug.Log(Combatants[0].Name);

                                hitEnemies = Physics2D.OverlapCircleAll(clickPosWorld, 0.5f, unitLayers[1]);
                                Debug.Log("Button Clicked");
                                Combat(hitEnemies);
                                break;
                            case 2:
                                Debug.Log(Combatants[0]);

                                hitEnemies = Physics2D.OverlapCircleAll(clickPosWorld, 0.5f, unitLayers[0]);
                                Debug.Log("Button Clicked");
                                Debug.Log(hitEnemies[0]);
                                Combat(hitEnemies);
                                break;

                        }

                    }
                }
            }
        }
        else
        {
            if (Input.GetKeyDown(LeftClick))
            {
                hitEnemies = Physics2D.OverlapCircleAll(clickPosWorld, 0, unitLayers[2]);

                int i = 0;
                int check = 0;
                while(check == 0)
                {

                    if (hitEnemies.Length>= 1) {

                        if (hitEnemies[0] == p1Units[i].unitCollider)
                        {
                            showUnitStats(p1Units[i]);
                            check = 1;
                        }
                        else if (hitEnemies[0] == p2Units[i].unitCollider)
                        {
                            showUnitStats(p2Units[i]);
                                check = 1;
                        }
                        
                        i += 1;
                    }
                    else
                    {
                        check = 1;
                    }
                }
            }
        }

    }

    //combat math
    void Combat(Collider2D[] Enemies)
    {

        if (Enemies.Length >= 1)
        {
            Debug.Log(Enemies[0]);

            switch (team)
            {
                case 1:

                    if (Enemies[0] == p2Units[0].unitCollider)
                    {
                        Combatants[1] = p2Units[0];
                    }
                    else if (Enemies[0] == p2Units[1].unitCollider)
                    {
                        Combatants[1] = p2Units[1];
                    }
                    else if (Enemies[0] == p2Units[2].unitCollider)
                    {
                        Combatants[1] = p2Units[2];
                    }
                    else if (Enemies[0] == p2Units[3].unitCollider)
                    {
                        Combatants[1] = p2Units[3];
                    }
                    else if (Enemies[0] == p2Units[4].unitCollider)
                    {
                        Combatants[1] = p2Units[4];
                    }

                    break;

                case 2:

                    if (Enemies[0] == p1Units[0].unitCollider)
                    {
                        Combatants[1] = p1Units[0];
                    }
                    else if (Enemies[0] == p1Units[1].unitCollider)
                    {
                        Combatants[1] = p1Units[1];
                    }
                    else if (Enemies[0] == p1Units[2].unitCollider)
                    {
                        Combatants[1] = p1Units[2];
                    }
                    else if (Enemies[0] == p1Units[3].unitCollider)
                    {
                        Combatants[1] = p1Units[3];
                    }
                    else if (Enemies[0] == p1Units[4].unitCollider)
                    {
                        Combatants[1] = p1Units[4];
                    }

                    break;

            }

            CombatLog.text += Environment.NewLine + Combatants[1].Name + " is defending";

            Debug.Log(Combatants[0].Name);
            Debug.Log(Combatants[1].Name);
            Debug.Log("Combat Started");
            System.Random r = new System.Random();
            int genRand = r.Next(1, 100);
            
            for (int i = 0; i < Combatants[0].actionsAvaliable; i++)
            {
                if (genRand <= Combatants[0].Stats[4])
                {
                    CombatLog.text += Environment.NewLine + "Hit";
                    Debug.Log("Hit");

                    genRand = r.Next(1, 6);
                    if (Combatants[0].Stats[1] + Combatants[0].wStats[0] >= Combatants[1].Stats[2] * 2)
                    {
                        if (genRand >= 2)
                        {
                            CombatLog.text += Environment.NewLine + Combatants[0].Name + " Wounds the target" + Environment.NewLine + "Dealing " + Combatants[0].wStats[1] + " Damage!";
                            Combatants[1].Stats[0] -= Combatants[0].wStats[1];
                            Debug.Log(Combatants[1].Name + " takes " + Combatants[0].wStats[1] + " Damage!");
                        }
                        else
                        {
                            CombatLog.text += Environment.NewLine + Combatants[0].Name + " fails to wound the target" + Environment.NewLine + "Dealing no Damage!";
                            Combatants[1].Stats[0] -= Combatants[0].wStats[1];
                            Debug.Log("Does no Damage (" + genRand + ")");
                        }
                    }
                    else if (Combatants[0].Stats[1] + Combatants[0].wStats[0] > Combatants[1].Stats[2])
                    {
                        if (genRand >= 3)
                        {
                            CombatLog.text += Environment.NewLine + Combatants[0].Name + " Wounds the target" + Environment.NewLine + "Dealing " + Combatants[0].wStats[1] + " Damage!";
                            Combatants[1].Stats[0] -= Combatants[0].wStats[1];
                            Debug.Log(Combatants[1].Name + " takes " + Combatants[0].wStats[1] + " Damage!");
                        }
                        else
                        {
                            CombatLog.text += Environment.NewLine + Combatants[0].Name + " fails to wound the target" + Environment.NewLine + "Dealing no Damage!";
                            Debug.Log("Does no Damage (" + genRand + ")");
                        }
                    }
                    else if (Combatants[0].Stats[1] + Combatants[0].wStats[0] == Combatants[1].Stats[2])
                    {
                        if (genRand >= 4)
                        {
                            CombatLog.text += Environment.NewLine + Combatants[0].Name + " Wounds the target" + Environment.NewLine + "Dealing " + Combatants[0].wStats[1] + " Damage!";
                            Combatants[1].Stats[0] -= Combatants[0].wStats[1];
                            Debug.Log(Combatants[1].Name + " takes " + Combatants[0].wStats[1] + " Damage!");
                        }
                        else
                        {
                            CombatLog.text += Environment.NewLine + Combatants[0].Name + " fails to wound the target" + Environment.NewLine + "Dealing no Damage!";
                            Debug.Log("Does no Damage (" + genRand + ")");
                        }
                    }
                    else if (Combatants[0].Stats[1] + Combatants[0].wStats[0] < Combatants[1].Stats[2])
                    {
                        if (genRand >= 5)
                        {
                            CombatLog.text += Environment.NewLine + Combatants[0].Name + " Wounds the target" + Environment.NewLine + "Dealing " + Combatants[0].wStats[1] + " Damage!";
                            Combatants[1].Stats[0] -= Combatants[0].wStats[1];
                            Debug.Log(Combatants[1].Name + " takes " + Combatants[0].wStats[1] + " Damage!");
                        }
                        else
                        {
                            CombatLog.text += Environment.NewLine + Combatants[0].Name + " fails to wound the target" + Environment.NewLine + "Dealing no Damage!";
                            Debug.Log("Does no Damage (" + genRand + ")");
                        }
                    }
                    else if (Combatants[0].Stats[1] + Combatants[0].wStats[0] <= Combatants[1].Stats[2] / 2)
                    {
                        if (genRand >= 6)
                        {
                            CombatLog.text += Environment.NewLine + Combatants[0].Name + " Wounds the target" + Environment.NewLine + "Dealing " + Combatants[0].wStats[1] + " Damage!";
                            Combatants[1].Stats[0] -= Combatants[0].wStats[1];
                            Debug.Log(Combatants[1].Name + " takes " + Combatants[0].wStats[1] + " Damage!");
                        }
                        else
                        {
                            CombatLog.text += Environment.NewLine + Combatants[0].Name + " fails to wound the target" + Environment.NewLine + "Dealing no Damage!";
                            Debug.Log("Does no Damage (" + genRand + ")");
                        }
                    }
              
                }
                else
                {

                    CombatLog.text += Environment.NewLine + Combatants[0].Name + " Misses";
                    Debug.Log("Miss(" + genRand + ")");
                }
            }

            if (Combatants[1].Stats[0] <= 0)
            {
                switch (team)
                {
                    case 1:
                        p1Points++;
                        break;
                    case 2:
                        p2Points++;
                        break;
                }
                CombatLog.text += Environment.NewLine + Combatants[1].Name + " is killed";
                Combatants[1].unitSelector.gameObject.SetActive(false);
                Combatants[1].unit.transform.localScale = new Vector3(0,0,0);
                // impliment a deployment system
                Debug.Log(Combatants[1].Name + " was killed");
            }
            else
            {

                CombatLog.text += Environment.NewLine + Combatants[1].Name + " survives with " + Combatants[1].Stats[0] + " wounds remaining";
                Debug.Log(Combatants[1].Name + " survives with " + Combatants[1].Stats[0] + " wounds remaining");
            }


            RangeCircle.gameObject.transform.localScale = new Vector3(0, 0, 0);
            selectedUnit = null;
            selectedUnitSpeed = 0;

        }
        else
        {
            CombatLog.text += Environment.NewLine + "No ones there u dingus";
            Debug.Log("No ones there u dingus");
        }
    }

}
