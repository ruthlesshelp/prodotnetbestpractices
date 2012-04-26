using System;
using System.IO;
using System.Collections;
using System.Data.SqlClient;
using System.Resources;
using System.Reflection;

using OperationNS;
using PersonInformationNS;

[assembly:CLSCompliant(true)]
namespace PeopleNS
{
	public partial class People
	{
		ResourceManager resourceManager;
		ArrayList _fullNames;
		ArrayList _companyNames;

#if OPTIMIZED_GETPEOPLE

		public People()
		{
			this.resourceManager = new ResourceManager("PeopleNS.Resources", Assembly.GetExecutingAssembly());
			// Get a list of names
			this._fullNames = GetNames(resourceManager, "fullNames");
			this._companyNames=GetNames(resourceManager, "companyNames");
		}
#else
		public People()
		{
			this.resourceManager = new ResourceManager("PeopleNS.Resources", Assembly.GetExecutingAssembly());
		}
#endif

#if OPTIMIZED_GETPEOPLE

		public ArrayList GetPeople(int number)
		{
			// This helps us illustrate our progress
			OperationControl.GetInstance().Maximum += number;
			ArrayList people = new ArrayList();

			
			for (int i = 1; i <= number; i++)
			{
				string companyName = "";
				string fullName = "";

				fullName = (string)this._fullNames[i];
				companyName = (string)this._companyNames[i];

				// Create an instance of Person based on name & company and add to list
				people.Add(new PersonInformation(fullName, companyName));
				// Increment our progress
				OperationControl.GetInstance().Increment(1);
			}
			// Return our list of people
			return people;
		}
#else
		public ArrayList GetPeople(int number)
		{
			// This helps us illustrate our progress
			OperationControl.GetInstance().Maximum += number;
			ArrayList people = new ArrayList();

		
			for (int i = 1; i <= number; i++)
			{
				string companyName = "";
				string fullName = "";

				//Get a list of all full names
				this._fullNames = GetNames(this.resourceManager, "fullNames");
				fullName = (string)this._fullNames[i];

				//Get a list of all company names
				this._companyNames = GetNames(this.resourceManager, "companyNames");
				companyName = (string)this._companyNames[i];

				// Create an instance of Person based on name & company and add to list
				people.Add(new PersonInformation(fullName, companyName));
				// Increment our progress
				OperationControl.GetInstance().Increment(1);
			}
			// Return our list of people
			return people;
		}
#endif

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1500:VariableNamesShouldNotMatchFieldNames", MessageId = "resourceManager")]
        private static ArrayList GetNames(ResourceManager resourceManager, string fileName)
		{
			ArrayList names = new ArrayList();
			string contents = resourceManager.GetString(fileName);

			using (StringReader reader = new StringReader(contents))
			{
				string name = reader.ReadLine();

				while (name != null)
				{
					names.Add(name.Trim());
					name = reader.ReadLine();
				}
			}
			return names;
		}
	}
}
