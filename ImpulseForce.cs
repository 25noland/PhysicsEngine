using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhysicsEngine
{
    public class ImpulseForce : Force
    {
        private Vector2 velocity;
        private int impulseTime = 0;

        public ImpulseForce(Vector2 velocity) : base(velocity)
        {
            this.velocity = -velocity * 7.5f; // Why does this constant behave so weirdly?
        }

        public override Vector2 getForce()
        {
            impulseTime++;
            if (impulseTime <= 2)
            {
                return velocity;
            }

            finish();
            return new Vector2(0, 0);
        }
    }
}
