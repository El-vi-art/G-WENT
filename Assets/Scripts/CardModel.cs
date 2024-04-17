using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardModel : MonoBehaviour
{
    public List<string> Names = new List<string>(){"Maria Silvia","Comandante Marcial","General Resoplez","Coronel Cetaceo","Pepito Corneta","Oliverio","Media Cara","Cortico","Palmiche","Partelo Jabao","Canon de Cuero","Velita"};
    public List<string> Tipos = new List<string>(){"unidad","clima","aumento","despeje","senuelo"};
    public List<string> Campos = new List<string>(){"M","R","S"};// M es cuerpo a cuerpo, R es ataque a distancia y S es asedio
    public List<string> Metals = new List<string>(){"gold","silver","none"};
    public List<string> Acciones = new List<string>(){"Multiplica x3 el poder del campo en que se ponga","Multiplica x2 el poder del campo en que se ponga","Disminuye la mitad de los puntos del campo que se ponga, propio y enemigo","Sustituye una carta en cualquier campo y la regresa a la mano","Congela un campo, propio y enemigo, anulando sus puntos","Reduce a la mitad los puntos de un campo, propio y enemigo","Elimina el invierno","Elimina el verano"};
    
}
