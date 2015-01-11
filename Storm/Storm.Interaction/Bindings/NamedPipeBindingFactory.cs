using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Storm.Interaction.Bindings
{
    public class NamedPipeBindingFactory : BindingFactory<NetNamedPipeBinding>
    {
        public override string GetEndpointPrefics()
        {
            return "net.pipe://";
        }
    }
}
