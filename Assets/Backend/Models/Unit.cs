﻿using Unirea_UI.Models;

namespace Assets.Backend.Models
{
    public class Unit
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public int Amount { get; private set; }
        public Town Town { get; private set; }
        public UnitType UnitType { get; private set; }

        public Unit(int id, string name, int amount, Town town, UnitType unitType)
        {
            Id = id;
            Name = name;
            Amount = amount;
            Town = town;
            UnitType = unitType;
        }
    }
}
