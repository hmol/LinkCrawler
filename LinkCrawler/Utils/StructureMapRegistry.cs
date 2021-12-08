using LinkCrawler.Utils.Outputs;
using LinkCrawler.Utils.Settings;
using StructureMap;
using System;
using System.Collections;
using System.Configuration;
using System.Linq;

namespace LinkCrawler.Utils
{
    public class StructureMapRegistry : Registry
    {
        public StructureMapRegistry()
        {
            Scan(scan =>
            {
                scan.TheCallingAssembly();
                scan.WithDefaultConventions();
            });

            var providers = (ConfigurationManager.GetSection(Constants.AppSettings.OutputProviders) as Hashtable)?
                .Cast<DictionaryEntry>()
                .ToDictionary(d => d.Key.ToString(), d => d.Value.ToString());

            if (providers != null)
            {
                var pluginType = typeof(IOutput);

                foreach (var provider in providers)
                {
                    var concreteType = Type.GetType(provider.Value);

                    if (concreteType == null)
                    {
                        throw new ConfigurationErrorsException(string.Format(
                            "Output provider '{0}' not found: {1}",
                            provider.Key,
                            provider.Value
                        ));
                    }

                    if (!concreteType.GetInterfaces().Contains(pluginType))
                    {
                        throw new ConfigurationErrorsException(string.Format(
                            "Output provider '{0}' does not implement IOutput: {1}",
                            provider.Key,
                            provider.Value
                        ));
                    }

                    For(pluginType).Add(concreteType).Named(provider.Key);
                }
            }
        }
    }
}
