#region Using Statements
using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio; //Directiva using para efectos de sonido
using Microsoft.Xna.Framework.Media; //Dirusing Microsoft.Xna.Framework.GamerServices


/// <summary>
/// /Esto es un Algoritmo de propiedad de Emanuel Franco Amerise sin fin lucrativo, solo educativo
/// </summary>
#endregion
namespace AlgoRobot
{


	/// <summary>
    /// This is the main type for your game
    /// </summary>

  public class Game1 : Microsoft.Xna.Framework.Game
	{

	    GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;
	
		//Menu:
		MainMenu menu;
		SoundEffect [] smenu= new SoundEffect[3];

		//Enemigo:
		Texture2D [] texen= new Texture2D[3];
		SoundEffect shoten;

		//Player:
		Texture2D [] texplay= new Texture2D[7];
		SoundEffect [] splay= new SoundEffect[2];
		SoundEffect [] strans= new SoundEffect[2];
		//Boss:
		Texture2D [] texboss = new Texture2D[9];
		SoundEffect shotboss;

		//Explosion:
		Texture2D [] texp = new Texture2D[2];
		SoundEffect [] soexp= new SoundEffect[2];

		//Impacto:
		SoundEffect [] simpac= new SoundEffect[3];

		//Placa:
		Texture2D placa;

		//Musica:
		Song  [] vecmusic = new Song[3];

		//Pantalla:

		Pantallas pant;
		//Textura de las pantallas
		Texture2D [] tpan = new Texture2D[4];
 		

		// salud de los enemigos
		int [] vitalen= new int[4];
		//salud de los boss
		int [] vitalboss= new int[4];
        //daño de los boss
		int [] bossdamage= new int[4];
		// cantidad de enemigos por ventilacion
		int [] cantenem= new int[4];

		bool guardo = false;


		public int  i=0,nump=0;

		SpriteFont fuente1;


		public Game1()
			: base()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "../../Content";
		}
		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize()
		{
			// TODO: Add your initialization logic here

			this.graphics.PreferredBackBufferWidth = 800;
			this.graphics.PreferredBackBufferHeight = 600;
			this.graphics.IsFullScreen = false;


		        
			menu = new MainMenu ();

	
			base.Initialize();
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch(GraphicsDevice);
			//Inicializando las pantallas
			pant = new Pantallas ();


			menu.panux = new Pantallas ();

			menu.panux2 = new Pantallas ();

			for (i=0 ; i < pant.pan.Length ; i++)
				pant.pan  [i]  = new Pantalla (); 



			for (i=0 ; i < menu.panux.pan.Length ; i++)
				menu.panux.pan  [i]  = new Pantalla ();


			for (i=0 ; i < menu.panux2.pan.Length ; i++)
				menu.panux2.pan  [i]  = new Pantalla ();

			for (i=0 ; i < vitalen.Length ; i++)
			{
				vitalen [i] = 15 + (i*5);

			}

			for (i=0 ; i < vitalboss.Length ; i++)
			{
				vitalboss [i] = 150 + (i*25);

			}

			for (i=0 ; i < bossdamage.Length ; i++)
			{

				bossdamage[i] = 25+(i*25);
			}

			for (i=0 ; i < cantenem.Length ; i++)
			{
				cantenem [i] = 10 + (i*5);

			}

			//fuente:
			fuente1 =  Content.Load<SpriteFont> ("MyFont");

			//Menu

			menu.LoadContent(Content,new Size(800,600));

			//sonido
			smenu [0] = Content.Load<SoundEffect> ("Media/Sonidos/menu/menu00");
			smenu [1] = Content.Load<SoundEffect> ("Media/Sonidos/menu/menu01");
			smenu [2] = Content.Load<SoundEffect> ("Media/Sonidos/menu/menu02");

			//Enemigo:
			//text
			texen [0] = Content.Load<Texture2D> ("Texturas/Enemy/bad00");
			texen [1] = Content.Load<Texture2D> ("Texturas/Enemy/bad01");
			texen [2] = Content.Load<Texture2D> ("Texturas/Enemy/bad02");
			//sonido
			shoten = Content.Load<SoundEffect> ("Media/Sonidos/enemy/bad00");


			//Player:
			//text
			texplay [0] = Content.Load<Texture2D> ("Texturas/Player/player00");
			texplay [1] = Content.Load<Texture2D> ("Texturas/Player/player01");
			texplay [2] = Content.Load<Texture2D> ("Texturas/Player/player02");
			texplay [3] = Content.Load<Texture2D> ("Texturas/Player/player03");
			texplay [4] = Content.Load<Texture2D> ("Texturas/Player/player04");
			texplay [5] = Content.Load<Texture2D> ("Texturas/Player/player05");
			texplay [6] = Content.Load<Texture2D> ("Texturas/Player/player06");
			//sonido
			splay [0] = Content.Load<SoundEffect> ("Media/Sonidos/player/play00");
			splay [1] = Content.Load<SoundEffect> ("Media/Sonidos/player/play01");

			strans [0] = Content.Load<SoundEffect>("Media/Sonidos/player/play02");
			strans [1] = Content.Load<SoundEffect>("Media/Sonidos/player/play03");


			//Boss:

			//textura
			texboss [0] = Content.Load<Texture2D> ("Texturas/Boss/boss00");
			texboss [1] = Content.Load<Texture2D> ("Texturas/Boss/boss00cut");
			texboss [2] = Content.Load<Texture2D> ("Texturas/Boss/boss01");
			texboss [3] = Content.Load<Texture2D> ("Texturas/Boss/boss01cut");
			texboss [4] = Content.Load<Texture2D> ("Texturas/Boss/boss02");
			texboss [5] = Content.Load<Texture2D> ("Texturas/Boss/boss02cut");
			texboss [6] = Content.Load<Texture2D> ("Texturas/Boss/boss03");
			texboss [7] = Content.Load<Texture2D> ("Texturas/Boss/boss03cut");
			texboss [8] = Content.Load<Texture2D> ("Texturas/Boss/boss04");
		
		    //sonido
			shotboss= Content.Load<SoundEffect> ("Media/Sonidos/boss/boss00");

			//Explosion 
			//textura
			texp [0] = Content.Load<Texture2D> ("Texturas/AnimacionExplo/explosion00");
			texp [1] = Content.Load<Texture2D> ("Texturas/AnimacionExplo/explosion01");

			//sonido
			soexp [0] = Content.Load<SoundEffect> ("Media/Sonidos/explosion/explo00");
			soexp [1] = Content.Load<SoundEffect> ("Media/Sonidos/explosion/explo01");
			//Impacto:
			//sonido
			simpac [0] = Content.Load<SoundEffect> ("Media/Sonidos/impacto/impact00");
			simpac [1] = Content.Load<SoundEffect> ("Media/Sonidos/impacto/impact01");
			simpac [2] = Content.Load<SoundEffect> ("Media/Sonidos/impacto/impact02");


			//Placa
			placa= Content.Load<Texture2D> ("Texturas/placa/placa00");

			//Musica

			vecmusic [0] = Content.Load<Song> ("Media/Musica/mus00.wav");
			vecmusic [1] = Content.Load<Song> ("Media/Musica/mus01.wav");
			vecmusic [2] = Content.Load<Song> ("Media/Musica/mus02.wav");


			//Pantallas:

			tpan [0] = Content.Load<Texture2D> ("Texturas/Pantallas/pantalla00");
			tpan [1] = Content.Load<Texture2D> ("Texturas/Pantallas/pantalla01");
			tpan [2] = Content.Load<Texture2D> ("Texturas/Pantallas/pantalla02");
			tpan [3] = Content.Load<Texture2D> ("Texturas/Pantallas/pantalla03");

			//Creando las 4 pantallas:
		

			menu.panux.pan[0].Initialize (texplay,texen,texboss[0],texboss[1],texboss[8],tpan[0],texp,placa,splay,shoten,shotboss,strans,soexp,simpac,cantenem[0],vitalboss[0],vitalen [0],1, bossdamage[0],vecmusic,fuente1);
			menu.panux.pan[1].Initialize (texplay,texen,texboss[2],texboss[3],texboss[8],tpan[1],texp,placa,splay,shoten,shotboss,strans,soexp,simpac,cantenem[1],vitalboss[1],vitalen [1],2, bossdamage[0],vecmusic,fuente1);
			menu.panux.pan[2].Initialize (texplay,texen,texboss[4],texboss[5],texboss[8],tpan[2],texp,placa,splay,shoten,shotboss,strans,soexp,simpac,cantenem[2],vitalboss[2],vitalen [2],3, bossdamage[0],vecmusic,fuente1);
			menu.panux.pan[3].Initialize (texplay,texen,texboss[6],texboss[7],texboss[8],tpan[3],texp,placa,splay,shoten,shotboss,strans,soexp,simpac,cantenem[3],vitalboss[3],vitalen [3],4, bossdamage[0],vecmusic,fuente1);


			menu.panux2.pan[0].Initialize (texplay,texen,texboss[0],texboss[1],texboss[8],tpan[0],texp,placa,splay,shoten,shotboss,strans,soexp,simpac,cantenem[0],vitalboss[0],vitalen [0],1, bossdamage[0],vecmusic,fuente1);
			menu.panux2.pan[1].Initialize (texplay,texen,texboss[2],texboss[3],texboss[8],tpan[1],texp,placa,splay,shoten,shotboss,strans,soexp,simpac,cantenem[1],vitalboss[1],vitalen [1],2, bossdamage[0],vecmusic,fuente1);
			menu.panux2.pan[2].Initialize (texplay,texen,texboss[4],texboss[5],texboss[8],tpan[2],texp,placa,splay,shoten,shotboss,strans,soexp,simpac,cantenem[2],vitalboss[2],vitalen [2],3, bossdamage[0],vecmusic,fuente1);
			menu.panux2.pan[3].Initialize (texplay,texen,texboss[6],texboss[7],texboss[8],tpan[3],texp,placa,splay,shoten,shotboss,strans,soexp,simpac,cantenem[3],vitalboss[3],vitalen [3],4, bossdamage[0],vecmusic,fuente1);


			pant.pan[0].Initialize (texplay,texen,texboss[0],texboss[1],texboss[8],tpan[0],texp,placa,splay,shoten,shotboss,strans,soexp,simpac,cantenem[0],vitalboss[0],vitalen [0],1, bossdamage[0],vecmusic,fuente1);
			pant.pan[1].Initialize (texplay,texen,texboss[2],texboss[3],texboss[8],tpan[1],texp,placa,splay,shoten,shotboss,strans,soexp,simpac,cantenem[1],vitalboss[1],vitalen [1],2, bossdamage[0],vecmusic,fuente1);
			pant.pan[2].Initialize (texplay,texen,texboss[4],texboss[5],texboss[8],tpan[2],texp,placa,splay,shoten,shotboss,strans,soexp,simpac,cantenem[2],vitalboss[2],vitalen [2],3, bossdamage[0],vecmusic,fuente1);
			pant.pan[3].Initialize (texplay,texen,texboss[6],texboss[7],texboss[8],tpan[3],texp,placa,splay,shoten,shotboss,strans,soexp,simpac,cantenem[3],vitalboss[3],vitalen [3],4, bossdamage[0],vecmusic,fuente1);

			
		
			// TODO: use this.Content to load your game content here
		
		}
		/// <summary>
		/// UnloadContent will be called once per game and is the place to unload
		/// all content.
		/// </summary>
		
		protected override void UnloadContent()
		{
			// TODO: Unload any non ContentManager content here
		}

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update(GameTime gameTime)
		{
			// Allows the game to exit

				
			
			//Actualizando el menu
			menu.Update (ref pant,gameTime,vecmusic[0],smenu);

		
			if ( menu.salir==true ) 
			 {  
				 this.Exit(); 
				if (!guardo) 
				{  
					menu.archtxts.Cerrar ();
					guardo = true;
				}
				
			} 


			// TODO: Add your update logic here
		   base.Update(gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear (Color.Transparent);
			// TODO: Add your drawing code here
			spriteBatch.Begin ();
			//Dibujando el Menu
			menu.Draw (spriteBatch, pant);
		
			spriteBatch.End();

			base.Draw(gameTime);
		}
	}

}