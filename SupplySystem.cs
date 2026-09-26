using UnityEngine;

public class SupplySystem : MonoBehaviour
{
	public supplyData[] supplies;
	
	[System.Serializable]
	public class supplyData
	{
		public string supplyName;
		
		public float gain; //Gain per unit of time
		public float amount; //TotalSupply
		public float useLoss; //loss or use of item
		
		public float useType; //0 = Military good, 1 = Staple Good - Food/Water, 2 = Leisure, 3 = Medical, 4 - Raw Material
		public float useMult; //Mult of good use.
		
		public bool usesStorage; //dictates if this good uses units of storage.
		
		public float tradePrice;
		public int tradeID; //Id for consumption 
		//[1=Rations][2=FreshGameFish][3=Water][4=Alcohol][5=MilitarySupplies][6=Gunpowder][7=Fabric/Clothes][8=Furs][9=Iron/Steel][10=MedicalSupplies][11=Wood][12=TobbaccoHerbs]
		
		public float consumptionPerLaborer;
		public float consumptionPerWarman;
		
		public float labourAlocatedToProduction; //Since there can only be 1 PM at a time, this can change. Labour Required to produce 1u/ of this unit
		public float currentLabourAlocated; //Labour Used.
		
	}
	
	public float totalStorage; //total Amount of barrels, crates, and other forms of storage.
	public float currentStored; //amount of every item
	public float requiredStorage; //required for currentStored if it surpasses storage.
	
	[Range(0, 100)]
	public float generalSufferingIndex; //Injuries of soldiers or shortages... lowers morale.
	
	public int nonCombatants; //Civilians... not the number of warmen, Laborer size
	public int infantrySize; //War party from YOU
	public int enemyWarParty; //Enemies to spawn every so oftern
	
	public float labourProduced;
	public float labourUsed;
	public float totalLabourNeeded;
	
	public float avgMorale;
	
	public EnvironmentTimeSystem daysPassedScript;
	//Weather conditions
	
	[Range(1, 4)]
	public int currentSeason;
	public float rainIntensity;
	public float snowIntensity;

	//ADD a new array for soldiers... 
	
	public void GarrisonLabourUse()
	{
		labourProduced = nonCombatants / (generalSufferingIndex + 1f);
	}

	public void GoodProductionSchemes()
	{
		supplyData rations = null;
		supplyData freshGame = null;
		supplyData water = null;
		supplyData alcohol = null;
		supplyData militarySupplies = null;
		supplyData gunpowder = null;
		supplyData fabric = null; 
		supplyData furs = null;
		supplyData metalIngots = null;
		supplyData medicalSupplies = null;
		supplyData wood = null;
		supplyData tobaccoHerbs = null;
		
		foreach (supplyData supply in supplies)
		{		
			//Rations[1]
			if (supply.tradeID == 1) rations = supply;
			
			//Fresh Game/Fish[2]
			else if (supply.tradeID == 2) freshGame = supply;
			
			//Water[3]
			else if (supply.tradeID == 3) water = supply;
			
			//Alcohol[4]
			else if (supply.tradeID == 4) alcohol = supply;
			
			//Military Supplies[5]
			else if (supply.tradeID == 5) militarySupplies = supply;
			
			//Gunpowder[6]
			else if (supply.tradeID == 6) gunpowder = supply;
			
			//Fabric[7]
			else if (supply.tradeID == 7) fabric = supply;
			
			//Furs[8]
			else if (supply.tradeID == 8) furs = supply;
			
			//Metal Ingots[9]
			else if (supply.tradeID == 9) metalIngots = supply;
			
			//medicalSupplies[10]
			else if (supply.tradeID == 10) medicalSupplies = supply;
			
			//Wood[11]
			else if (supply.tradeID == 11) wood = supply;
			
			//Tobacco and foragable herbs[12]
			else if (supply.tradeID == 12) tobaccoHerbs = supply;
		}
		
		if (rations == null || freshGame == null || water == null || alcohol == null || militarySupplies == null || gunpowder == null || fabric == null || furs == null || metalIngots == null || medicalSupplies == null || wood == null || tobaccoHerbs == null) return; //add "|| above foreach line name == null"
		
		//Rations
		rations.amount += rations.gain - rations.useLoss; //should be per day.
		
		//FreshGameFish
		freshGame.amount += freshGame.gain - freshGame.useLoss;
		
		//Labour Generation
		
		if (generalSufferingIndex >= 1f)
		{
			labourProduced = nonCombatants / generalSufferingIndex;
		}
		else 
		{
			labourProduced = nonCombatants; //GSI less than 1 doesnt matter.
		}
		
		//Gain of Goods (Production Schemes
		
		rations.gain = (rations.currentLabourAlocated / rations.labourAlocatedToProduction);
		freshGame.gain = (freshGame.currentLabourAlocated / freshGame.labourAlocatedToProduction);
		water.gain = (water.currentLabourAlocated / water.labourAlocatedToProduction);
		alcohol.gain = (alcohol.currentLabourAlocated / alcohol.labourAlocatedToProduction);
		militarySupplies.gain = (militarySupplies.currentLabourAlocated / militarySupplies.labourAlocatedToProduction);
		gunpowder.gain = (gunpowder.currentLabourAlocated / gunpowder.labourAlocatedToProduction);
		fabric.gain = (fabric.currentLabourAlocated / fabric.labourAlocatedToProduction);
		furs.gain = (furs.currentLabourAlocated / furs.labourAlocatedToProduction);
		metalIngots.gain = (metalIngots.currentLabourAlocated / metalIngots.labourAlocatedToProduction);
		medicalSupplies.gain = (medicalSupplies.currentLabourAlocated / medicalSupplies.labourAlocatedToProduction);
		wood.gain = (wood.currentLabourAlocated / wood.labourAlocatedToProduction);
		tobaccoHerbs.gain = (tobaccoHerbs.currentLabourAlocated / tobaccoHerbs.labourAlocatedToProduction);
	}
	
	public void StorageAmountGoods()
	{
		currentStored = 0f;
		
		foreach (supplyData supply in supplies)
		{		
			if (supply.usesStorage)
			{
				currentStored += supply.amount;
			}
			
			
		}			
	}
	
	public void GeneralSufferingIndex()
	{
		
	}
	
	
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        StorageAmountGoods();
		GoodProductionSchemes();
    }
}
