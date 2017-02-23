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
			if (allAircraft != null)
			{
				return allAircraft.Where((aircraft) =>
				{
					if (aircraft != null)
					{
						var landingCount = aircraft.Landings.Count(L => L != null && L >= aircraft.LastTireChange);
						return (aircraft.Manufacturer == "FooPlane" && landingCount >= 120)
						|| (aircraft.Manufacturer == "BarPlane" && landingCount >= 75)
						|| (aircraft.Manufacturer == "BazPlane" && landingCount >= 200);
					}
					return false;
				}).ToArray();
			}

			return new List<AircraftModel>().ToArray();
		}
	}
}
