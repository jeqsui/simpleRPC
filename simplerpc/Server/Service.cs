namespace Servers;

using Services;


/// <summary>
/// Service`asign 4 theards for each methods.
/// assigne a thread for resting
/// </summary>
public class Service : MarshalByRefObject, IService
{
	/// <summary>
	/// Access lock.
	/// </summary>
	private readonly Object accessLock = new Object();

	/// <summary>
	/// Service logic implementation.
	/// </summary>
	private static ServiceLogic logic = new ServiceLogic();

   


	/// <summary>
	/// Add given numbers.
	/// </summary>
	/// <param name="left">Left number.</param>
	/// <param name="right">Right number.</param>
	/// <returns>left + right</returns>
	//public int AddLiteralHeat(int v)
	//{
 //           return logic.AddLiteralHeat(v);    

 //   }
    public double AddLiteralFoodUseVar(double v)
    {
           return logic.AddLiteralFoodUseVar(v);
    }
    public double AddLiteralHeatUseVar(double v)
    {
        return logic.AddLiteralHeatUseVar(v);
    }


    public static bool ResetServer()
    {
        return logic.ResetServer();
    }


    public static void sold()
    {
         logic.sold();
    }


 



}