﻿using UnityEngine;

public class AddOn : MonoBehaviour
{
    public ItemBehaviour Item;
    public enum ItemClass
    {
        Switch = 0,
        Antenna = 1,
        Garbage = 2,
    }

    public ItemClass Class;
}