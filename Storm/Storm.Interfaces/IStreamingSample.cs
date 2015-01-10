﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Storm.Interfaces
{
    public interface IStreamingSample
    {
        [OperationContract]
        Stream GetStream(string data);

        [OperationContract]
        bool UploadStream(Stream stream);

        [OperationContract]
        Stream EchoStream(Stream stream);

        [OperationContract]
        Stream GetReversedStream();

    }
}