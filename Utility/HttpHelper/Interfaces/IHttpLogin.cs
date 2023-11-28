using System.Threading.Tasks;

namespace AdminClient.Utility.HttpHelper.Interfaces
{
    internal interface IHttpLogin
    {
		Task<string?> AdminLogin();
	}
}
