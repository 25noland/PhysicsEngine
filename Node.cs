using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PhysicsEngine
{
    public class Node
    {
        private Vector2 position;
        private Vector2 velocity;

        private Vector2 offset;
        private float rotatedDegrees;

        private SquareMesh parent;

        public Node(SquareMesh parent, Vector2 position, Vector2 velocity, Vector2 offset)
        {
            this.position = position - offset;
            this.offset = offset;
            this.parent = parent;
        }

        public void Update(Vector2 position, Vector2 velocity, float rotation)
        {
            this.velocity = velocity;

            Vector2 rotated = new Vector2((float) (offset.X * Math.Cos(rotation) - offset.Y * Math.Sin(rotation)), (float) (offset.X * Math.Sin(rotation) + offset.Y * Math.Cos(rotation)));

            this.position =  position + rotated;
        }
        
        public bool Collision(SquareMesh mesh)
        {
            if (position.Y > mesh.getNode(0).getPosition().Y && position.Y < mesh.getNode(2).getPosition().Y && position.X > mesh.getNode(0).getPosition().X && position.X < mesh.getNode(1).getPosition().X)
            {
                return true; // Calculate normal for better prediction
            }
            return false;
        }

        public void CollisionSolver(SquareMesh mesh)
        {
            if (Collision(mesh))
            {
                ImpulseForce force = new ImpulseForce(velocity);
                parent.AddForce(force);
            }
        }

        public Vector2 getPosition()
        {
            return position;
        }
    }
}
