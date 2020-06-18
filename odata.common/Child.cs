using System;
using System.Collections.Generic;
using System.Text;

namespace odata.common
{
    public class Child
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Parent Parent { get; set; }
    }
}
