using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;

namespace Storm.Interaction.Bindings
{
    public interface IBindingsFactory
    {
        object GetBinding();
    }

    public interface IBindingsFactory<out T> : IBindingsFactory where T : Binding
    {
        T GetBinding();
    }
}
