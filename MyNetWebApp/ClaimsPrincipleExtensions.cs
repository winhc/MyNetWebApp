using System;
using System.Security.Claims;

namespace MyNetWebApp
{
	public static class ClaimsPrincipleExtensions
	{
		public static string GetUserId(this ClaimsPrincipal claimsPrincipal)
		{
			return claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier).Value;
		}
	}
}

