using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhysicsEngine
{
    public class Force
    {
        private Vector2 velocity;
        public bool complete = false;
        
        public Force(Vector2 velocity)
        {
            this.velocity = velocity;
        }

        public virtual Vector2 getForce()
        {
            return velocity;
        }

        public void finish()
        {
            complete = true;
        }
    }
}
