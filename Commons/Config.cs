﻿using UnityEngine;
using System.Collections;

namespace Popup.Configs
{
    public static class Config
    {
        public const int minLevel           = 1;
        public const int maxLevel           = 10;
        public const int stringSize         = 64;
        public const int slotWeightCapacity = 2;
        public const int slotVolumeCapacity = 1;
        public const int squadSize          = 4;
        public const int squadInventorySize = 16;
        public const int warehouseSize      = 128;
        public const int showSize           = 32;
        public const int extraPoolSize      = 5;
    }

    public static class Path
    {
        public const string manager         = "Prefabs/Global/Manager";
    }

    public static class OName
    {
        public const string canvas          = "Canvas";
    }
}