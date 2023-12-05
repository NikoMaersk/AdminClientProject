using System;

namespace AdminClient.Model.DataObjects
{
	internal class NameMatch
	{
		public string Id { get; set; }
		public DateTime Date { get; set; }
		public string Name { get; set; }

		public NameMatch(string id, DateTime date, string name)
		{
			Id = id;
			Date = date;
			Name = name;
		}
	}
}
