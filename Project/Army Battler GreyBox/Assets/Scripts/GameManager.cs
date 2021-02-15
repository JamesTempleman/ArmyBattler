using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Button[] unitButtons;
    public Toggle zoomToggle;
    public int buttonCheck;

    public Camera cam;
    public GameObject playerView;
    public GameObject RangeCircle;
    public GameObject p1UI;
    public GameObject p2UI;
    public GameObject P1Win;
    public GameObject p2Win;

    public Unit[] p1Units;
    public Unit[] p2Units;
    public int team;
    public int currentTurn;
    public int p1Points=0;
    public int p2Points=0;

    public Collider2D[] hitEnemies;
    public Unit[] Combatants;
    public GameObject selectedUnit;
    public int selectedUnitSpeed;
    public Vector3 orignalPosition;
    public Vector3 pos;

    public LayerMask[] unitLayers;

    public KeyCode LeftClick = KeyCode.Mouse0;

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

    public void setButtonCheck(int i)
    {
        buttonCheck = i;
    }

    public void mAttack()
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

        orignalPosition = pos;

        zoomToggle.isOn = false;

        playerView.transform.position = pos;
        RangeCircle.transform.position = pos;
        RangeCircle.gameObject.transform.localScale = new Vector3(8, 8, 0);
    }

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



        Debug.Log(currentTurn);
    }


    // Start is called before the first frame update
    void Start()
    {
        //hitEnemies[0] = null;
        Debug.Log(p1Units[0].Name + " " + p1Units[1].Name + " " + p1Units[2].Name + " " + p1Units[3].Name);
        Debug.Log(p2Units[0].Name + " " + p2Units[1].Name + " " + p2Units[2].Name + " " + p2Units[3].Name);
    }

    // Update is called once per frame
    void Update()
    {

        if (p1Points == 5)
        {
            P1Win.SetActive(true);
        }
        else if (p2Points == 5)
        {
            p2Win.SetActive(false);
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

            var clickPosScreen = Input.mousePosition;
            var clickPosWorld = cam.ScreenToWorldPoint(new Vector3(clickPosScreen.x, clickPosScreen.y, cam.nearClipPlane+1));

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
                    if (Input.GetKeyDown(KeyCode.Mouse0))
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
    }

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

            Debug.Log(Combatants[0].Name);
            Debug.Log(Combatants[1].Name);
            Debug.Log("Combat Started");
            System.Random r = new System.Random();
            int genRand = r.Next(1, 100);
            
            for (int i = 0; i < Combatants[0].actionsAvaliable; i++)
            {
                if (genRand <= Combatants[0].Stats[4])
                {
                    Debug.Log("Hit");

                    if (Combatants[0].Stats[1] + Combatants[0].wStats[0] >= Combatants[1].Stats[2] * 2)
                    {
                        genRand = r.Next(1, 6);
                        if (genRand >= 2)
                        {
                            Combatants[1].Stats[0] -= Combatants[0].wStats[1];
                            Debug.Log(Combatants[1].Name + " takes " + Combatants[0].wStats[1] + " Damage!");
                        }
                        else
                        {
                            Debug.Log("Does no Damage (" + genRand + ")");
                        }
                    }
                    else if (Combatants[0].Stats[1] + Combatants[0].wStats[0] > Combatants[1].Stats[2])
                    {
                        genRand = r.Next(1, 6);
                        if (genRand >= 3)
                        {
                            Combatants[1].Stats[0] -= Combatants[0].wStats[1];
                            Debug.Log(Combatants[1].Name + " takes " + Combatants[0].wStats[1] + " Damage!");
                        }
                        else
                        {
                            Debug.Log("Does no Damage (" + genRand + ")");
                        }
                    }
                    else if (Combatants[0].Stats[1] + Combatants[0].wStats[0] == Combatants[1].Stats[2])
                    {
                        genRand = r.Next(1, 6);
                        if (genRand >= 4)
                        {
                            Combatants[1].Stats[0] -= Combatants[0].wStats[1];
                            Debug.Log(Combatants[1].Name + " takes " + Combatants[0].wStats[1] + " Damage!");
                        }
                        else
                        {
                            Debug.Log("Does no Damage (" + genRand + ")");
                        }
                    }
                    else if (Combatants[0].Stats[1] + Combatants[0].wStats[0] < Combatants[1].Stats[2])
                    {
                        genRand = r.Next(1, 6);
                        if (genRand >= 5)
                        {
                            Combatants[1].Stats[0] -= Combatants[0].wStats[1];
                            Debug.Log(Combatants[1].Name + " takes " + Combatants[0].wStats[1] + " Damage!");
                        }
                        else
                        {
                            Debug.Log("Does no Damage (" + genRand + ")");
                        }
                    }
                    else if (Combatants[0].Stats[1] + Combatants[0].wStats[0] <= Combatants[1].Stats[2] / 2)
                    {
                        genRand = r.Next(1, 6);
                        if (genRand >= 6)
                        {
                            Combatants[1].Stats[0] -= Combatants[0].wStats[1];
                            Debug.Log(Combatants[1].Name + " takes " + Combatants[0].wStats[1] + " Damage!");
                        }
                        else
                        {
                            Debug.Log("Does no Damage (" + genRand + ")");
                        }
                    }


                }
                else
                {
                    Debug.Log("Miss(" + genRand + ")");
                }
            }

            if (Combatants[1].Stats[0] <= 0)
            {
                Destroy(Combatants[1].unitSelector);
                Destroy(Combatants[1].unit);
                Debug.Log(Combatants[1].Name + " was killed");
            }
            else
            {
                Debug.Log(Combatants[1].Name + " survives with " + Combatants[1].Stats[0] + " wounds remaining");
            }


            RangeCircle.gameObject.transform.localScale = new Vector3(0, 0, 0);
            selectedUnit = null;
            selectedUnitSpeed = 0;

        }
        else
        {
            Debug.Log("No ones there u dingus");
        }
    }

}
