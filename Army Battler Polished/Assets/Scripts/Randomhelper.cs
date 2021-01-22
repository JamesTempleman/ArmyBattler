using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Randomhelper
{

    public static int Range(Vector2 location, int key, float range)
    {
        return Range(location.x, location.y, key, (int)range);
    }

    public static int Range(float x, float y, int key, int range)
    {
        return Range((int)x, (int)y, key, range);
    }
    public static int Range(int x, int y, int key, int range)
    {

        //this is taking key, x and y and then running it through mathmatical equations
        //to give me an appearing random number
        uint hash = (uint)key;
        hash ^= (uint)x;
        hash *= 0x51d7348d;
        hash ^= 0x85dbdda2;
        hash = (hash << 16) ^ (hash >> 16);
        hash *= 0x7588f287;
        hash ^= (uint)y;
        hash *= 0x487a5559;
        hash ^= 0x64887219;
        hash = (hash << 16) ^ (hash >> 16);
        hash *= 0x63288691;
        return (int)(hash % range);
    }

    /* public static float Percent(float x, float y, float key)
     {
         return Percent((int)x, (int)y, key);
     }*/
    public static float Percent(int x, int y, int key)
    {
        return (float)Range(x, y, key, int.MaxValue) / (float)int.MaxValue;
    }
}
