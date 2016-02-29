using StructureMap.Configuration.DSL;
using StructureMap.Graph;

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
        }
    }
}
