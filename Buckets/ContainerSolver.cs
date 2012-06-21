using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Containers
{
    class ContainerSolver
    {
        internal static bool ValidateProblemVariables(int find, int a, int b)
        {
            if (a < 1 || b < 1 || find < 1)
                throw new ApplicationException("No value can be zero.");
            if (a < find && b < find)
                throw new ApplicationException("At least one container must be larger than the find value.");
            if (a % 2 == 0 && b % 2 == 0 && find % 2 != 0)
                throw new ApplicationException("An odd find can not be possible with even buckets.");
            if (b % a == 0 && find % a != 0)
                throw new ApplicationException("Buckets divisable exactly cannot ever reach the solution.");


            return true;
        }

        internal static int NextStep(Container a, Container b, int find, int counter)
        {
            int maxSteps = 100;
            if (counter >= maxSteps)
            {
                Trace.TraceWarning("Max attempts exception: {0} : {1} | {2}", find, a.Capacity, b.Capacity);
                throw new ApplicationException("Max attempts exceeded without a solution.");
            }
            if (b.Units == 0)
                b.Fill();
            else if (a.Units == a.Capacity)
                a.Dump();
            else
                a.Transfer(b);

            a.Report();
            b.Report();
            counter++;

            if (a.Units != find && b.Units != find)
                counter = NextStep(a, b, find, counter);

            return counter;
        }
    }
}
