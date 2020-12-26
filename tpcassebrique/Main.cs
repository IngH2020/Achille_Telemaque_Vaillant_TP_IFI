using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace tpcassebrique
{
    public class Main : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Objet briquebleue;
        Objet briquegrise;
        Objet unebriquenoire;
        Objet briqueorange;
        Objet briquerouge;
        Objet briqueviolet;

        private SpriteFont textFont;
        private int TAILLEBRIQUEX;
        private int TAILLEBRIQUEY;
        private int NBBRIQUES = 8;
        private int NBLIGNES = 5;

        private Balle uneballe;
        private Raquette raquette;
        private int TAILLEH;
        private int TAILLEV;
        private Brique[,] mesBriques;
        private Texture2D fond;
        private static Boolean aGagne = false;

        public Main()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

   
        protected override void Initialize()
        {
            int offsetX = 40;
            int offsetY = 40;
            TAILLEH = 1300; 
            TAILLEV = 560; 
            TAILLEBRIQUEX = 150;
            TAILLEBRIQUEY = 50; 

            
            uneballe = new Balle(this, TAILLEH, TAILLEV);
            raquette = new Raquette(this, TAILLEH, TAILLEV);
            uneballe.Raquette = raquette;
            raquette.Balle = uneballe;
            mesBriques = new Brique[NBLIGNES, NBBRIQUES];
            
            int xpos, ypos;
            for (int x = 0; x < NBLIGNES; x++)
            {
                ypos = offsetY + x * TAILLEBRIQUEY;
                for (int y = 0; y < NBBRIQUES; y++)
                {
                    xpos = offsetX + y * TAILLEBRIQUEX;

                    Vector2 pos = new Vector2(xpos, ypos);
                    
                    mesBriques[x, y] = new Brique(this, pos, new Vector2(TAILLEBRIQUEX, TAILLEBRIQUEY));
                }
            }

            uneballe.MesBriquesballe = mesBriques;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            graphics.PreferredBackBufferWidth = TAILLEH;
            graphics.PreferredBackBufferHeight = TAILLEV;
            graphics.ApplyChanges();

            briquegrise = new Objet(Content.Load<Texture2D>("brique01"), new Vector2(0f, 0f), new Vector2(TAILLEBRIQUEX, TAILLEBRIQUEY), Vector2.Zero);
            briquebleue = new Objet(Content.Load<Texture2D>("brique02"), new Vector2(0f, 0f), new Vector2(TAILLEBRIQUEX, TAILLEBRIQUEY), Vector2.Zero);
            briqueorange = new Objet(Content.Load<Texture2D>("brique03"), new Vector2(0f, 0f), new Vector2(TAILLEBRIQUEX, TAILLEBRIQUEY), Vector2.Zero);
            briquerouge = new Objet(Content.Load<Texture2D>("brique04"), new Vector2(0f, 0f), new Vector2(TAILLEBRIQUEX, TAILLEBRIQUEY), Vector2.Zero);
            briqueviolet = new Objet(Content.Load<Texture2D>("brique02"), new Vector2(0f, 0f), new Vector2(TAILLEBRIQUEX, TAILLEBRIQUEY), Vector2.Zero);
            unebriquenoire = new Objet(Content.Load<Texture2D>("briquetransparente"), new Vector2(0f, 0f), new Vector2(TAILLEBRIQUEX, TAILLEBRIQUEY), Vector2.Zero);

            fond = Content.Load<Texture2D>("back13");

            this.textFont = Content.Load<SpriteFont>("font");

        }

        protected override void UnloadContent()
        {
            
        }

        protected override void Update(GameTime gameTime)
        {
           
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

                 base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            Vector2 pos;
            GraphicsDevice.Clear(Color.Black);
            drawBgMotif(fond);
            spriteBatch.Begin();
            spriteBatch.DrawString(this.textFont, "SCORE : " + uneballe.Score, new Vector2(90, 10), Color.Yellow);
            spriteBatch.DrawString(this.textFont, "VIE : " + uneballe.Nombreballes, new Vector2(1150, 10), Color.Green);
            drawPartie();
            if (uneballe.Nombreballes == 0)
            {
                if (!aGagne)
                    spriteBatch.DrawString(this.textFont, "Game Over ... ! Vous avez epuise les 3 vies. Votre score est :" + uneballe.Score, new Vector2(400, 460), Color.Red);
                redemarrage();

            }
            for (int x = 0; x < NBLIGNES; x++)
            {
                for (int y = 0; y < NBBRIQUES; y++)
                {

                    pos = mesBriques[x, y].Position;
                    if (!mesBriques[x, y].Marque)
                        switch (x)
                        {
                           
                            case 1: spriteBatch.Draw(briquegrise.Texture, pos, Color.Gray); break;
                            case 2: spriteBatch.Draw(briquerouge.Texture, pos, Color.Red); break;
                            case 3: spriteBatch.Draw(briqueorange.Texture, pos, Color.Orange); break;
                            case 4: spriteBatch.Draw(briqueviolet.Texture, pos, Color.Violet); break;
                        }
                    else
                    {
                        spriteBatch.Draw(unebriquenoire.Texture, pos, Color.Black);
                    }



                }
            }
            if (uneballe.Compteur == NBLIGNES * NBBRIQUES)
            {


                spriteBatch.DrawString(this.textFont, "Felicitation, Vous avez gagne ! Votre score est :" + uneballe.Score, new Vector2(13 * 20, 18 * 20), Color.Blue);
                aGagne = true;
                redemarrage();
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
        private void drawBgMotif(Texture2D motif)
        {
            int nbMotifsLargeur = this.GraphicsDevice.Viewport.Width / motif.Width + 1;
            Console.WriteLine("nbMotifsLargeur : " + nbMotifsLargeur);
            int nbMotifsHauteur = this.GraphicsDevice.Viewport.Height / motif.Height + 1;
            Console.WriteLine("nbMotifsLargeur : " + nbMotifsLargeur);

            spriteBatch.Begin();
            for (int i = 0; i < nbMotifsLargeur; i++)
            {
                for (int j = 0; j < nbMotifsHauteur; j++)
                    spriteBatch.Draw(this.fond,
                                    new Vector2(i * motif.Width, j * motif.Height),
                                    Color.Azure);
            }
            spriteBatch.End();
        }
        private void drawPartie()
        {
            if (!uneballe.EstDemarre)
            {
                spriteBatch.DrawString(this.textFont, "Appuyez sur Espace pour commencer Jouer", new Vector2(500, 480), Color.White);
            }
        }

        private void redemarrage()
        {
            uneballe.EstDemarre = false;
            KeyboardState keyboard = Keyboard.GetState();
            if (keyboard.IsKeyDown(Keys.Space))
            {
                uneballe.Compteur = 0;
                uneballe.Score = 0;
                uneballe.Nombreballes = 3;
                raquette.Uneraquette.Texture = this.Content.Load<Texture2D>("raquette11");
                raquette.TailleX = 90;
                uneballe.EstDemarre = false;
                uneballe.Uneballe.Position = uneballe.PositionDep;
                uneballe.Uneballe.Vitesse = Vector2.Zero;
                uneballe.EstDemarre = false;
                for (int x = 0; x < NBLIGNES; x++)
                {
                    for (int y = 0; y < NBBRIQUES; y++)
                    {
                        mesBriques[x, y].Marque = false;
                    }
                }
            }
        }

        public static Boolean Agagne
        {
            get { return aGagne; }
            set { aGagne = value; }
        }
    }
}