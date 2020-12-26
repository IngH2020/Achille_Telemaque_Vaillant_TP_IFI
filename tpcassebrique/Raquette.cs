using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace tpcassebrique
{
    public class Raquette : Microsoft.Xna.Framework.DrawableGameComponent
    {
        private int maxX, minX;
        private int maxY, minY;
        private int TAILLEX, TAILLEY;
        private Vector2 position_depart;
        private int VITESSE_RAQUETTE = 10;
        private SpriteBatch spriteBatch;
        private BoundingBox bbox;
        private Balle balle;
        private Objet uneraquette;
        private string joueur;

        public Raquette(Game game, int th, int tv)
            : base(game)
        {

            maxX = th;
            maxY = tv;
            this.Game.Components.Add(this);

        }
        
        public override void Initialize()
        {
            minX = 0;
            TAILLEX = 110;
            TAILLEY = 18;
            this.position_depart = new Vector2((maxX - TAILLEX) / 2, maxY - TAILLEY - 20);

            base.Initialize();
        }
 
        protected override void LoadContent()
        {

            spriteBatch = new SpriteBatch(GraphicsDevice);
            uneraquette = new Objet(Game.Content.Load<Texture2D>("raquette11"), this.position_depart, new Vector2(TAILLEX, TAILLEY), new Vector2(VITESSE_RAQUETTE, 0));


            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(uneraquette.Texture, uneraquette.Position, Color.Azure);
            spriteBatch.End();
            base.Draw(gameTime);
        }



        public override void Update(GameTime gameTime)
        {
            bbox = new BoundingBox(new Vector3(uneraquette.Position.X, uneraquette.Position.Y, 0),
                new Vector3(uneraquette.Position.X + uneraquette.Texture.Width, uneraquette.Position.Y + uneraquette.Texture.Height, 0));
            if (Menu.CheckActionDroite())
            {
                if (!Element2D.testCollision(this, this.Balle.Bbox))
                {
                
                    if (uneraquette.Position.X + uneraquette.Texture.Width < maxX)
                    {
                 
                        float tempo = uneraquette.Position.X;
                        tempo += uneraquette.Vitesse.X;
                        Vector2 pos = new Vector2(tempo, uneraquette.Position.Y);
                        uneraquette.Position = pos;
                    }

                }
                else Console.WriteLine("CheckActionDown (joueur" + joueur + ") --> collision ");
            }
            else if (Menu.CheckActionGauche())
            {
                if (!Element2D.testCollision(this, this.Balle.Bbox))
                {
                
                    if (uneraquette.Position.X > minX)
                    {
                        float tempo = uneraquette.Position.X;
                        tempo -= uneraquette.Vitesse.X;
                        Vector2 pos = new Vector2(tempo, uneraquette.Position.Y);
                        uneraquette.Position = pos;
                    }


                }
                else
                    Console.WriteLine("CheckActionUp (joueur" + joueur + ") --> collision ");
            }

            base.Update(gameTime);
        }


        public BoundingBox Bbox
        {
            get { return bbox; }
            set { bbox = value; }
        }

        public Balle Balle
        {
            get { return balle; }
            set { balle = value; }
        }

        public Objet Uneraquette
        {
            get { return uneraquette; }
            set { uneraquette = value; }
        }

        public int TailleX
        {
            get { return TAILLEX; }
            set { TAILLEX = value; }
        }

        public int TailleY
        {
            get { return TAILLEY; }
            set { TAILLEY = value; }
        }
    }
}