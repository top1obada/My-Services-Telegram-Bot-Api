using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyServicesTelegramBotBussinessTier.Services
{
    public interface IGetAllService<T>
    {

        public List<T> Get();

    }
}
