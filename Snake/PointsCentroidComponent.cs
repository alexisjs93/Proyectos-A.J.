// 2022
// Own project


using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;

namespace Snake
{
    public class PointsCentroidComponent : GH_Component
    {
        /// Initializes a new instance of the PointsCentroidComponent class.
        public PointsCentroidComponent()
          : base("PointsCentroidComponent", "PCC",
              "Given a list of points the component outputs the centroid and the distance between the centroid and the input points.",
              "Workshop", "Point")
        {
        }
		
        /// Registers all the input parameters for this component.
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddPointParameter("Points", "PTS", "Input the point list of which you want to calculate the centroid point and the distances between the centroid point and each input point", GH_ParamAccess.list);
        }

        /// Registers all the output parameters for this component.
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddPointParameter("Centroid", "CR", "The centroid point of the input point list.", GH_ParamAccess.item);
            pManager.AddNumberParameter("Distances", "DS", "List of distances between the input points and their centroid", GH_ParamAccess.list);
        }

        /// This is the method that actually does the work.
        protected override void SolveInstance(IGH_DataAccess DA)
        {
			// Initialize the variables
            List<Point3d> points = new List<Point3d>();
            DA.GetDataList(0, points);
            Point3d centroid = new Point3d(0,0,0);
            List<double> distances = new List<double>();
			
			// For each point in the point list: add the value to the centroid variable
            foreach (Point3d pt in points)
            {
		
                centroid += pt;

            }

			// Divide the centroid variable by the amount of points
            centroid /= points.Count;

			// Prepare the output variables
            DA.SetData(0, centroid);

            foreach (Point3d pt in points)
            {
                distances.Add(centroid.DistanceTo(pt));

            }
            DA.SetDataList(1, distances);

        }

        /// Provides an Icon for the component.
        protected override System.Drawing.Bitmap Icon => Properties.Resources.Centroid;

        /// Gets the unique ID for this component. Do not change this ID after release.
        public override Guid ComponentGuid
        {
            get { return new Guid("044C15D2-4228-454B-9299-D29CEB251280"); }
        }
    }
}