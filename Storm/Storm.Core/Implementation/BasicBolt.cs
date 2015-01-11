using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Storm.Core.Abstract;

namespace Storm.Core.Implementation
{
    public class BasicBolt : IBasicBolt
    {
        public virtual string Execute1(string input)
        {
            return input + " Bolted";
        }
        public void Execute(string input)
        {
            return;
        }

        public void Cleanup()
        {

        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {

        }
    }
    public class BasicBoltTest : BasicBolt
    {
        public override string Execute1(string input)
        {
            input += " twice ";
            return base.Execute1(input);
        }
    }
}
