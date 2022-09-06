using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace PhysicsEngine
{
    public class World
    {
        private ArrayList gameObjects = new ArrayList();

        private Vector2 gravity = new Vector2(0, 9.81f);

        public void AddObject(GameObject obj)
        {
            gameObjects.Add(obj);
            obj.AddGlobalForce(new Force(gravity));
        }

        public void LoadContent(ContentManager content)
        {
            foreach (GameObject obj in gameObjects)
            {
                obj.LoadContent(content);
            }
        }

        public void Update(float dt)
        {
            foreach (GameObject obj in gameObjects)
            {
                MeshSolver(obj);
                CollisionSolver(obj);
                ForceSolver(obj, dt);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            foreach (GameObject obj in gameObjects)
            {
                obj.Draw(spriteBatch);
            }
            spriteBatch.End();
        }

        private void MeshSolver(GameObject obj)
        {
            obj.MeshUpdate();
        }

        private void ForceSolver(GameObject obj, float dt)
        {
            
        }

        private void CollisionSolver(GameObject obj)
        {
            obj.CollisionUpdate(gameObjects);
        }
    }
}
