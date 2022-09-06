using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhysicsEngine
{
    public class SquareMesh
    {
        private Node[] nodes;

        private Vector2 position;
        private Vector2 velocity;
        private Vector2 netForce;

        private ArrayList forces = new ArrayList();

        private float mass = 1f;

        private int width;
        private int height;

        private Texture2D texture;

        private float rotation;

        private bool active;

        public SquareMesh(int width, int height, Vector2 position, float rotation, bool active)
        {
            nodes = new Node[4];

            this.width = width;
            this.height = height;

            this.position = position;
            this.rotation = rotation;

            this.velocity = new Vector2(0, 0);

            this.active = active;

            /* Node Order:
             * 
             * 0 --- 1
             * |     |
             * |     |
             * 2 --- 3
             */

            // Initialize nodes
            nodes[0] = new Node(this, position, new Vector2(0, 0), new Vector2(width / 2, height / 2));
            nodes[1] = new Node(this, position, new Vector2(0, 0), new Vector2(-width / 2, height / 2));
            nodes[2] = new Node(this, position, new Vector2(0, 0), new Vector2(width / 2, -height / 2));
            nodes[3] = new Node(this, position, new Vector2(0, 0), new Vector2(-width / 2, -height / 2));
        }

        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("point");
        }

        public void Update(float dt)
        {
            ArrayList toRemove = new ArrayList();

            if (active)
            {
                foreach (Force force in forces)
                {
                    if (force.complete == true)
                    {
                        toRemove.Add(force);
                    }
                    netForce += mass * force.getForce();
                }

                velocity += netForce / mass * dt;
                position += velocity * dt;

                netForce = new Vector2(0, 0);
            }

            foreach (Force force in toRemove)
            {
                forces.Remove(force);
            }

            foreach (Node node in nodes)
            {
                node.Update(position, velocity, rotation);
            }
        }

        public void Draw(SpriteBatch _spriteBatch)
        {   
            for (int i = 0; i < nodes.Length; i++)
            {
                _spriteBatch.Draw(texture, new Vector2(nodes[i].getPosition().X - 5, nodes[i].getPosition().Y - 5), Color.White);
            }
        }

        public bool Collision(SquareMesh mesh)
        {
            foreach (Node node in nodes)
            {
                if (node.Collision(mesh))
                {
                    return true;
                }
            }

            return false;
        }

        public void CollisionSolver(SquareMesh mesh)
        {
            foreach (Node node in nodes)
            {
                node.CollisionSolver(mesh);
            }
        }

        public void AddForce(Force force)
        {
            forces.Add(force);
        }

        public Node getNode(int index)
        {
            return nodes[index];
        }

        public Node[] getNodes()
        {
            return nodes;
        }

        public Vector2 getPosition()
        {
            return position;
        }

        public float getRotation()
        {
            return rotation;
        }
    }
}
