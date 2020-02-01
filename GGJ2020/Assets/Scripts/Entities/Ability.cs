using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability
{
    public Ability(string name, int parametar)
    {
        this.name = name;
        this.parametar = parametar;
    }
    string name;
    int parametar;
}
