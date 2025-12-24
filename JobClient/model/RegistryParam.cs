using System;

namespace JobClient
{
    public class RegistryParam
    {
        public RegistryParam()
        {

        }

        public RegistryParam(string registryGroup, string appName, string executorAddress)
        {
            this.registryGroup = registryGroup;
            this.registryKey = appName;
            this.registryValue = executorAddress;
        }

        public string registryGroup { get; set; }
        public string registryKey { get; set; }
        public string registryValue { get; set; }


        public override string ToString()
        {
            return "RegistryParam{" +
              "registryGroup='" + registryGroup + '\'' +
              ", registryKey='" + registryKey + '\'' +
              ", registryValue='" + registryValue + '\'' +
              '}';
        }
    }
}
