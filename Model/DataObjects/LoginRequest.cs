using System.ComponentModel.DataAnnotations;

namespace AdminClient.Model.DataObjects
{
	public class LoginRequest
	{
		[Required]
		public string Email { get; set; }
		[Required]
		public string Password { get; set; }

		public LoginRequest(string email, string password)
		{
			Email = email;
			Password = password;
		}
	}
}
