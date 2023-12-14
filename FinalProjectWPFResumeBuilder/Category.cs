using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProjectResumeMaker
{
    class Category
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }
        public string Location { get; set; }
        public string YOA { get; set; }


        public override string ToString()
        {
            string formatted = String.Format("{0}\t {1}\t  {2}\t  {3}\t {4}", Id, CategoryName, CategoryDescription, Location, YOA);
            return formatted;
        }
    }
}
