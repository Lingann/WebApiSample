using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public interface IAppSetting
    {
       string Secret { get; set; }
    }
}
