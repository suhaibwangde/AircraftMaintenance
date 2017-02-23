using System;
using System.Collections.Generic;
using System.Linq;
using TireChangeRefactor.Model;

namespace TireChangeRefactor
{
	/// <summary>
	/// Service responsible for determining the maintenance that is required
	/// for aircrafts
	/// </summary>
	public class MaintenanceService
	{
		/// <summary>
		/// Gets all the aircraft that are due for a tire change.
		/// </summary>
		/// <returns>An array of aircraft that require tire changes according to mfg specifications</returns>
		public AircraftModel[] GetAllAircraftDueForTireChange()
		{
			// There are 3 aircraft manufactures, each with different requirements 
			//  for when the tires need to be changed
			//      FooPlane: 120 landings
			//      BarPlane: 75 landings
			//      BazPlane: 200 landings

			var repo = new DAL.AircraftRepository();
			var allAircraft = repo.GetAll().ToArray();
			var requiresTireChange = new List<AircraftModel>();
			if (allAircraft != null)
			{
				allAircraft.ToList().ForEach((aircraft) =>
				{
					var landings = new List<DateTime>();
					if (aircraft != null)
					{
						aircraft.Landings.ToList().ForEach((landing) =>
						{
							if (landing != null && landing >= aircraft.LastTireChange)
								landings.Add(landing);
						});
						if (aircraft.Manufacturer == "FooPlane" && landings.Count() >= 120)
							requiresTireChange.Add(aircraft);
						else if (aircraft.Manufacturer == "BarPlane" && landings.Count() >= 75)
							requiresTireChange.Add(aircraft);
						else if (aircraft.Manufacturer == "BazPlane" && landings.Count() >= 200)
							requiresTireChange.Add(aircraft);
					}
				});
			}

			return requiresTireChange.ToArray();
		}
	}
}
