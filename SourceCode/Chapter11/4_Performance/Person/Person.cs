using System;

[assembly:CLSCompliant(true)]
namespace PersonInformationNS
{
	/// <summary>
	/// Summary description for Person.
	/// </summary>
	public class PersonInformation
	{
		string _fullName;
		string _employer;

		public PersonInformation(string fullName, string employer)
		{
			_fullName = fullName;
			_employer = employer;
		}

		public string FullName
		{
			get { return _fullName; }
		}

		public string Employer
		{
			get { return _employer; }
		}
	}
}
