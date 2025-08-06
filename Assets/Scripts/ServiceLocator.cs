using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServiceLocator : MonoBehaviour
{
    public static Inventory inventory;
    public static ResourceCache resourceCache;
    public static CardCache cardCache;
    public static BugCache bugCache;
    public static CraftingTable craftingTable;
}
