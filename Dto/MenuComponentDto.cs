using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokladna.Dto
{
    internal class MenuComponentDto
    {
        public string Name { get; set; } = string.Empty;
        public List<string> Notes { get; set; } = new();
    }
}
