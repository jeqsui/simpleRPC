namespace Servers;

using NLog;

using Services;
using System.Linq;


/// <summary>
/// Service logic.
/// </summary>
class ServiceLogic : IService
{
	
	//create a 
	public ServiceLogic() {
		aTimer = new System.Timers.Timer();
		aTimer.Interval = 2000;
		aTimer.Elapsed += OnTimedEvent;
		aTimer.AutoReset = true;
		aTimer.Enabled = true;
	}
	//a timer Constructor
	/// <summary>
	/// Logger for this class.
	/// </summary>
	private Logger log = LogManager.GetCurrentClassLogger();
	private readonly Object accessLock = new Object();
    private readonly int intervalmin = 10;
	private readonly int intervalmax = 150;
	public double v1SumOfHeat;
	public double v2SumOfFood;
	public double v3SumOfFishMass;
	public double FishMassMax = 20;
	bool FishmassEx = false;
	public static System.Timers.Timer aTimer;
	
	public  void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
	{
		CheckHeat(intervalmin, intervalmax, v2SumOfFood);
	}
	public double AddLiteralHeatUseVar(double v)
	{
		lock (accessLock)
		{
				
				v1SumOfHeat += v;
				log.Info(" Heat Amount " + v1SumOfHeat);
				return v1SumOfHeat;
		}

	}

	public double AddLiteralFoodUseVar(double v)
	{
		lock (accessLock)
		{
			if (!FishmassEx) {
				CheckFishMass(v2SumOfFood, v3SumOfFishMass);
				v2SumOfFood += v;
				log.Info(" Food Amount " + v2SumOfFood);
			}
			return v2SumOfFood;
		}
	}



	//public void twosecondtimerforheat()
 //   {
	//	aTimer = new System.Timers.Timer();
	//	aTimer.Interval = 2000;
	//	log.Info("{}", DateTime.Now);
	//	CheckHeat(intervalmin, intervalmax, v2SumOfFood);
	//	aTimer.AutoReset = true;
	//	aTimer.Enabled = true;
	//}


	public bool CheckHeat(int imn, int imx, double v2SumOfFood)
    {
		lock (accessLock)
        {
				if (v1SumOfHeat > imn && v1SumOfHeat < imx)
				{
					double pr = v2SumOfFood * 0.1;	
					double rmfood = v2SumOfFood - pr;
					v2SumOfFood = rmfood;
					v3SumOfFishMass += pr;
				log.Info("Heat Interval {} - {}", imn, imx);
				log.Info(" Removed Food {}, Amount of food after process {}, Amount of fish mass after process: {}. ", pr, rmfood, v3SumOfFishMass);
					return true;
				}
				else{
				return false;
				}
        }
    }

    public void CheckFishMass(double v2SumOfFood, double v3SumOfFishMass)
    {
        lock (accessLock)
        {
            if (v3SumOfFishMass > FishMassMax)
            {
				v2SumOfFood = v2SumOfFood;
				v3SumOfFishMass = v3SumOfFishMass;
				log.Info("Food FischMass {} - {}", v2SumOfFood, v3SumOfFishMass);
				log.Info("Die Fische werden verkauft.");
				log.Info("Der Server wird neu gestartet.");
				FishmassEx = true;
				//log.Info("{}", FishmassEx);
			}
        }

    }


	public  bool ResetServer()
    {
        lock (accessLock) {
			if (FishmassEx == true)
			{
				v1SumOfHeat = 0;
				v2SumOfFood = 0;
				v3SumOfFishMass = 0;
				log.Info("{}-{}-{}", v1SumOfHeat, v2SumOfFood, v3SumOfFishMass);
				return true;
			}
			else
			{
				return false;
			}

		}
            
	}

    public void sold()
    {
        FishmassEx = false;
    }





}