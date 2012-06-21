using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Containers
{
    [TestClass]
    public class ConteinerTests
    {
        [TestMethod]
        public void SolutionStupidSimple()
        {
            int find = 4;

            Container a = new Container('a', 3);
            Container b = new Container('b', 5);

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
            int find = 4;
            int counter = 0;

            Container a = new Container('a', 3);
            Container b = new Container('b', 5);

            counter = ContainerSolver.NextStep(a, b, find, counter);

            Assert.IsTrue(b.Units == find);
            Assert.IsTrue(counter == 6);
            Trace.TraceInformation("Solution Successful in {0} steps", counter);
        }

        [TestMethod]
        public void FindBucketSixModThree()
        {
            int find = 6;
            int counter = 0;

            Container a = new Container('a', 3);
            Container b = new Container('b', 9);

            if (find % a.Capacity == 0)
                counter = ContainerSolver.NextStep(b, a, find, counter);
            else
                counter = ContainerSolver.NextStep(a, b, find, counter);

            Assert.IsTrue(b.Units == find);
            Assert.IsTrue(counter == 4);
            Trace.TraceInformation("Solution Successful in {0} steps", counter);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException), "Buckets divisable exactly cannot ever reach the solution.")]
        public void TestBucketsDivisibleEvenlyException()
        {
            Assert.IsTrue(ContainerSolver.ValidateProblemVariables(15, 3, 18));

            ContainerSolver.ValidateProblemVariables(14, 3, 18);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException), "No value can be zero.")]
        public void TestZeroFindValueException()
        {
            ContainerSolver.ValidateProblemVariables(0, 3, 5);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException), "No value can be zero.")]
        public void TestZeroBucketCapacityException()
        {
            ContainerSolver.ValidateProblemVariables(4, 0, 5);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException), "An odd find can not be possible with even buckets.")]
        public void TestEvenEvenOddValidation()
        {
            ContainerSolver.ValidateProblemVariables(3, 2, 4);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException), "At least one container must be larger than the find value.")]
        public void TestFindLargerThanBucketsValidation()
        {
            ContainerSolver.ValidateProblemVariables(10, 2, 4);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException), "Max attempts exceeded without a solution.")]
        public void TestMaxStepsCounterException()
        {
            int find = 149;
            int counter = 0;

            Container a = new Container('a', 1);
            Container b = new Container('b', 150);

            counter = ContainerSolver.NextStep(b, a, find, counter);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException), "You cannot put that much water in this container!")]
        public void TestMaxCapacityException()
        {
            int capacity = 1;
            Container a = new Container('a', capacity);

            a.Units = 2;
        }

        [TestMethod]
        public void TestOneHundredVariationsWithExceptions()
        {
            Random rand = new Random();

            List<Exception> exceptions = new List<Exception>();

            for (int i = 0; i < 100; i++)
            {
                int min = 0;
                int max = 25;
                int find = rand.Next(min, max);
                int aC = rand.Next(min, max);
                int bC = rand.Next(min, max);
                int counter = 0;

                try
                {
                    if (ContainerSolver.ValidateProblemVariables(find, aC, bC))
                    {
                        Trace.TraceInformation("Starting: {0} : {1} / {2}", find, aC, bC);
                        Container a = new Container('a', aC < bC ? aC : bC);
                        Container b = new Container('b', aC < bC ? bC : aC); // Container "b" should be the largest

                        if (find % a.Capacity == 0)
                            counter = ContainerSolver.NextStep(b, a, find, counter);
                        else
                            counter = ContainerSolver.NextStep(a, b, find, counter);

                        Assert.IsTrue(a.Units == find || b.Units == find);
                        Trace.TraceInformation("Finished: {0} : {1} / {2} in {3} steps", find, aC, bC, counter);
                    }
                }
                catch (Exception e)
                {
                    exceptions.Add(e);
                }
            }

            Trace.TraceWarning("Execptions caught:{0}", exceptions.Count);
            exceptions.ForEach(x => Trace.TraceWarning(x.Message));
        }  
    }
}
