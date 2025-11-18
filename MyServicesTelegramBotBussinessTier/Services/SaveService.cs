using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyServicesTelegramBotBussinessTier.Services
{
    public interface ISaveService<R,P>
    {

        public R Save(P value);

    }
}
