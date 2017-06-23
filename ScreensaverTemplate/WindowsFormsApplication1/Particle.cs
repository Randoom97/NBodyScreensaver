using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    class Particle
    {

        public static double singularityMass { get { return 1000.0; } }
        public static double particleMass { get { return 1.0; } }

        public double mass { get { if (singularity) { return singularityMass; } return particleMass; } }
        public bool singularity { get; private set; }
        public double x { get; set; }
        public double y { get; set; }
        public double vx { get; set; }
        public double vy { get; set; }

        public Particle(double x, double y, bool singularity)
        {
            this.singularity = singularity;
            this.x = x;
            this.y = y;
        }

        public Particle(double x, double y, double vx, double vy, bool singularity)
        {
            this.singularity = singularity;
            this.x = x;
            this.y = y;
            this.vx = vx;
            this.vy = vy;
        }

    }
}
