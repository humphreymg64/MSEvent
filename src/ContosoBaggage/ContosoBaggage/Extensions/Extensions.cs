using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

using Xamarin.Forms;

namespace ContosoBaggage.Extensions
{
	public static class Extensions
	{
		#region Generic

		public static string GetFileContents(this string fileName)
		{
			var assembly = typeof(App).GetTypeInfo().Assembly;
			var name = assembly.ManifestModule.Name.Replace(".dll", string.Empty);
			var stream = assembly.GetManifestResourceStream($"{name}.Resources.{fileName}");

			if(stream == null)
				return null;

			string content;
			using(var reader = new StreamReader(stream))
			{
				content = reader.ReadToEnd();
			}

			return content;
		}

		#endregion
	}
}