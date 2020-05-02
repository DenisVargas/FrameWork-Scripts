using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class PointWindow : EditorWindow
{
    public PointManager manager;
    public Point point;
    public Point currentpoint;

    [MenuItem("IA/Point Manager")]
    public static void OpenWindow()
    {
        GetWindow(typeof(PointWindow));
    }
    private void OnEnable()
    {
        manager = FindObjectOfType<PointManager>();
    }

    private void OnGUI()
    {
        manager = (PointManager)EditorGUILayout.ObjectField(manager, typeof(PointManager), true);

        GetNodePrefab();

         if ( !currentpoint )
            CreateFirstPoint();
         else
        {
          Current();
          PointThing();
        }

    }

    void GetNodePrefab()
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Prefab", GUILayout.Width(50));
        point = (Point)EditorGUILayout.ObjectField(point, typeof(Point), false);
        EditorGUILayout.EndHorizontal();
    }
    void CreateFirstPoint() {
        if(GUILayout.Button("First Button") ) {
            Point p = Instantiate(point);
            currentpoint = p;
            manager.points.Add(p);
            p.name = manager.points.Count.ToString();
            currentpoint.selected = true;

        }
    }
    void Current()
    {
        GUILayout.Space(10);
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Current Point", GUILayout.Width(100));
        currentpoint = (Point)EditorGUILayout.ObjectField(currentpoint, typeof(Point), false);
        EditorGUILayout.EndHorizontal();

    }

    void PointThing() {
        //TODO la cosita para agregar o quitar nodos que es como una +
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.BeginVertical();
        GUILayout.Space(20);
        Point p = currentpoint.GetAdjacentPoint(currentpoint.transform.position + Vector3.left * manager.distance);

        if ( p == null ) {
            if ( GUILayout.Button("+") ) {
                Point check = manager.GetAdjacentPoint(currentpoint.transform.position + Vector3.left * manager.distance);
                if ( check ) {
                    check.adjacents.Add(currentpoint);
                    currentpoint.adjacents.Add(check);
                } else {

                Point p2 = Instantiate(point, currentpoint.transform.position + Vector3.left * manager.distance, Quaternion.identity);
                manager.points.Add(p2);
                p2.adjacents.Add(currentpoint);
                currentpoint.adjacents.Add(p2);
                p2.name = manager.points.Count.ToString(); ;
                }
            }

        } else if(p != null){
            if ( GUILayout.Button("←") ) {
                currentpoint.selected = false;
                currentpoint = p;
                currentpoint.selected = true;
                SceneView.RepaintAll();
            }

        }
        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginVertical();

        Point q = currentpoint.GetAdjacentPoint(currentpoint.transform.position + Vector3.forward * manager.distance);

        if ( q == null ) {
            if ( GUILayout.Button("+") ) {
                Point check = manager.GetAdjacentPoint(currentpoint.transform.position + Vector3.forward * manager.distance);
                if ( check ) {
                    check.adjacents.Add(currentpoint);
                    currentpoint.adjacents.Add(check);
                } else {

                    Point q2 = Instantiate(point, currentpoint.transform.position + Vector3.forward * manager.distance, Quaternion.identity);
                    manager.points.Add(q2);
                    q2.adjacents.Add(currentpoint);
                    currentpoint.adjacents.Add(q2);
                    q2.name = manager.points.Count.ToString();
                }
            }

        } else if ( q != null ) {
            if ( GUILayout.Button("↑") ) {
                currentpoint.selected = false;
                currentpoint = q;
                currentpoint.selected = true;
                SceneView.RepaintAll();
            }

        }

        GUILayout.Space(20);

        Point r = currentpoint.GetAdjacentPoint(currentpoint.transform.position + Vector3.back   * manager.distance);

        if ( r == null ) {
            if ( GUILayout.Button("+") ) {
                Point check = manager.GetAdjacentPoint(currentpoint.transform.position + Vector3.back * manager.distance);
                if ( check ) {
                    check.adjacents.Add(currentpoint);
                    currentpoint.adjacents.Add(check);
                } else {

                    Point r2 = Instantiate(point, currentpoint.transform.position + Vector3.back * manager.distance, Quaternion.identity);
                    manager.points.Add(r2);
                    r2.adjacents.Add(currentpoint);
                    currentpoint.adjacents.Add(r2);
                    r2.name = manager.points.Count.ToString();
                }
            }

        } else if ( r != null ) {
            if ( GUILayout.Button("↓") ) {
                currentpoint.selected = false;
                currentpoint = r;
                currentpoint.selected = true;
                SceneView.RepaintAll();
            }

        }
        EditorGUILayout.EndVertical();
        EditorGUILayout.BeginVertical();
        GUILayout.Space(20);
        Point s = currentpoint.GetAdjacentPoint(currentpoint.transform.position + Vector3.right * manager.distance);

        if ( s == null ) {
            if ( GUILayout.Button("+") ) {
                Point check = manager.GetAdjacentPoint(currentpoint.transform.position + Vector3.right * manager.distance);
                if ( check ) {
                    check.adjacents.Add(currentpoint);
                    currentpoint.adjacents.Add(check);
                } else {

                    Point s2 = Instantiate(point, currentpoint.transform.position + Vector3.right * manager.distance, Quaternion.identity);
                    manager.points.Add(s2);
                    s2.adjacents.Add(currentpoint);
                    currentpoint.adjacents.Add(s2);
                    s2.name = manager.points.Count.ToString();
                }
            }

        } else if ( s != null ) {
            if ( GUILayout.Button("→") ) {
                currentpoint.selected = false;
                currentpoint = s;
                currentpoint.selected = true;
                SceneView.RepaintAll();

            }

        }

        EditorGUILayout.EndVertical();
        EditorGUILayout.EndHorizontal();
            

    }

}
/*
{
    [SerializeField]
    public NodeGroup nodegroup;
    public Node nodo;
    private string newFolderName = "Bullet prefabs";
    bool hasFolder;
    private string folderPath;

    [MenuItem("IA/Node Group")]
    public static void OpenWindow()
    {       //Start 
        GetWindow(typeof(GroupEditorWindow));

    }
    private void OnEnable()
    {
        folderPath = "Assets/Node editor/Node Prefabs";
    }

    private void OnGUI()
    {
        Rect buttonRect = new Rect(position.height / 3, position.width / 5, 0, 0);
        EditorGUILayout.LabelField("Grupo de Nodos", EditorStyles.centeredGreyMiniLabel);

        EditorGUILayout.BeginHorizontal();
        ChooseNodeGroup();
        NewNodeGroup();
        DeleteGroup();
        EditorGUILayout.EndHorizontal();

        if (nodegroup)
        {

            //TODO Funciones+
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(nodegroup.name + ": Edicion", EditorStyles.boldLabel);
            if (GUILayout.Button("Editar"))
            {
                Debug.Log("editadah");
                var popup = new EditNamePopup
                {
                    ng = nodegroup
                };
                PopupWindow.Show(buttonRect, popup);
            }

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            GetNodePrefab();
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            CreateNode();
            AddFirst();
            AddLast();
            ClearButton();
            EditorGUILayout.EndHorizontal();


            if (AssetDatabase.IsValidFolder(folderPath))
            {
                if (GUILayout.Button("Guardar grupo como archivo"))
                {
                    PrefabUtility.CreatePrefab(folderPath + "/" + nodegroup.name + ".prefab", nodegroup.gameObject);
                }
            }
            else
            {
                EditorGUILayout.HelpBox("No tenes carpeta de Prefabs! Haz click para crear una.", MessageType.Warning);
                if (GUILayout.Button("Crear Carpeta "))
                {
                    AssetDatabase.CreateFolder("Assets/Node editor", "Node Prefabs");
                }

            }
        }
    }

    void ChooseNodeGroup()
    {
        EditorGUILayout.LabelField("Grupo", GUILayout.Width(50));
        nodegroup = (NodeGroup)EditorGUILayout.ObjectField(nodegroup, typeof(NodeGroup), true);
    }

    public void NewNodeGroup()
    {
        if (GUILayout.Button("+", GUILayout.Width(20)))
        {

            NodeGroup ng = new GameObject().AddComponent<NodeGroup>(); ;
            ng.nodeList = new List<Node>();
            nodegroup = ng;
            nodegroup.name = "NewGroup";
        }
    }

    public void DeleteGroup()
    {
        if (GUILayout.Button("-", GUILayout.Width(20)) && nodegroup)
        {
            DestroyImmediate(nodegroup.gameObject);
            Repaint();
        }
    }

    void GetNodePrefab()
    {
        EditorGUILayout.LabelField("Nodo", GUILayout.Width(50));
        nodo = (Node)EditorGUILayout.ObjectField(nodo, typeof(Node), true);
        nodegroup.nodePrefab = nodo;
    }

    private void CreateNode()
    {
        if (GUILayout.Button("Crear Nodo"))
        {
            Node n = nodegroup.NewNode();
            n.name = n.id.ToString();
            Selection.activeGameObject = n.gameObject;
        }
    }
    private void AddFirst()
    {
        if (GUILayout.Button("Agregar primer"))
        {
            Node n = nodegroup.NewNode();
            nodegroup.AddFirst(n);
            n.name = n.id.ToString();
        }
    }
    private void AddLast()
    {
        if (GUILayout.Button("Agregar ultimo"))
        {
            Node n = nodegroup.NewNode();
            nodegroup.AddLast(n);
            n.name = n.id.ToString();
        }

    }

    void ClearButton()
    {
        if (GUILayout.Button("Limpiar nodos"))
        {
            nodegroup.Clear();
        }

    }


}
*/
