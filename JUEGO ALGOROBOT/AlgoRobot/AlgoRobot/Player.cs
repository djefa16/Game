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
	public class Player
	{


		SoundEffect [] soundshot = new SoundEffect[2];
		SoundEffect  [] soundtrans= new SoundEffect[2];
		SoundEffect	sounddisp, soundexp;
		Texture2D [] vectext= new Texture2D[7];// 1 es spritenormal,2 transformado ,3 es el spritesheet de la animacion,4 bala, 6 misil,7 sombra,8 powerup
		 Vector2 posombra= Vector2.Zero;
		public Rectangle rectangulop;
		private int imageWidth, imageHeigth, velocidad,elapsedtime,timetrans,i=0;
		public int pdamage, vida,vitalidad;
		public Color colorplay;
		public Colision1 choqueplayen,choqueplaypow,choqueplayboss;
		public Animacion animtrans,explosion;
		public bool actshot,impact,actbon,trans=false,disparo,muerto,acttra=false,nosonexp=false;
		Vector2 posanim= Vector2.Zero;
		ulong cant;
		bool desactt=false, actboss;
		//KeyboardState oldState;

		public void Initialize(Texture2D [] vecp, SoundEffect [] soushot,SoundEffect []strans,
		                       Texture2D tboss,Texture2D tbosscut, SoundEffect soexp, Texture2D texp)
		{

			vida = 3;
			vitalidad = 280;
			timetrans =30000;
			colorplay = Color.White;
			velocidad = 8;

			pdamage = 2;
			elapsedtime = 0;
			soundexp = soexp;
			//oldState = Keyboard.GetState ();
			muerto = false;
			//Instanciando objetos 
			choqueplayboss = new Colision1 ();
			choqueplaypow = new Colision1 ();
			choqueplayen = new Colision1 ();
			animtrans = new Animacion ();
			explosion = new Animacion ();
			// agregando vector de texturas2D
			for ( i=0 ; i < vecp.Length ; i++ )
				vectext[i] = vecp [i];
			// posiciones: 0 es spritenormal,1 transformado ,2 animacion,3 bala, 4 misil,5 sombra,6 powerup
		
			//sonidos de bala y misil
			for (i=0; i < soundshot.Length ; i++)
				soundshot [i] = soushot[i] ;
			// Sonidos de transformacion activada y desactivada
			for (i=0; i < soundtrans.Length ; i++)
				soundtrans[i] = strans[i] ;

			sounddisp= soundshot [0];
			imageWidth=vectext [0].Width;
			imageHeigth= vectext[0].Height;
			posombra= new Vector2 (30,560);
			rectangulop= new Rectangle (40, 490, imageWidth, imageHeigth);

			//colision player a enemigo y viga
			choqueplayen.Initialize (vectext [0],vectext[3], vectext[4],vectext[4],1,1);

			//colision player powerup
			choqueplaypow.Initialize (vectext [0], vectext[6], vectext[4],vectext[6], 4,3);

		    // colision player boss
			choqueplayboss.Initialize(tboss,vectext[3],vectext[4],tbosscut,2,3);

			//Animacion transformacion
			animtrans.Initialize (vectext [2],posanim,65,80,9,100,Color.White,1.0f,false,true);

			//Animacion Explosion
			explosion.Initialize(texp,posanim,76, 76, 16, 30, Color.White, 1.0f, false,true);

		}


		public void Update(GameTime gameTime,KeyboardState keyboard, KeyboardState oldState, bool bonus, bool actb)
		{
		

			//KeyboardState keyboard = Keyboard.GetState ();
			actboss = actb;
			disparo = false;

			if (bonus == true)
				vida++;

			if (vitalidad <= 0)     
			{ 
				vida--;   
				if (vida > 0)
				vitalidad = vitalidad + 300 ;
			}

			if (vida > 0) 
			{ 
				if (rectangulop.X < 690)
				  if (keyboard.IsKeyDown (Keys.Right)) 
				  {    
					 rectangulop.X += velocidad;
					 posombra.X += velocidad;

				 }        
				 
			    if (rectangulop.X >= 40)
				   if (keyboard.IsKeyDown (Keys.Left)) 
				{

					 rectangulop.X -= velocidad;
					 posombra.X -= velocidad;

				}        
				


			     // Is the SPACE key down?
				if (keyboard.IsKeyDown (Keys.Space)) 
				{
					// If not down last update, key has just been pressed.
					if (!oldState.IsKeyDown (Keys.Space)) 
					{
						cant++;
						sounddisp.Play ();
						disparo = true;
					}
				} 
				else 
				if (oldState.IsKeyDown (Keys.Space)) 
				{
					disparo = false;
					// Key was down last update, but not down now, so
					// it has just been released.
				}

				// Update saved state.
			   

				if (trans == true) 
				{
					  if (acttra==false)
					{
						soundtrans [0].Play ();
						acttra = true;
					}
					
					animtrans.Update (gameTime, rectangulop);
					sounddisp = soundshot [1];
					pdamage = 10;
					if (elapsedtime < timetrans)
						elapsedtime += (int)gameTime.ElapsedGameTime.TotalMilliseconds;
					else 
					{
						 if (desactt==false)  
						{   
							soundtrans [1].Play ();
							desactt=true;
						}
						animtrans.Update (gameTime, rectangulop);
						sounddisp = soundshot [0];
						pdamage = 2;
						trans = false;
					 
					}
				}
			
				colorplay = Color.White;
			} 
			else
			{
				if (nosonexp==false)
				{   
					soundexp.Play();
					nosonexp = true;
				} 
				explosion.Update(gameTime,rectangulop);
				if (explosion.Active == false)
					muerto = true;

			}


	

		
	}

	  public void Draw(SpriteBatch spriteBatch)
	  {
			if (vida> 0) 
			{
				spriteBatch.Draw (vectext[5], posombra, null, colorplay);
				if (!actboss)
				  choqueplayen.Draw (spriteBatch);
				else
				{ 
					choqueplaypow.Draw (spriteBatch);
				    choqueplayboss.Draw (spriteBatch);
				}
				if (trans == true)
			     {
				      animtrans.Draw (spriteBatch);
				     if (animtrans.Active==false)
				     {    
					    //dibujo el player transformado
					    spriteBatch.Draw (vectext[1], rectangulop, null, colorplay);
				     }	
			    }
			    else 
		          //dibujo el player normal
				     spriteBatch.Draw (vectext[0], rectangulop, null, colorplay);
			}	  
			else
			{    
				explosion.Draw (spriteBatch);
			}	
		
		  
	    }

    }
}		
   




