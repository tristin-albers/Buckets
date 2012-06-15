using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Buckets
{
    [TestClass]
    public class BucketsTest
    {
        [TestMethod]
        public void SolutionStupidSimple()
        {
            //Lake
            //3 unit container
            //5 unit container
            //find 4 units

            int find = 4;

            Container a = new Container(3);
            Container b = new Container(5);

            Assert.IsTrue(a.Capacity == 3);
            Assert.IsTrue(b.Capacity == 5);
            Trace.TraceInformation("a: {0}/{1}", a.Units, a.Capacity);
            Trace.TraceInformation("b: {0}/{1}", b.Units, b.Capacity);
            //0/0

            b.Fill();
            Trace.TraceInformation("a: {0}/{1}", a.Units, a.Capacity);
            Trace.TraceInformation("b: {0}/{1}", b.Units, b.Capacity);
            Assert.IsTrue(b.Units == 5);
            //0/5
            
            a.Transfer(b);
            //3/2
            Trace.TraceInformation("a: {0}/{1}", a.Units, a.Capacity);
            Trace.TraceInformation("b: {0}/{1}", b.Units, b.Capacity); 
            Assert.IsTrue(a.Units <= a.Capacity);

            a.Dump();
            Assert.IsTrue(a.Units == 0);
            Trace.TraceInformation("a: {0}/{1}", a.Units, a.Capacity);
            Trace.TraceInformation("b: {0}/{1}", b.Units, b.Capacity);
            //0/2

            a.Transfer(b);
            Trace.TraceInformation("a: {0}/{1}", a.Units, a.Capacity);
            Trace.TraceInformation("b: {0}/{1}", b.Units, b.Capacity);
            Assert.IsTrue(a.Units == 2, "Expected 2");
            Assert.IsTrue(b.Units == 0, "Expected 0");
            //2/0

            b.Fill();
            //2/5

            a.Transfer(b);
            //3/4
            Trace.TraceInformation("a: {0}/{1}", a.Units, a.Capacity);
            Trace.TraceInformation("b: {0}/{1}", b.Units, b.Capacity);

            Assert.IsTrue(b.Units == find);
            Trace.TraceInformation("You Win!");
        }

        [TestMethod]
        public void ABitTougherSolution()
        {
            //Lake
            //3 unit container
            //5 unit container
            //find 4 units
            int find = 7;
            Container a = new Container(5);
            Container b = new Container(9);

            Trace.TraceInformation("a: {0}/{1}", a.Units, a.Capacity);
            Trace.TraceInformation("b: {0}/{1}", b.Units, b.Capacity);

            b.Fill();
            Trace.TraceInformation("a: {0}/{1}", a.Units, a.Capacity);
            Trace.TraceInformation("b: {0}/{1}", b.Units, b.Capacity);


            a.Transfer(b);
            Trace.TraceInformation("a: {0}/{1}", a.Units, a.Capacity);
            Trace.TraceInformation("b: {0}/{1}", b.Units, b.Capacity);

            a.Dump();
            Trace.TraceInformation("a: {0}/{1}", a.Units, a.Capacity);
            Trace.TraceInformation("b: {0}/{1}", b.Units, b.Capacity);

            a.Transfer(b);
            Trace.TraceInformation("a: {0}/{1}", a.Units, a.Capacity);
            Trace.TraceInformation("b: {0}/{1}", b.Units, b.Capacity);

            b.Fill();

            a.Transfer(b);
            Trace.TraceInformation("a: {0}/{1}", a.Units, a.Capacity);
            Trace.TraceInformation("b: {0}/{1}", b.Units, b.Capacity);

            //Assert.IsTrue(b.Units == find);
            Trace.TraceInformation("You Win!");
        }
    }

    public class Container
    {
        int capacity = 0;
        int units = 0;

        public Container(int maxCapacity)
        {
            capacity = maxCapacity;
        }

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
                    throw new Exception("You cannot put that much water in this container!");
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
    }
}
