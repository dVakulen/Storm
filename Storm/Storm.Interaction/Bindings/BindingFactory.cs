using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;

namespace Storm.Interaction.Bindings
{
    public abstract class BindingFactory<T> : IBindingsFactory<T> where T : Binding, new()
    {
        public T GetBinding()
        {
            return new T();
        }

        public abstract string GetEndpointPrefics();

    }
}
