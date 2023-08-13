using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleOne
{
    public class CreateTeamErrorResponseModel
    {
        public string type;
        public string message;

        public void setType(string type)
        {
            this.type = type;
        }
        public void setMessage(string message)
        {
            this.message = message;
        }

        public string getType()
        {
            return this.type;
        }

        public string getMessage()
        {
            return this.message;
        }
    }
}
