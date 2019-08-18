using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MazeManager : MonoBehaviour
{

    public GameObject slot;
    public static List<Slot> maze = new List<Slot>();
    public static MazeManager instance;

    private void Awake()
    {
        instance = this;
    }

    //cuando se presiona "Generate Maze"
    public void CreateMaze()
    {
        StopAllCoroutines();
        StartCoroutine(Maze());
    }

    
    IEnumerator Maze()
    {
        //limpia la grilla previa
        foreach (var item in maze)
        {
            yield return new WaitForEndOfFrame();
            Destroy(item.gameObject);
        }
        maze.Clear();
        //crea una nueva grilla
        StartCoroutine(CreateEmptyMaze(int.Parse(GameObject.Find("Height").GetComponent<Text>().text), int.Parse(GameObject.Find("Width").GetComponent<Text>().text)));
    }

    IEnumerator CreateEmptyMaze(int x, int y)
    {
        //creacion de la nueva grilla
        float xBase = -Screen.width * 0.5f + 100;
        float yBase = -Screen.height * 0.5f + 100;
        int id = 0;
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                id++;
                GameObject slGo = Instantiate(slot);
                slGo.transform.SetParent(FindObjectOfType<Canvas>().transform);
                slGo.GetComponent<RectTransform>().localPosition = new Vector3(xBase + j * 40, yBase, 0);
                var sl = slGo.GetComponent<Slot>();
                sl.id = id;
                sl.transform.GetChild(4).GetComponent<Text>().text = "" + id;
                sl.pos[0] = xBase + j * 40;
                sl.pos[1] = yBase;
                maze.Add(sl);
                yield return new WaitForEndOfFrame();
            }
            yBase += 40;
        }
        //crea el vecindario
        StartCoroutine(CreateNeighbourhood());
    }

    IEnumerator CreateNeighbourhood()
    {
        //se agregan los vecinos a las listas
        foreach (var sl in maze)
        {
            var cols = Physics.OverlapSphere(sl.transform.position, 30);
            foreach (var neigh in cols)
            {
                if (neigh.gameObject != sl.gameObject)
                    sl.links.Add(neigh.GetComponent<Slot>());
            }
        }
        //se inicia el DFS para crear el laberinto
        StartCoroutine(DFS.DeepFirstSearch(maze[0]));
        yield return null;
    }

    //se llama al BFS para solucionar el laberinto
    public void DoneDFS()
    {
        StartCoroutine(BFS.BreadthFirstSearch(maze[0], maze[maze.Count - 1]));
    }

    public void DoneBFS()
    {
        //freno las corrutinas (si es que quedo alguna)
        StopAllCoroutines();

        //termino de colorear y acomodar la UI
        StartCoroutine(ColorThePath());
        BringUIToFront();
    }

    //pinto el camino
    IEnumerator ColorThePath()
    {
        for (int i = BFS.Path.Count - 1; i > 0; i--)
        {
            BFS.Path[i].transform.GetComponentInChildren<Text>().color = Color.blue;
            yield return new WaitForSeconds(0.2f);
        }
    }

    //traigo los botones y labels al frente
    void BringUIToFront()
    {
        GameObject.Find("Inputs").transform.SetParent(transform);
        GameObject.Find("Inputs").transform.SetParent(FindObjectOfType<Canvas>().transform);
    }
}
