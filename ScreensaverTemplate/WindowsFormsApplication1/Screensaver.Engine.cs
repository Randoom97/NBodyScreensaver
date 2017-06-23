using System;
using System.Collections.Generic;
using System.Drawing;

namespace WindowsFormsApplication1
{
    public partial class Screensaver
    {
        //Determines the target frames per second
        private int fps = 60;
        // Determines the size of the internal bitmap to render to.
        // 0.5 would be half the size of the actual window.
        private double internalScale = 1.0;

        //The width and height of the internal bitmap
        private int width;
        private int height;

        private Particle[] particles;
        private Random rand;
        private double gconstant;

        private double ptopf, stopf, stosf;

        /// <summary>
        /// Allows you to add fields to the settings form.
        /// You must first set the registry sub key.
        /// I recomend the name of the screensaver.
        /// </summary>
        public static void SetFields()
        {

        }

        /// <summary>
        /// Allows you to set up any needed variables before any rendering is done.
        /// </summary>
        private void Setup()
        {
            gconstant = 2.0;
            ptopf = (gconstant * Math.Pow(Particle.particleMass, 2));
            stopf = (gconstant * Particle.particleMass * Particle.singularityMass);
            stosf = (gconstant * Math.Pow(Particle.singularityMass, 2));
            width = image.Width;
            height = image.Height;

            rand = new Random();
            particles = new Particle[100];
            for(int i = 0; i < 3; i++)
            {
                particles[i] = new Particle(rand.NextDouble() * width, rand.NextDouble() * height, true);
            }
            for(int i = 3; i < 100; i++)
            {
                particles[i] = new Particle(rand.NextDouble() * width, rand.NextDouble() * height, false);
            }
        }

        /// <summary>
        /// Allows you to update any needed variables before any rendering is done.
        /// Happens roughly at the same speed as fps.
        /// </summary>
        private void Tick()
        {
            gravity(particles);
            acceleration(particles);
        }

        /// <summary>
        /// Draws things to an underlying bitmap that will then get rendered to the screen.
        /// </summary>
        /// <param name="g">Graphics object for drawing</param>
        /// <param name="width">Width of the drawing space</param>
        /// <param name="height">Height of the drawing space</param>
        private void Render(Graphics g, int width, int height)
        {
            g.FillRectangle(Brushes.Black, 0, 0, width, height);
            SolidBrush brush = new SolidBrush(Color.FromArgb(255, 255, 0, 255));
            foreach(Particle p in particles){
                if (p.singularity)
                {
                    g.FillEllipse(brush, (int)p.x, (int)p.y, 20, 20);
                }
                else
                {
                    g.FillEllipse(brush, (int)p.x, (int)p.y, 10, 10);
                }
            }
        }

        private void gravity(Particle[] particles)
        {
            double force = 0;
            for(int i = 0; i < particles.Length-1; i++){
                for(int j = i+1; j < particles.Length; j++)
                {
                    if(particles[i].singularity & particles[j].singularity)
                    {
                        force = stosf / distanceSquared(particles[i].x, particles[i].y, particles[j].x, particles[j].y);
                    }
                    else if(!particles[i].singularity && !particles[j].singularity)
                    {
                        force = ptopf / distanceSquared(particles[i].x, particles[i].y, particles[j].x, particles[j].y);
                    }
                    else
                    {
                        force = stopf / distanceSquared(particles[i].x, particles[i].y, particles[j].x, particles[j].y);
                    }
                    particles[i].vx += force * ((particles[j].x - particles[i].x) / width) / particles[i].mass;
                    particles[i].vy += force * ((particles[j].y - particles[i].y) / height) / particles[i].mass;
                    particles[j].vx += force * ((particles[i].x - particles[j].x) / width) / particles[j].mass;
                    particles[j].vy += force * ((particles[i].y - particles[j].y) / height) / particles[j].mass;
                }
            }
        }

        private void acceleration(Particle[] particles)
        {
            foreach(Particle p in particles)
            {
                p.x += p.vx;
                p.y += p.vy;
            }
        }

        private double distanceSquared(double x1, double y1, double x2, double y2)
        {
            return Math.Max((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2), double.MinValue);
        }
    }
}
