using System;
using System.Collections.Generic;
using Jypeli;
using Jypeli.Assets;
using Jypeli.Controls;
using Jypeli.Effects;
using Jypeli.Widgets;
using System.Linq;

public struct XYC
{
    public double X;
    public double Y;
    public double D;
}
public struct VRPProblem
{ 
    public string Name;
    public double BestKnown;
    public int Dimension;
    public double Capacity;
    public XYC[] Requests;
}

enum VRPReadMode
{
    BASIC_INFO,
    COORDINATES,
    DEMANDS
};

public class VehicleRouter : Game
{
    VRPProblem ReadVRP(string fn)
    {
        VRPReadMode rm = VRPReadMode.BASIC_INFO;

        VRPProblem p = new VRPProblem();
        string[] lines = System.IO.File.ReadAllLines(@"C:\Users\Public\TestFolder\WriteLines2.txt");
        foreach (string line in lines)
        {
            if (rm == VRPReadMode.BASIC_INFO)
            {
                if (line.Contains("NAME"))
                    p.Name = line.Split(':')[1].Trim();
                if (line.Contains("BEST_KNOWN"))
                    p.BestKnown = Convert.ToDouble(line.Split(':')[1].Trim());
                if (line.Contains("DIMENSION"))
                    p.Capacity = Convert.ToDouble(line.Split(':')[1].Trim());
                if (line.Contains("DIMENSION"))
                {
                    p.Dimension = Convert.ToInt32(line.Split(':')[1].Trim());
                    p.Requests = new XYC[p.Dimension];
                }
                if (line.Contains("NODE_COORD_SECTION"))
                    rm = VRPReadMode.COORDINATES;
                if (line.Contains("DEMAND_SECTION"))
                    rm = VRPReadMode.DEMANDS;
            }
            else if (rm == VRPReadMode.COORDINATES)
            {
                var parts = line.Split();
                var reqIdx = Convert.ToInt32( parts[0] );
                var x = Convert.ToDouble( parts[1] );
                var y = Convert.ToDouble( parts[2] );
                p.Requests[reqIdx].X = x;
                p.Requests[reqIdx].X = y;
            }
            else if (rm == VRPReadMode.DEMANDS)
            {
                var parts = line.Split();
                var reqIdx = Convert.ToInt32(parts[0]);
                var d = Convert.ToDouble(parts[1]);
                p.Requests[reqIdx].D = d;
            }
        }
        return p;
    }

    public override void Begin()
    {
        // Kirjoita ohjelmakoodisi tähän

        PhoneBackButton.Listen(ConfirmExit, "Lopeta peli");
        Keyboard.Listen(Key.Escape, ButtonState.Pressed, ConfirmExit, "Lopeta peli");


        var problem = ReadVRP(@"C:\Users\juherask\Dissertation_TODO_SORT\Phases\Tuning\Benchmarks\Christofides\Christofides_06.vrp");

        problem.Requests.Min(r => r.X);
        problem.Requests.Max(r => r.X);

        foreach (var req in problem.Requests)
        {
            
        }
    }
}
