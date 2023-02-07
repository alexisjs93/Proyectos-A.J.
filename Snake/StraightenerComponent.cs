// 2022
// Own project

using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;

namespace Snake
{
    public class StraightenerComponent : GH_Component
    {
        /// Initializes a new instance of the MyComponent2 class.
        public StraightenerComponent()
          : base("Straightener", "ST",
              "Component that stightens a curve. Transforms Z values into Y values.",
              "Workshop", "Curve")
        {
        }

        /// Registers all the input parameters for this component.
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddBooleanParameter("Run", "RUN", "[BOOLEAN] This input takes a boolean value. It allows the component to be stopped. The default value is TRUE", GH_ParamAccess.item, true);
            pManager[0].Optional = true;
            pManager.AddCurveParameter("Curve", "Curve", "[CURVE] Curve that will be straightened.", GH_ParamAccess.item);
            pManager.AddIntegerParameter("No. Divisions", "ND", "[INTEGER] Integer number which defines the amount of divisions. Default number of divisions is 10.", GH_ParamAccess.item, 10);
            pManager[2].Optional = true;
        }

        /// Registers all the output parameters for this component.
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddTextParameter("Info", "VO", "Component run verbose output", GH_ParamAccess.item);
            pManager.AddPointParameter("Points", "SP", "List of points that form the straightened curve.", GH_ParamAccess.list);
            pManager.AddCurveParameter("Polyline", "PL", "Polyline of the straightened points.", GH_ParamAccess.list);

        }

        /// This is the method that actually does the work.
        protected override void SolveInstance(IGH_DataAccess DA)
        {
			// Load input values into variables
            Boolean active = new Boolean();
            DA.GetData(0, ref active);
            Curve curve = null;
            DA.GetData(1, ref curve);
            int numOfDivisions = 10;
            DA.GetData(2, ref numOfDivisions);
            List<Point3d> finalPoints = new List<Point3d>();
            Polyline oPolyline = null;
            string oStr = "Nothing here yet! :-D";

			// If the component has been activated
            if (active)
            {
				// Divide the curve
                Point3d[] originalPoints;
                curve.DivideByCount(numOfDivisions, true, out originalPoints);


                //CREATE THE ORIGIN POINT AT 0,0,0
                finalPoints.Add(new Point3d(0, originalPoints[0][2], 0));

				// Calculate the z, dx, dy, b and add a new point
                for (int i = 1; i < originalPoints.Length; i++)
                {
                    double z = originalPoints[i][2];
                    double dx = (originalPoints[i][0] - originalPoints[i - 1][0]) * (originalPoints[i][0] - originalPoints[i - 1][0]);
                    double dy = (originalPoints[i][1] - originalPoints[i - 1][1]) * (originalPoints[i][1] - originalPoints[i - 1][1]);
                    double b = Math.Sqrt(dx + dy);
                    finalPoints.Add(new Point3d(finalPoints[i - 1][0] + b, z, 0));
                }
				
				// Create a polyline that passes through all the new points
                oPolyline = new Polyline(finalPoints);

                double oCrvLen = curve.GetLength();
                double fPolLen = oPolyline.Length;

                oStr = $"Original curve length {oCrvLen}u and the strightened curve has a length of {fPolLen}u. " +
                       $"\n The length difference is: {fPolLen - oCrvLen}u." +
                       $"\n The length deviation is: {(oCrvLen - fPolLen) * 100 / oCrvLen}%" +
                       $"\n The average distance between points is {fPolLen / numOfDivisions}u.";

            }


			// Prepare output variables
            DA.SetData(0, oStr);
            DA.SetDataList(1, finalPoints);
            DA.SetData(2, oPolyline);
        }

        /// Provides an Icon for the component.
        protected override System.Drawing.Bitmap Icon => Properties.Resources.straightener;

        /// Gets the unique ID for this component. Do not change this ID after release.
        public override Guid ComponentGuid
        {
            get { return new Guid("E835ADCB-1B42-424A-9858-1E0B29DD0B58"); }
        }
    }
}