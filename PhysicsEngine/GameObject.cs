using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace PhysicsEngine
{
    public class GameObject
    {
        Texture2D texture;

        public Vector2 position;
        public Vector2 velocity;
        public Vector2 netForce;

        public bool active;
        public float elasticity = 0.1f;

        private int width;
        private int height;

        public float rotation;
        public float mass;

        private SquareMesh mesh;
        private Rectangle bounds;

        public GameObject(Vector2 position, float rotation, int width, int height, bool active)
        {
            this.position = position;
            this.rotation = MathF.PI /180 * rotation; // Converts rotation from degrees to radians

            this.width = width;
            this.height = height;

            this.active = active;

            mesh = new SquareMesh(this.width, this.height, this.position, this.rotation, this.active);
            
            bounds.Width = this.width;
            bounds.Height = this.height;
        }

        public void MeshUpdate()
        {
            bounds.Location = new Point((int) position.X - width/2, (int) position.Y - height/2); // Losing information about location, will need to change later
            
            mesh.Update(Main.dt);

            this.position = mesh.getPosition();
            this.rotation = mesh.getRotation();
        }

        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("square");

            mesh.LoadContent(content);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, bounds, null, Color.White, rotation, new Vector2(0, 0), 0, 0);
            //_spriteBatch.Draw(texture, bounds, Color.White);

            mesh.Draw(spriteBatch);
        }

        public void CollisionUpdate(ArrayList gameObjects)
        {
            foreach (GameObject gameObject in gameObjects)
            {
                if (!gameObject.Equals(this) && this.active == true)
                {
                    mesh.CollisionSolver(gameObject.getMesh());

                    // Fix sinking bug - Then aabb collision should be mostly complete
                    // Thinking about doing it as a separate normal force that constricts movement more rigorously idk

                    // Instead: nodes will report collisions to the mesh which will calculate the forces that need to be applied and do them
                    // Nodes will not have their own position or velocity, but instead will be used to determine what kind of forces the object will undergo
                }
            }
        }

        public void AddGlobalForce(Force force)
        {
            mesh.AddForce(force);
        }

        public SquareMesh getMesh()
        {
            return mesh;
        }
    }
}
