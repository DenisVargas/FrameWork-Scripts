//Source de ejemplo de design patterns, nunca compilado
//Por Martín Sebastián Wain 2016-05-01
//Actualizado 2016-11-19

/////////////////////////////////////////////////
//Strategy
/////////////////////////////////////////////////

interface IStrategy {
	void OnHit(Spell s, Target t);
}

public class Damage : IStrategy { ... }
public class Heal : IStrategy { ... }

public class Spell {
	IStrategy _strategy;
	int _repeat = 5;

	public Gun(IStrategy strtegy) {
		_strategy = _strategy;
	}

	public void OnHitTarget(Target t) {
		if(_repeat > 0) {
			_strategy.OnHit(s, t);
			_repeat--;
		}
	}
}

/////////////////////////////////////////////////
//(Partial) Builder
/////////////////////////////////////////////////

class ParticleBuilder {
	Particle _p;
	ParticleBuilder(Particle p = null) {
		_p = p != null ? p : new Particle();
	}
	ParticleBuilder Pos(int x, int y) { _p.x = x; _p.y = y; return this; }
	ParticleBuilder Vel(int vx, int vy) { _p.velocity.x = x; _p.velocit.y = y; return this; }
	ParticleBuilder Color(int hexColor) { _p.material = Utils.HexToMaterial(hexColor); return this; }

	Particle GetResult() { return _p; }
}

var pb = new ParticleBuilder();
pb.Pos(10,11).Vel(80, 0);
//...
var particle = pb.Color(0xff0000).GetResult();
addToScene(particle);

/////////////////////////////////////////////////
// Factory: Depende de parametros
/////////////////////////////////////////////////

interface IFood {
	void Eat();
}

class Burger : IFood { ... }
class HotDog : IFood { ... }

class Factory {
	public IFood CreateFood(string type) {
		switch(type) {
			case "Burger": return new Burger();
			case "Hotdog": return new HotDog();
			default: throw new Exception("Invalid food type");
		}
	}
}

/////////////////////////////////////////////////
// Factory: Función anónima (Delegate/lambda)
/////////////////////////////////////////////////

delegate IFood CreateFoodDelegate();

CreateFoodDelegate burgerFactory = () => { return new Burger(); };
CreateFoodDelegate hotdogFactory = () => { return new HotDog(); };

/////////////////////////////////////////////////
// Factory method: Depende de la fabrica (NO parametros)
/////////////////////////////////////////////////

interface IFood {
	void Eat();
}
interface IFactory {
	void CreateFood();
}

class Burger : IFood { ... }
class HotDog : IFood { ... }

class BurgerFactory : IFactory {
	public IFood CreateFood() { return new Burger(); }
}

class HotDogFactory : IFactory {
	public IFood CreateFood() { return new Hotdog(); }
}

/////////////////////////////////////////////////
// Singleton
/////////////////////////////////////////////////

class Singleton {
	//Statics porque son campo/propiedad de la clase, no de los objetos
	static Singleton _instance;
	static public Singleton instance {
		get {
			if(_instance == null)
				_instance = new Singleton();
			return _instance;
		}
	}
	private Singleton() {	//¡Constructor privado! Solo se puede crear desde funciones de la clase

	}
	public int score;
}

//Desde cualquier lado
Singleton.instance.score++;

/////////////////////////////////////////////////
//Flyweight
/////////////////////////////////////////////////
//Esta es la clase Flyweight real (almacena los datos comunes).
class Mesh {
	Vector3 vertices;
}

class Flyweight {
	public Vector3 offset { get; set; }
	Mesh _mesh;
	Flyweight(Mesh mesh) {
		_mesh = mesh;
		offset = new Vector3;
	}
	public void Render() {
		//Todos usan el mismo Mesh, pero con distinto offset
		foreach(v in _mesh.vertices)
			DrawLine(v + offset)
	}
}

/////////////////////////////////////////////////
//Abstract factory
/////////////////////////////////////////////////

//Varios factory method organizados por temática (Auto <- Puerta, Chasis, Rueda)
interface IAbstractCarFactory {
	Door CreateDoor();
	Chassis CreateChassis();
	IWheel CreateWheel();
}

class RenaultFactory : IAbstractCarFactory {
	public Door    CreateDoor()    { return new RenaultDoor(); }
	public Chassis CreateChassis() { return new RenaultChassis(); }
	public IWheel  CreateWheel()   { return new RenaultWheel(); }
}

class ChevroletFactory : IAbstractCarFactory {
	public Door   CreateDoor()     { return new ChevroletDoor(); }
	public Chassis CreateChassis() { return new ChevroletChassis(); }
	public IWheel  CreateWheel()   { return new ChevroletWheel(); }
}

/////////////////////////////////////////////////
//Prototype
/////////////////////////////////////////////////

abstract class Prototype {
	public abstract Prototype Clone();
}
//o
interface IPrototype {
	IPrototype Clone();
}
//o también funcion en LA CLASE MISMA (sin herencia/implementacion), no hace falta q sean derivadas.

class PrototypeGuy : Prototype {
	AttackStrategy attack;
	List<Item> inventory;
	int hp;
	int xp;
	//...

	override Prototype Clone() {
		var r = new PrototypeGuy();
		r.attack = attack;
		r.hp = hp;
		r.xp = xp;

		//Deep-copy del inventario (¡No queremos compartir items en memoria!)
		r.inventory = new List<Item>();
		for(i in inventory)
			r.Add(i.Clone())

		return r;
	}
}

class Item : Prototype {	//Tambien prototype
	... //lo mismo
	override Item Clone() { ... }
}

/////////////////////////////////////////////////
//Lookup table
/////////////////////////////////////////////////

const string nombreMes[] = { "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" }

string numeroANombreMes(int numeroMes) {
	if(numeroMes< 1 || numeroMes > 12) return "Invalido";
	else return NombreMes[numeroMes-1];
}

//Otro ejemplo:
Dictionary<string, IFactory> factories;
factories["goblin"] = new GoblinFactory();
factories["elf"]    = new ElfFactory();
//...
var newEnemy = factories[type].CreateEnemy()

/////////////////////////////////////////////////
//Adapter
/////////////////////////////////////////////////

interface IService {
	Vector3 position { get; set; }
}

interface IClient {
	void Move(float x, float y, float z);
}

class Adapter : IClient {
	IService _obj

	Adapter(IService obj) {
		_obj = obj;
	}

	public void Move(float x, float y, float z) {
		_obj.position += Vector3(x, y, z);
	}
}

/////////////////////////////////////////////////
//Proxy 1 (control a maestro)
/////////////////////////////////////////////////

interface IService {
	void LateUpdate();		//Sucede al final del frame.
	void Destroy();			//Si se llama en el medio de un frame rompe la física.
}

class Proxy1 : IService {
	IService _master;
	bool _flagDestroy;

	Proxy1(IService master) { _master = master; }

	public void Destroy() {
		//Acá está controlando acceso al maestro:
		//No destruir durante un frame de renderizado (por ej. desde un thread)
		if(Renderer.InsideFrame)   
			_flagDestroy = true;
		else
			_master.Destroy();
	}

	public void LateUpdate() {
		if(_flagDestroy)
			_master.Destroy();
		else
			_master.LateUpdate();
	}

	public void ComeCrawling() {
		Faster();
		ObeyYour(_master);
	}
}

var proxy = Proxy1(realObject);
void ThreadFunc() {
    ...
	realObject.Destroy()	//Error!
	proxy.Destroy();		//OK. Internamente lo hará luego en un late update
}

/////////////////////////////////////////////////
//Proxy 2 aka "Virtual Proxy" (Lazy initialization) 
/////////////////////////////////////////////////

//Acopla a ConcreteService, pero lo crea cuando REALMENTE hace falta.
class Proxy2 : IService {
	ConcreteService _image;
	Rectangle _rect;
	string _path;
	
	public Proxy2() {
	}

	public void LoadImage(string path) {
		var md = MetaDataLoader(path)
		_path = path;
		//Guarda una información general de tamaño
		_rect = md.GetRectangle()
	}

	public Rectangle rect { get; set; }

	//Crea acá porque hay q mostrar la imagen
	public void RenderImage()
		if(_image == null) {
			_image = new ConcreteService(_path);
		_image.Render(_rect)
	}
}



/////////////////////////////////////////////////
// Decorator
/////////////////////////////////////////////////

interface IService {
	void Emanar();
}

class Arbolito : IService {
	public void Emanar() { ... }
}

class DecoGuirnalda : IService {
	IService _serv;
	public DecoGuirnalda(IService serv) { _serv = serv; }
	public void Emanar() {
		_serv.Emanar();	//Llamada en cadena puede estar antes
		Global.Amor();
	}
}

class DecoLucesChinas : IService {
	IService _serv;
	public DecoLucesChinas(IService serv) { _serv = serv; }
	public void Emanar() {
		Global.IluminarElAlma();
		if(Random.value < 0.1) Global.IncendiarCasa();
		_serv.Emanar();	//Llamada en cadena puede estar luego
	}
}

class DecoEsferas : IService {
	IService _serv;
	public DecoEsferas(IService serv) { _serv = serv; }
	public void Emanar() {
		Global.ReflexionarSobreDecisionesPasadas();
		_serv.Emanar();
	}
}

//Notese que se extiende SIN HERENCIA CLÁSICA.
var arbol = new Arbolito();
var esferas = new DecoEsferas(arbol);				//esferas->arbol
var guirnalda1 = new DecoGuirnalda(esferas);		//guirnalda1->esferas->arbol
var luces = new DecoLucesChinas(guirnalda1);		//luces->guirnalda1->esferas->arbol
var guirnalda2 = new DecoGuirnalda();				//guirnalda2->luces->guirnalda1->esferas->arbol

guirnalda2.Emanar();	//llama toda la cadena

AddService(guirnalda2);	//como todas son IService son apilables y se pueden pasar a lo que espera un IService




/////////////////////////////////////////////////
//Facade
/////////////////////////////////////////////////

class BakeryFacade {
	Bank _bank;
	Bag _bag;
	BakeryFacade(Bank bank) {
		_bank = bank;
		_bag = new Bag();
	}
	Bag robBank() {
		var drillGuy = new DrillGuy();
		var bp = Government.instance.findBlueprint(_bank);
		var vault = bp.getVaultPos(bank);
		while(!vault.wall[Dir.LEFT].isOpen()) {
			drillGuy.move(Dir.DOWN);
			drillGuy.drill(vault);
			drillGuy.move(Dir.UP);
			drillGuy.sleep(8);
		}
		drillGuy.move(Dir.RIGHT);
		drillGuy.fill(_bag, vault.contents);
		drillGuy.move(Dir.LEFT);
		drillGuy.move(Dir.UP);
		drillGuy.escape();
		return bag;
	}
}

/////////////////////////////////////////////////
//Composite
/////////////////////////////////////////////////

interface IComponent {
	void Render();
	int Count();
}

interface IComposite : IComponent {		//Notar que extiende la interfaz de IComponent
	void Add(Component c)
	void Remove(Component c)
}

class Sprite : IComponent {
	public void Render() { ... }
	public int Count() { return 1; }
}

class SpriteContainer : IComposite {
	List<IComponent> _components;
	public void Add(Component c) { _components.Add(c); }
	public void Remove(Component c) { _components.Remove(c); }

	public void Render() {
		foreach(c in components)
			c.Render();
	}

	public int Count() {
		int accum = 0;
		foreach(c in components)
			accum += c.Count();
		return accum;
	}
}

void CompositeRunStuff() {
	var a = SpriteContainer();
	a.Add(new Sprite())
	a.Add(new Sprite())

	var b = new SpriteContainer()
	b.add(new Sprite())
	b.add(a);			//<--- !!! Notese que se agregó un composite como componente.

	//...
	int count = b.Count();		//da 3!
	//...
	b.Render();		//dibuja en orden de agregados
}

/////////////////////////////////////////////////
//Dispose
/////////////////////////////////////////////////

class TextFileWriter {
	public TextFileWriter(string path) {
		...abre archivo...
	}
	public void Dispose() {
		...cierra archivo así otro lo puede usar...
	}
}

/////////////////////////////////////////////////
//Command 1 (solo parametros que ejecutará el handler/listener/receiver)
/////////////////////////////////////////////////

class Event {
	enum Type {
		Frame
	}
	int type { get; set; }
}

class MouseEvent : Event {
	enum Type {
		MouseClick,
		MouseMove
	}
	int x, int y;
	int buttonIndex;
}

class KeyEvent : Event {
	enum Type {
		KeyUp,
		KeyDown
	}
	int keyCode;
}

void listener(Event e) {
	if(e.type == MouseEvent.MouseClick) {
		var me = e as MouseEvent;
		Game.SelectAt(me.x, me.y)
	}
}
/////////////////////////////////////////////////
//Command 2 (El mismo command ejecuta la acción)
/////////////////////////////////////////////////

interface ICommand {
	void Do();
	void Undo();
}

class CommandSetPixel : ICommand {
	int _x, _y;
	Image _image;
	UInt32 _colorNew;
	UInt32 _colorPrev;
	CommandSetPixel(Image image, int x, int y, UInt32 color) {
		_image = image;
		_x = x;
		_y = y;
		_colorNew = color;
	}
	public void Do() {
		_colorPrev = _image.pixels[x][y];
		_image.pixels[x][y] = _colorNew;
	}
	public void Undo() {
		_image.pixels[x][y] = _colorPrev;
	}

}

Stack<ICommand> history;

//On click...
var command = new CommandSetPixel(image, event.x, event.y, palette.selected);
history.Push(command);
command.Do();

//On undo...
if(history.Count > 0)
	history.Pop().Undo();


/////////////////////////////////////////////////
// Memento
/////////////////////////////////////////////////

class Memento {
	public List<Object> objects;
	public int index;
}	

class SelectionManager {
	List<Object> _selected;
	int _index;

	public Memento GetMemento() {
		var m = new Memento;
		m.objects = new List<Object>();
		m.objects.AddRange(_selected);
		m.index = _index;
		return m;
	}
	public void SetMemento(Memento m) {
		_selected.Clear();
		_selected.AddRange(m.objects)
		_index = m.index;
		UpdateSelectionGraphics();
	}

	...
}

/////////////////////////////////////////////////
// Observer
/////////////////////////////////////////////////

interface IObserver {
	void Notify(int i, string s);
}

class Observer1 : IObserver {
	public void Notify(int i, string s) {
		if(i > 10) Debug.Log(s);
	}
}

class Observer2 : IObserver {
	public void Notify(int i, string s) {
		if(s == "reset")
			ReloadLevel();
	}
}

class Subject {
	List<IObserver> _observers;

	public void AddObserver(IObserver obs) { list.Add(obs); }
	public void RemoveObserver(IObserver obs) { list.Remove(obs); }
	void NotifyObservers(int i, string s) {
		foreach(obs in _observers)
			obs.notify(i, s);
	}
}