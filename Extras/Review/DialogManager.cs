using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour {

    public Text NameText;
    public Text dialogeText;

    public Animator DialogObject; //Acá hay que tirar el objeto que tiene la animación (se toma su animator automaticamente).

    public Queue<string> sentences; //Queue Es una collección de objetos que siempre saca afuera (queue.dequeue) el primer elemento que mandamos!

    // Use this for initialization
    void Start() {
        sentences = new Queue<string>();
    }
    public void StartDialoge(Dialoge IncomingDialoge)
    {
        DialogObject.SetBool("DialogActive", true); // Activo  el panel de Dialogo!
        Playerbrain.IcanMove = false; //Le digo al PJ que no se mueva mientas inicia un dialogo.
        NameText.text = IncomingDialoge.Name; // Le pasa el nombre del NPC al texto que contiene el "Nombre de NPC"
        
        sentences.Clear(); // Limpio el contenido de la queue antes de "reasignar" los nuevos valores.

        foreach (string sentece in IncomingDialoge.sentences)
        {
            sentences.Enqueue(sentece); // Recorro "IncomingDialoge" y añado cada párrafo a al queue.
        }
        DisplayNextSentece(); //
    }

    public void DisplayNextSentece()
    {
        if (sentences.Count == 0)
        {
            EndDialoge();
            return;
        }
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(typeSentece(sentence));
    }
    IEnumerator typeSentece (string sentece)
    {
        dialogeText.text = "";

        foreach (char letter in sentece.ToCharArray()) //Transformamos sentence en un array de caracteres!
        {
            dialogeText.text += letter; //Sumamos letra x letra.
            yield return null; //Le añadimos un delay visible entre letra y letra. Yield actua como un retrasador.
        }
    }
    public void EndDialoge()
    {
        DialogObject.SetBool("DialogActive", false); //Desactivo El panel de dialogo!
        Playerbrain.IcanMove = true; // Le digo al PJ que ya se puede mover!
    }
}