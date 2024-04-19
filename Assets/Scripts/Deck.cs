using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{		
		public List<string> Names = new List<string>(){"Maria Silvia","Maria Silvia","Maria Silvia",
		"Comandante Marcial","General Resoplez","Coronel Cetaceo","Coronel Cetaceo","Coronel Cetaceo",
		"Pepito Corneta","Pepito Corneta","Oliverio","Oliverio","Oliverio","Media Cara","Media Cara",
		"Cortico","Cortico","Palmiche","Partelo Jabao","Canon de Cuero","Velita","Invierno","Verano",
		"Canchanchara","Limonada"};

		public List<string> Campos = new List<string>() { "M", "R", "S", "Side"};// M es cuerpo a cuerpo, R es ataque a distancia y S es asedio

		public static List<Card> deck1 = new List<Card>();
		public static List<Card> deck2 = new List<Card>();
		public Deck()
		{
		
			for (int i = 0; i < 25; i++)
			{
				int nameinx = Random.Range(0, Names.Count);
				int power = Random.Range(0, 11);
			
				string campo;
				if (Names[nameinx] == "Palmiche"||Names[nameinx] == "Partelo Jabao"||Names[nameinx] == "Canon de Cuero"||Names[nameinx] == "Velita"||Names[nameinx] == "Invierno"||Names[nameinx] == "Verano"||Names[nameinx] == "Canchanchara"||Names[nameinx] == "Limonada")
				{
					campo="Side";
				}
				else
				{
					Campos.Remove("Side");
					int campoinx = Random.Range(0, Campos.Count);
					campo=Campos[campoinx];
				}
				string type = "";
				if (Names[nameinx] == "Maria Silvia" || Names[nameinx] == "Comandante Marcial" || Names[nameinx] == "General Resoplez" || Names[nameinx] == "Coronel Cetaceo" || Names[nameinx] == "Oliverio" || Names[nameinx] == "Media Cara" || Names[nameinx] == "Pepito Corneta" || Names[nameinx] == "Cortico")
				{ type = "Unidad"; }
				if (Names[nameinx] == "Palmiche" || Names[nameinx] == "Canon de Cuero" || Names[nameinx] == "Partelo Jabao")
				{ type = "Aumento"; }
				if (Names[nameinx] == "Velita")
				{ type = "Senuelo"; }
				if (Names[nameinx] == "Invierno" || Names[nameinx] == "Verano")
				{ type = "Clima"; }
				if (Names[nameinx] == "Canchanchara" || Names[nameinx] == "Limonada")
				{ type = "Despeje"; }

				string metal = "";
				if (Names[nameinx] == "Maria Silvia" || Names[nameinx] == "Coronel Cetaceo" || Names[nameinx] == "Oliverio" || Names[nameinx] == "Media Cara" || Names[nameinx] == "Pepito Corneta" || Names[nameinx] == "Cortico")
				{ metal = "Silver"; }
				if (Names[nameinx] == "Comandante Marcial" || Names[nameinx] == "Generaal Resoplez")
				{ metal = "Gold"; }

				string accion = "";
				if (Names[nameinx] == "Palmiche")
				{
					accion = "Multiplica x3 el poder del campo en que se ponga";
				}	
				if (Names[nameinx] == "Canon de Cuero")
				{
					accion = "Multiplica x2 el poder del campo en que se ponga";
				}
				if (Names[nameinx] == "Partelo Jabao")
				{
					accion = "Disminuye la mitad de los puntos del campo que se ponga, propio y enemigo";
				}
				if (Names[nameinx] == "Velita")
				{
					accion = "Sustituye una carta en cualquier campo y la regresa a la mano";
				}
				if (Names[nameinx] == "Invierno")
				{
					accion = "Congela un campo, propio y enemigo, anulando sus puntos";
				}
				if (Names[nameinx] == "Verano")
				{
					accion = "Reduce a la mitad los puntos de un campo, propio y enemigo";
				}
				if (Names[nameinx] == "Canchanchara")
				{
					accion = "Elimina el invierno";
				}
				if (Names[nameinx] == "Limonada")
				{
					accion = "Elimina el verano";
				}

				deck1.Add(new Card(Names[nameinx], power, type, campo, metal, accion));
				deck2.Add(new Card(Names[nameinx], power, type, campo, metal, accion));
				Names.Remove(Names[nameinx]);
				Debug.Log("Carta agregada");
			}
				Debug.Log("el deck tiene"+deck1.Count+"cartas");
		} 	
	
	public static void SacarCartas(int CantidadASacar, List<Card> deck, List<Card> Mano)
	{
		for (int i = 0; i < CantidadASacar; i++)
		{
			int random = Random.Range(0, deck.Count);
			Mano.Add(deck[random]);
			deck.RemoveAt(random);
		}
		Debug.Log(CantidadASacar+" cartas sacadas");
		Debug.Log("le quedan" +deck.Count);
	}
}
public class Card : ScriptableObject
{
	public string name;
	public int power;
	public string type;
	public string campo;
	public string metal;
	public string accion;
	public Sprite Image;

	public Card(string name, int power, string type, string campo, string metal, string accion)
	{
		this.name = name;
		this.power = power;
		this.type = type;
		this.campo = campo;
		this.metal = metal;
		this.accion = accion;
	}
}
