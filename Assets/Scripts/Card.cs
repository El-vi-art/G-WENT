using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public string name;
    public int power;
    public string type;
    public string campo;
    public string metal;
    public string accion;
    
    public Card (string name, int power, string type, string campo, string metal, string accion)
    {
    	this.name= name;
    	this.power=power;
    	this.type=type;
    	this.campo=campo;
    	this.metal=metal;
    	this.accion=accion;
    }
}
    