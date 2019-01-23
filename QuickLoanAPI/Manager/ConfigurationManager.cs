using System;
using Microsoft.Extensions.Configuration;

namespace QuickHomeLoanAPI.Manager
{
	public interface IConfigurationManager
    {
		string GetConfigurationByKey();

    }
	public class ConfigurationManager : IConfigurationManager
    {
		public ConfigurationManager(IConfiguration configuration)
        {
			Configuration = configuration;
        }
		private readonly IConfiguration Configuration;
		public string GetConfigurationByKey()
        {
			return Configuration.GetSection("CloudMessage").GetValue<string>("ServerKey");//["CloudMessage:ServerKey"];
        }
    }
}
