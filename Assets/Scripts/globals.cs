using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class globals
{
    /**
     * RACES
     */
    public const string RaceHuman = "Human";
    public const string RaceElf = "Elf";
    public const string RaceDwarf = "Dwarf";
    public const string RaceOrc = "Orc";
    public static List<string> Races = new List<string>()
    {
        RaceHuman, 
        RaceElf, 
        RaceDwarf, 
        RaceOrc
    };
    
    public static IDictionary<string, string> RacesDict = new Dictionary<string, string>()
    {
        {RaceHuman, "Human"},
        {RaceElf, "Elf"},
        {RaceDwarf, "Dwarf"},
        {RaceOrc, "Orc"}
    };
    
    /**
     * CLASSES
     */
    public const string ClassFighter = "Fighter";
    public const string ClassSpecialist = "Specialist";
    public const string ClassTemplar = "Templar";
    public const string ClassMageBlade = "Mageblade";
    public const string ClassSorcerer = "Sorcerer";
    public const string ClassDruid = "Druid";
    public static List<string> Classes = new List<string>()
    {
        ClassFighter, 
        ClassSpecialist, 
        ClassTemplar,
        ClassMageBlade, 
        ClassSorcerer, 
        ClassDruid
    };
    
    /**
     * XP TABLE
     */
    public static List<int> XPPerLevel = new List<int>()
    {
        0, 100, 300, 600, 1000, 1500, 2100, 2800, 3600, 4500,
        5500, 6600, 7800, 9100, 10500, 12000, 13600, 15300, 17100, 19000,
        21000, 22100, 24300, 26600, 29000, 31500, 34100, 36800, 39600, 42500,
        45500, 48600, 51800, 55100, 58500, 62000, 65600, 69300, 73100, 77000,
        81000, 85100, 89300, 93600, 98000, 102500, 107100, 111800, 120000
    };
}
