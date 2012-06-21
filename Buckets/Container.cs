using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Containers
{
    public class Container
    {
        int capacity = 0;
        int units = 0;

        public Container(char name, int maxCapacity)
        {
            Name = name;
            capacity = maxCapacity;
        }

        public char Name { get; set; }
        public int Capacity { get { return capacity; } }
        public int Units
        {
            get
            {
                return units;
            }
            set
            {
                if (value > Capacity || value < 0)
                    throw new ApplicationException("You cannot put that much water in this container!");
                units = value;
            }
        }

        public void Fill()
        {
            Trace.TraceInformation("Filling...");
            Units = Capacity;
        }

        public void Dump()
        {
            Trace.TraceInformation("Dumping...");
            Units = 0;
        }

        public void Transfer(Container source)
        {
            Trace.TraceInformation("Transfering...");
            if (source.Units + Units < Capacity)
            {
                Units += source.Units;
                source.Units = 0;
            }
            else
            {
                int movableUnits = source.Units - (source.Units - (Capacity - Units));
                Units += movableUnits;
                source.Units -= movableUnits;
            }
        }

        public void Report()
        {
            Trace.TraceInformation("{0}: {1}/{2}",Name, Units, Capacity);
        }
    }
}
