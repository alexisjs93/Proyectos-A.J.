// 2022
// Own project
// https://en.wikipedia.org/wiki/Euler_spiral

using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;

namespace Snake
{
    public class ClothoidComponent : GH_Component
    {
        /// Initializes a new instance of the MyComponent1 class.
        public ClothoidComponent()
          : base("Clothoid", "CL",
              "Component that generates a Clothoid curve with the final Radius, the length and the number of points as inputs.",
              "Workshop", "Curve")
        {
        }

        /// Registers all the input parameters for this component.
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddNumberParameter("Length", "LEN", "Total length of the Clothoid curve.", GH_ParamAccess.item);
            pManager.AddIntegerParameter("Nº Divisions", "ND", "Number of divisions of the Clothoid curve.", GH_ParamAccess.item);
            pManager.AddNumberParameter("Radius", "RAD", "Final radius of the Clothoid curve.", GH_ParamAccess.item, 3);

        }

        /// Registers all the output parameters for this component.
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddTextParameter("Info", "VO", "Component run verbose output", GH_ParamAccess.item);
            pManager.AddPointParameter("Points", "CP", "The points that conform the Clothoid.", GH_ParamAccess.list);
            pManager.AddCurveParameter("Curve", "CV", "Curve that interpolates the Clothoid points.", GH_ParamAccess.item);
        }

        /// This is the method that actually does the work.
        protected override void SolveInstance(IGH_DataAccess DA)
        {
			// Assign input values to the variables
            double L = double.NaN;
            DA.GetData(0, ref L);
            int numPts = 3;
            DA.GetData(1, ref numPts);
            double difl = L / numPts;
            double R = double.NaN;
            double l = double.NaN;
            DA.GetData(2, ref R);
            List<Point3d> points = new List<Point3d>();
            

            
			// For each point in points
            for (int i = 0; i < numPts + 1; i++)
            {
				
                l = difl * i;
                // Calculate the coordinates of the next point and add these coordinates to the points Array
                double x = l - (Math.Pow(l, 3) / (40 * R * R)) + (Math.Pow(l, 5) / (3456 * Math.Pow(R, 4)));
                double y = (Math.Pow(l, 2) / (6 * R)) - (Math.Pow(l, 4) / (336 * Math.Pow(R, 3))) + (Math.Pow(l, 6) / (42240 * Math.Pow(R, 5)));
                points.Add(new Point3d(x, y, 0));


            }

			// Create an interpolated curve with the points Array
            Curve oCrv = Curve.CreateInterpolatedCurve(points, 3);

			// Prepare the output information and values
            DA.SetData(0, $"The distance between points is {difl}u. " +
                $"\nThe length discrepancy is {L - oCrv.GetLength()}u.");
            DA.SetDataList(1, points);
            DA.SetData(2, oCrv);


        }

        /// Provides an Icon for the component.
        protected override System.Drawing.Bitmap Icon => Properties.Resources.clothoid;
        /// Gets the unique ID for this component. Do not change this ID after release.
        public override Guid ComponentGuid
        {
            get { return new Guid("9218B9E6-E56A-49EC-83C0-5E42C016C90B"); }
        }
    }
}