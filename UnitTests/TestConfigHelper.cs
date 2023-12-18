using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    public class TestConfigHelper
    {
        public static IConfigurationRoot GetIConfigurationRoot()
        {
            Console.WriteLine("Run");
            return new ConfigurationBuilder().AddJsonFile("appsettings.json").AddEnvironmentVariables().Build();
        }
    }
}
