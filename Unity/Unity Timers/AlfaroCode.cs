class lawea : monobehaviour
{
	public CountDownTimer Hability1 = new CountDownTimer().SetCoolDown(3f);
	
	
	attack()
	{
		if(Hability1.Ready)
		{
			//codigo
			//Verion io
			Hability1.StartCount();
			
			//Versión Alfaro
			StartCorroutine(Hability1.StartCD(3))
		}		
	}
}
////////////////////////////////////////////////////////////////////////////////////
public class CountDownTimer
{
	public bool Ready{get; private set;
	public float time;
	public Action miAcct = delegate{};
	public IEnumerator StartCD(float time){
		inCReadyD = true;
		yield return new Seconds(time);
		Ready = false;
		miAcct();
	}
}