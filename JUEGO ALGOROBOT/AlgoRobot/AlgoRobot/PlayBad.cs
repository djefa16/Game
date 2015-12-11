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

/// <summary>
///  
///   «Copyright 2015 Emanuel Amerise»
///  
///    This file is part of Algorobot

///       Algorobot is free software: you can redistribute it and/or modify
///	     it under the terms of the GNU General Public License as published by
///		the Free Software Foundation, either version 3 of the License, or
///		(at your option) any later version.
///
///		Algorobot is distributed in the hope that it will be useful,
///		but WITHOUT ANY WARRANTY; without even the implied warranty of
///	    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
///		GNU General Public License for more details.
///
/// 	You should have received a copy of the GNU General Public License
///     along with Algorobot.  If not, see <http://www.gnu.org/licenses/>.
/// 
/// </summary>

namespace AlgoRobot
{
	//Clase para los enemigos
	public class PlayBad
	{

		bool nosono=false;
		int i=0,elapsedtime;
		int timecolor;
		Vector2 posexp=Vector2.Zero ;
		public Animacion badAnimacion; 
		public Animacion explosion;
		public Movimiento badm;
		public Colision1 choco;
		public int vitalidad;
		public bool muerto,remover;
		public int posx;
		public int posy;
		public Color [] coloresb= new Color  [4];
		SoundEffect shot,soundexp;
		public int edamage;
		public int valor;

		public void Initialize(int vitali , Vector2 posbadm,Vector2 badposanim, Texture2D[] vecbtext 
		                       ,SoundEffect shotb,SoundEffect soexp, Texture2D texp)
		
		{
			i = 0;
			timecolor = 15000;
			vitalidad = vitali;
			valor = vitalidad;
			shot = shotb;
			badAnimacion = new Animacion();
			explosion = new Animacion ();
			badm = new Movimiento();
			choco = new Colision1 ();
			muerto = false;
			remover = false;
			soundexp = soexp;
			//colores del enemigo
			coloresb[0]=Color.Turquoise;
			coloresb[1]=Color.HotPink;
			coloresb [2] = Color.BlueViolet;
			coloresb [3]= Color.Red;
			//daño ejercido por enemigo
			edamage = 2;
			posx = (int)posbadm.X;
			posy = (int)posbadm.Y;
			//inicializo todo lo necesario para el enemigo
			badm.Initialize (vecbtext[0],posx,posy, 55, 55, 4,1,0.85f);
			badAnimacion.Initialize (vecbtext[1], badposanim, 110, 110, 6, 300,coloresb[i], 1f, false,false);
			choco.Initialize (vecbtext[0], vecbtext[2],vecbtext[2],vecbtext[2],3,9);
			explosion.Initialize (texp, posexp, 76, 76, 16, 50, Color.White, 1f, false,true);
		
		
		}
	    
	

		public void Update(GameTime gameTime)
		{

			//ejecuto solo si no esta "muerto"

		

			if (remover== false)
			{
			    elapsedtime +=  (int)gameTime.ElapsedGameTime.TotalMilliseconds;

			    badAnimacion.Update(gameTime, badAnimacion.sourceRect);
			   if (badAnimacion.Active== false)   
			   {
				   badm.Update ();
			   }
			   if (badm.Activar == true)
				   shot.Play ();
			   if (elapsedtime > timecolor) 
			   {
				  if (i < (coloresb.Length - 1))
				  {
					  i++;   
					  edamage += 2;
					  valor += 2;
					  vitalidad += 2;
				  }
			    
				 elapsedtime = 0;
			  }

			}
			else
			{
				if (nosono==false)   
				{  
					soundexp.Play ();
					nosono = true;
				}
				explosion.Update (gameTime, badm.rectangulo);
				if (explosion.Active==false)
					  muerto = true;
			}

			if (vitalidad <= 0)
				remover = true;

		
	   }

		public void Draw(SpriteBatch spriteBatch)
	    {

			if (remover == false) 
			{
				badAnimacion.Draw (spriteBatch);
				if (badAnimacion.Active == false)   
				{
					badm.Draw (spriteBatch,coloresb [i]);   
				    choco.Draw (spriteBatch);
			
				}
			} 
			else
				explosion.Draw (spriteBatch);

	    }
    }



}