using System.Collections.Generic;

namespace AdminClient.Model.DataObjects
{
	internal class Users
	{
		public string UserName { get; set; } = string.Empty;
		public string Email { get; set; } = string.Empty;
		public List<string> Names { get; set; }
		public string Partner { get; set; }

		public Users(string userName, string email, List<string> names, string partner)
		{
			UserName = userName;
			Email = email;
			Names = names;
			Partner = partner; // email
		}
	}
}
