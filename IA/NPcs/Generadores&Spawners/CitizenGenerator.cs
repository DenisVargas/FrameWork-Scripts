namespace CitizenGenerator
{
	using system;
	using system.generic;
	public class Ente
	{
		public string Nombre;
	}
	public class Humano : Ente
	{
		public string Apellido;
		//true Hombre, false Mujer; its a trap!
		public const bool Sexo;
	}
	public class Citizen : Humano
	{
		public const Citizen Padre;
		public const Citizen Madre;
		public string EstadoCivil;
		public List<Humano> Hijos = new Hijos<Humano>;
		//Añadir espacio para los tìtulos.
		public Citizen(string _Nombre, string _Apellido, bool _Sexo, string _EstadoCivil,Citizen _Padre = null,Citizen _Madre = null)
		{
			Nombre = _Nombre;
			Apellido = _Apellido;
			Sexo = _Sexo;
			EstadoCivil = _EstadoCivil;
			Padre = _Padre;
			Madre = _Madre;
		}
		public AddKids(Citizen _Hijo)
		{
			Hijos.add(_Hijo);
		}
	}
}
