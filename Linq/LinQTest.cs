using System.Linq;
using System.Text;
using System.Collections.Generic;
using UnityEngine;

public class LinQTest : MonoBehaviour {
    
    //Ultima página: 18

	// Use this for initialization
	void Start ()
    {
        
	}

    //Linq tiene 2 sintaxis
    //Expresion Lambda:
    // ---->  var LongWords = words.where ( w => w.lenght > 10);
    //Expresion Querry Sintax (comprensiva)
    // ---->  var LongWords = from w in words where w.lenght > 10;


    private void QuerryBasicsExample()
    {
        string[] Words = { "Lawea", "Linq", "Davinci", "SistemasOTL", "kcyo123", "DeDust" };

        //Obtengo solo las palabras mas cortas y las printeo
        var ShortWords = from word in Words where word.Length <= 5 select word;

        //Printeo cada palabra
        foreach (var word in ShortWords)
            print(word);
    }

    private void QuerryExpresionsTutorial1()
    {
        //1. Especifica la fuente de la información.
        int[] Scores = { 10, 2, 25, 3, 4, 6, 10, 8, 3 };
        //2. Define la expresion Querry.
        IEnumerable<int> ScoreQuerry = from score in Scores where score <= 10 select score;

        foreach (int i in ScoreQuerry)
            print(i + "  ");
    }

    /*
     * Filtering Operators:
     * Restringe los resultados a todos aquellos que cumplan una condición:
     * ---> where, OfType
     * Join Operators: 
     * ---> Join, GroupJoin
     *      Join: Une dos secuencias Basándose en las claves o elementos que coincidan.
     *      GroupJoin: Une dos secuencias y agrupa los los elementos que coincidan.
    */
    
}
