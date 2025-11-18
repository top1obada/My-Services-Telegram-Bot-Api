using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyServicesTelegramBotBussinessTier.Services
{
    public interface IGetAllServiceParameter<R,P>
    {

        public List<R> Get(P parameter);

    }
}
