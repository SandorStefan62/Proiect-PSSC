using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proiect.Domain.Models
{
    public record InvalidProduct(string Code, string Quantity, string Price);
}
