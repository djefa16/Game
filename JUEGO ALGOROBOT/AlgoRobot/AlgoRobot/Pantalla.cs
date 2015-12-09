#region Using Statements
using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio; //Directiva using para efectos de sonido
using Microsoft.Xna.Framework.Media; //Dirusing Microsoft.Xna.Framework.GamerServices

#endregion




namespace AlgoRobot
{
	public class Pantalla
	{
		int i,elapsedtime,puntbonus;
		Vector2 [] posanimb= new Vector2[4];
		Vector2 [] posienem = new Vector2[4];
		bool [] permitir= new bool[4];
		bool sonomusicp=false, sonomusicb=false;
		Vector2 posfondo= Vector2.Zero;
		Vector2 posfuentevital= new Vector2 (50,10);
		Vector2 posfuentevida = new Vector2 (200,10);
		Vector2 posfuentepunt = new Vector2 (300,10);
		SoundEffect[] soundimp= new SoundEffect[3];
		Song [] musicgame= new Song [3];
		Song musicact;
		public bool finish, gameover,bonus;
		bool callpow= false, entroboss=false ;
		int vitalenemi=0; // vitalidad enemigos (va en el game)
		int [] timeen= new int[4]; // tiempo para que aparezcan los enemigos por primera vez
		int [] limit = new int[4];
		public int cantenem=0;
		public int puntacionpan=0;
		List <PlayBad> listaenem0 = new List<PlayBad> ();
		List <PlayBad> listaenem1 = new List<PlayBad> ();
		List <PlayBad> listaenem2 = new List<PlayBad> ();
		List <PlayBad> listaenem3 = new List<PlayBad> ();
		Boss Jefe;
		public Player play;
		Movimiento Placa;
		SoundEffect shotb;
		Texture2D [] textbad= new Texture2D[4]; 
		Texture2D fondop;
		SpriteFont fuent;
		KeyboardState oldState;

		public void Initialize(Texture2D [] tplay, Texture2D []ten , Texture2D tboss,Texture2D tbosscut,Texture2D tshotboss,Texture2D tpant,Texture2D [] texpl,Texture2D tplaca,
		                       SoundEffect [] splay,SoundEffect shotbad, SoundEffect shotbos,SoundEffect[] strans, SoundEffect[]soexp, 
		                       SoundEffect[]simp,int cenemv, int vitaboss, int vitaenem, int nump, int bdamage, Song[]vmusic,SpriteFont fuente)
		
		
		
		
		{


			// nump es el numero de pantalla puede ser 1,2,3,4...
			fondop = tpant;
			bonus = false;
			finish = false;
			gameover = false;
			fuent = fuente;
			oldState = Keyboard.GetState();
			elapsedtime = 0;
			puntbonus = 0;   
		    cantenem = cenemv *4;

			//Limite de creacion de enemigos

			for (i=0 ; i <  limit.Length ; i++)
			{
				limit [i] = cenemv-1;

			}
			vitalenemi = vitaenem;

			for (i=0 ; i <  timeen.Length ; i++)
			{
				timeen [i] = 8000 * (i+1);

			}
			//cargo las texturas necesarias para el enemigo
			for (i=0 ; i <  ten.Length ; i++)
			{
				textbad [i] = ten [i];

			}
			//cargo los sonidos de impacto
			for (i=0 ; i <  simp.Length ; i++)
			{
				soundimp[i] = simp [i];

			}

			//cargo las canciones del juego
			for (i=0 ; i <  simp.Length ; i++)
			{
				musicgame[i] = vmusic [i];

			}

			//hablilitar que actualizen los enemigos
			for (i=0 ; i <  permitir.Length ; i++)
			{
				permitir[i] = true;

			}

			musicact = musicgame [1];

			//posiciones de animaciones
			posanimb [0] = new Vector2 (78, 85);
			posanimb [1] = new Vector2 (262, 85);
			posanimb [2] = new Vector2 (462, 85);
			posanimb [3] = new Vector2 (640, 85);
			//posiciones de los enemigos en movimiento
			posienem  [0] = new Vector2 (30, 15);
			posienem  [1] = new Vector2 (210, 15);
			posienem  [2] = new Vector2 (410, 15);
			posienem  [3] = new Vector2 (590, 15);

			shotb = shotbad;

		   // Instanciamos los objetos
			Placa = new Movimiento ();
			Jefe = new Boss ();
			play = new Player ();

			//Creo e inicialzo los enemigos 
			for (i=0 ; i< cenemv; i++)
			{
			    listaenem0.Add (new PlayBad ());
				listaenem0 [i].Initialize (vitalenemi,posienem [0],posanimb [0],textbad,shotb,soexp[0],texpl[0]);
				listaenem1.Add (new PlayBad ());
				listaenem1 [i].Initialize (vitalenemi,posienem [1],posanimb [1],textbad,shotb,soexp[0],texpl[0]);
				listaenem2.Add (new PlayBad ());
				listaenem2 [i].Initialize (vitalenemi,posienem [2],posanimb [2],textbad,shotb,soexp[0],texpl[0]);
				listaenem3.Add (new PlayBad ());
				listaenem3 [i].Initialize (vitalenemi,posienem [3],posanimb [3],textbad,shotb,soexp[0],texpl[0]);
			}

			// restando los limites de creacion de enemigos
		
			//Inicializando todo
			play.Initialize(tplay,splay,strans,tboss,tbosscut,soexp[0],texpl[0]);
			Placa.Initialize (tplaca, 670, 450, 124,27,8,2,1f);
			Jefe.Initialize (tboss,tbosscut, tshotboss, vitaboss, bdamage, shotbos,soexp[1],texpl[1]);


		}


		public void Update(GameTime gameTime,ref int totpuntuacion, ref bool pausa)
		{

			 

			  elapsedtime += (int)gameTime.ElapsedGameTime.TotalMilliseconds;
			  bonus = false;


			if (sonomusicp==false)
			{
				MediaPlayer.Play(musicact);
			    MediaPlayer.IsRepeating = true;
			    MediaPlayer.Volume = 0.35f;
				sonomusicp = true;
			}
			if (pausa==false)
				MediaPlayer.Volume = 0.35f;

			KeyboardState keyboard = Keyboard.GetState ();

			if (keyboard.IsKeyDown (Keys.Enter)) 
			{
				// If not down last update, key has just been pressed.
				if (!oldState.IsKeyDown (Keys.Enter)) 
				{

					if (pausa == false)   
					{	
						pausa = true;
						MediaPlayer.Volume = 0.009f;
					}
							
				}

			}

			bonus = false;
			if (puntbonus >= 150)   
			{
				bonus = true;
				puntbonus = puntbonus-150;	
			}

			if (play.muerto == true)
			{
				gameover = true;
			}
			// Entro a la ejecucion del programa solo si no esta en pausa, si no hay gameover y si termino la pantalla
			if (pausa == false && finish == false && gameover == false) 
			{

				play.Update (gameTime,keyboard,oldState,bonus,Jefe.activado);
				Placa.Update ();
			

				if (cantenem  > 0) 
				{

					// secuencia del juego iniciada

						listaenem0 [limit[0]].Update (gameTime);
						play.choqueplayen.Update (play.rectangulop,listaenem0 [limit[0]].badm.rectangulo, Placa.rectangulo, play.disparo,ref play.trans,false, soundimp [0], play.pdamage,ref listaenem0 [limit[0]].vitalidad);
						listaenem0 [limit[0]].choco.Update (listaenem0 [limit[0]].badm.rectangulo, play.rectangulop, Placa.rectangulo, listaenem0 [limit [0]].badm.Activar, ref play.trans,false, soundimp [1], listaenem0 [limit [0]].edamage, ref play.vitalidad);

					    listaenem1 [limit[1]].Update (gameTime);
						play.choqueplayen.Update (play.rectangulop, listaenem1 [limit[1]].badm.rectangulo,Placa.rectangulo, play.disparo,ref play.trans,false, soundimp [0], play.pdamage,ref listaenem1 [limit[1]].vitalidad);
						listaenem1 [limit[1]].choco.Update (listaenem1 [limit[1]].badm.rectangulo, play.rectangulop, Placa.rectangulo, listaenem1 [limit[1]].badm.Activar,ref play.trans,false, soundimp[1], listaenem1 [limit[1]].edamage,ref play.vitalidad);

					 
					    listaenem2 [limit[2]].Update (gameTime);
					    play.choqueplayen.Update (play.rectangulop, listaenem2 [limit[2]].badm.rectangulo,Placa.rectangulo, play.disparo,ref play.trans,false, soundimp [0], play.pdamage,ref listaenem2 [limit[2]].vitalidad);
						listaenem2 [limit[2]].choco.Update (listaenem2 [limit[2]].badm.rectangulo, play.rectangulop, Placa.rectangulo, listaenem2 [limit[2]].badm.Activar,ref play.trans,false, soundimp[1], listaenem2 [limit[2]].edamage,ref play.vitalidad);

                     
					    listaenem3 [limit[3]].Update (gameTime);
						play.choqueplayen.Update (play.rectangulop, listaenem3 [limit[3]].badm.rectangulo,Placa.rectangulo, play.disparo,ref play.trans,false, soundimp [0], play.pdamage,ref listaenem3 [limit[3]].vitalidad);
						listaenem3 [limit[3]].choco.Update (listaenem3 [limit[3]].badm.rectangulo, play.rectangulop, Placa.rectangulo, listaenem3 [limit[3]].badm.Activar,ref play.trans,false, soundimp[1], listaenem3 [limit[3]].edamage,ref play.vitalidad);
					
				
					      
					if (listaenem0 [limit[0]].muerto == true && permitir[0] == true) 
					{
						 cantenem--;
						 puntacionpan = puntacionpan + listaenem0 [limit [0]].valor;
						 puntbonus= puntbonus + listaenem0 [limit [0]].valor;

						if (limit [0] > 0) 
						{
							listaenem0.RemoveAt (limit [0]);
							limit [0]--;

						} 
						else
							permitir [0] = false;

					
					}
					if (listaenem1 [limit [1]].muerto == true  && permitir[1] == true) 
					{
						cantenem--;
						puntacionpan = puntacionpan + listaenem1 [limit [1]].valor;
						puntbonus= puntbonus + listaenem1 [limit [1]].valor;
						if (limit [1] > 0) 
						{
							listaenem1.RemoveAt (limit [1]);
							limit [1]--;
						 
						}
						else
							permitir [1] = false;
					 
					}  
					if (listaenem2 [limit [2]].muerto == true && permitir[2] == true) 
					{
						cantenem--;
						puntacionpan = puntacionpan + listaenem2 [limit [2]].valor;
						puntbonus= puntbonus + listaenem2 [limit [2]].valor;

						if (limit [2] > 0)
						{
							listaenem2.RemoveAt (limit [2]);
							limit [2]--;
						}
						else
							permitir [2] = false;
					
					}  
					if (listaenem3 [limit [3]].muerto == true && permitir[3] == true) 
					{
						cantenem--;
						puntacionpan = puntacionpan + listaenem3 [limit [3]].valor;
						puntbonus= puntbonus + listaenem3 [limit [3]].valor;

						if (limit [3] > 0) 
						{
							listaenem3.RemoveAt (limit [3]);
							limit [3]--;
						}
						else
							permitir [3] = false;
					
					}  

					
				} 
				else  
				{ 
					Jefe.Update (gameTime, ref finish);
				    if (sonomusicb==false) 
					{
						MediaPlayer.Stop ();
						musicact = musicgame [2];
						MediaPlayer.Play(musicact);
					    MediaPlayer.IsRepeating = true;
						MediaPlayer.Volume = 0.50f;
						sonomusicb = true;
					}
				}    
				       
	
				     if (Jefe.activado)
				     {  
				          //colision player a boss y a placa, y boss a player y a placa y player a power up:
					       callpow = true;
					       play.choqueplaypow.Update  (play.rectangulop,Placa.rectangulo, Jefe.rectaboss,callpow,ref play.trans,false,soundimp[1], play.pdamage,ref Jefe.vitalidad);
						//player a boss
					       play.choqueplayboss.Update (play.rectangulop,Jefe.rectaboss,Placa.rectangulo, play.disparo,ref play.trans,false, soundimp [0], play.pdamage,ref Jefe.vitalidad);
				         //boss a player
					       Jefe.chocobosspv.Update    (Jefe.rectaboss,play.rectangulop,Placa.rectangulo,Jefe.atacar,ref play.trans,true,soundimp[1],Jefe.bdamage,ref play.vitalidad);
				      }

				if (Jefe.muerto == true && entroboss==false )
				{
					MediaPlayer.Pause ();
					puntacionpan += Jefe.valor;
					totpuntuacion = totpuntuacion + puntacionpan;
					entroboss = true;
				}
			
			
			}		
			else
				totpuntuacion=  totpuntuacion + puntacionpan;


			oldState = keyboard;
		}
	   

	  public void Draw(SpriteBatch spriteBatch)
	  {

			//Dibujar fondo pantallaPlayer, Enemigos y Boss
	
		   if (finish== false && gameover==false)
		   {  
				spriteBatch.Draw (fondop, posfondo, Color.White);
				spriteBatch.DrawString (fuent, "Vitalidad:  "+play.vitalidad, posfuentevital, Color.GreenYellow);
				spriteBatch.DrawString (fuent, "Vida:  "+play.vida,posfuentevida, Color.GreenYellow);
				spriteBatch.DrawString (fuent, "Puntacion:  "+puntacionpan, posfuentepunt, Color.GreenYellow);
			    play.Draw (spriteBatch);
				Placa.Draw (spriteBatch,Color.White);
				if (cantenem > 0) 
				{
				   listaenem0 [limit [0]].Draw (spriteBatch);
				   listaenem1 [limit [1]].Draw (spriteBatch);
				   listaenem2 [limit [2]].Draw (spriteBatch);
			 	   listaenem3 [limit [3]].Draw (spriteBatch);
			    
				}
				else
					Jefe.Draw (spriteBatch);
			
			
			}
		 
	 
	   }
  }

}

