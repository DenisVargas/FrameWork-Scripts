using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //Acordate de esto

public class Console : MonoBehaviour {

    public static /*para que acceda al script desde cualquier lado*/ Console instance; 

    //Delegate es un tipo de variable que guarda funciones en ella, de la misma matera en la que string guarda texto.
    public delegate void FunctionPrototype(); //al ser una funcion en este caso entran variables. Aca hay un ejemplo en el que si entran. https://unity3d.com/es/learn/tutorials/topics/scripting/delegates
    public FunctionPrototype functionPrototype;  //Te recomiendo poner el mismo nombre asi no te mareas al nombrar la funcion, pero podes poner lo que sea
    public Dictionary<string, FunctionPrototype> /*nombre del diccionario*/ allCommands = new Dictionary<string, FunctionPrototype>();
    /*Aca usas la variable dictionary que usa dos entradas. La primera (string) es como vas a llamar a la funcion, pueden ser int o float o lo que quieras. 
     * Y la segunda es una funcion para ser invocada. en este caso, se va a ver despues ya que es un tipo delegate que va a contener funciones. https://unity3d.com/es/learn/tutorials/modules/intermediate/scripting/lists-and-dictionaries */

    #region texto y consola
    public Text backgroundText;
    public InputField inputText; //en donde vamos a ingresar el texto luego
    public Scrollbar scrollball; //El scroll de la consola

    public KeyCode keyForOpenCloseConsole; //= KeyCode.Escape; //usa la variable Keycode y la nombra para cambiar cual es la tecla para abrir o cerrar consola. Ahi le asignas el escape como tecla.

    public GameObject consoleContent; //objeto de la consola

    #endregion

    void Start () {
        if (instance == null) //instance es la consola, es lo primero que se declaro. Habla que si la consola es nula, que esto es instance
            instance = this;
        else
            GameObject.Destroy(this); //Si la consola no es nula, destruila.

        functionPrototype = Start; //iguala la funcion a la funcion start

        consoleContent = this.transform.FindChild("Image").gameObject; //Encuentra la imagen de la consola
	}

    void Update() {
        scrollball.value = 0; //Haces que el scroll este en cero siempre. Que siempre se vea lo ultimo que escribsite y que se mueva cada vez que se agrega algo 

        if (Input.GetKeyDown(keyForOpenCloseConsole)) //Para que se abra al bajar la tecla
        {
            consoleContent.SetActive(!consoleContent.activeSelf); //Activa la consola. Activeself se activa a si mismo
        }
        if (consoleContent.activeSelf && Input.GetKeyDown(KeyCode.Return))//Si la consola esta activa y se presiona enter
        {
            string commandName = inputText.text; //introduce el texto que escribiste en una variable

            if (allCommands.ContainsKey(commandName)) //ContainsKey es una funcion del Dictionary para checkear si existe una variable en el mismo. 
                //Ve si en el diccionario allCommands hay algo con el nombre introducido en commandName
            {
                allCommands[inputText.text].Invoke();// En invoke es una funcion de MonoBehaviour que invoca a la funcion para que se ejecute. Ahi dice que la funcion con el nombre input.Text sea invocada. Puede ser usada asi o con entrada https://docs.unity3d.com/ScriptReference/MonoBehaviour.Invoke.html
                //Existe tambien un cancellinvoke, pero eso es mas detalle y no sirve en este caso. No tendria sentido
            }
            else
            {
                WriteInConsole("This command doesn't exist");
            }
        }
	}

    //Registro
    public void RegisterCommand(string name, FunctionPrototype command) //Creas la funcion para crear comandos. El primero con el nombre de la funcion y el segundo toma la funcion. 
                                                                        //Esto sirve para unir el nombre con la funcion en el diccionario
    {
        allCommands[name] = command; //registro en el diccionado llamado allCommands
    }

    //Devolucion
    public void WriteInConsole(string txt) //Escribe lo que se ingrese en la consola
    {
        backgroundText.text += txt + "/n"; //ese /n es un enter
    }

    //Funciones (aca agregas las funciones que queres usar
    public void testConsoleWithoutReturn()//Cree este comando para testear la consola
    {
        print("endtest"); //tambien podes imprimir variables
        //Aca asignas a variables, sea activar funciones como un modo dios, vidas infinitas, etc
    }
    public void testConsoleWithReturn()//Cree esta funcion para que haga algo y devuelva algo a la consola
    {
        print("endtest");
        WriteInConsole("endtest");
    }
}
