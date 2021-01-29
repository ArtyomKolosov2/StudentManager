using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManager_Core.Entities.Base
{
    public interface IHasId
    {
        public string Id { get; set; }
    }
}
