#region Using Statements
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio; //Directiva using para efectos de sonido
using Microsoft.Xna.Framework.Media; //Dirusing Microsoft.Xna.Framework.GamerServices

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

#endregion

namespace AlgoRobot
{
	public class Movimiento
	{

		int cp=0;
		byte opcion; // opcion es los tipos de movimientos: 1Enemy, 2Viga 
		private int x;
		private int y;
		private int posix;
		private int posiy;
		private int imageWidth;
		private int imageHeigth;
		private int velocidad;
		private int limit;
		private int desp=175;
		public Rectangle rectangulo;
		private Texture2D textura;
		private int i=0;
		public int ind = 0;
		int vuelta;
		public bool Activar;
		public Color [] colores= new Color  [4];
		private int[] vecl= new int[7];

		public void Initialize(Texture2D textura, int x, int y, int imageWidth, int imageHeigth, 
		                       int velocidad, byte opcion,float scale)
		{

			this.opcion = opcion;
			this.textura = textura;

			this.velocidad = velocidad;
			this.x = x;
			this.y = y;
			this.posix = x;
			this.posiy = y;

			rectangulo = new Rectangle(x, y, Convert.ToInt16(scale*imageWidth),Convert.ToInt16(scale*imageHeigth));

           if (opcion==1)
		   {

			  vuelta = 0;
			  limit = desp / velocidad;
			  this.imageHeigth = imageHeigth;
			  this.imageWidth = imageWidth;
			  for ( i=0; i< vecl.Length; i++)
				  vecl [i] = i * limit;

			
			}


		}

		public void Update()
		{

			//Aqui se activan los disparos por esquina del enemigo
			if (opcion == 1 )
			{	
				if (vuelta == vecl [0] || vuelta == vecl [1] || vuelta == vecl [2] || vuelta == vecl [3] || vuelta == vecl [4] ||
				    vuelta == vecl [5] || vuelta==vecl [6])
					Activar = true;
				else
					Activar = false;

				//1 de arriba para abajo
				if (y < ((posiy + (desp - 1)) - imageHeigth) && (vuelta >= vecl [0] && vuelta < vecl [1]))

					y += velocidad;
				else
				//2 de derecha a izquierda
				if (x < ((posix + (desp - 1)) - imageWidth) && (vuelta >= vecl [1] && vuelta < vecl [2]))
					x += velocidad;
				else 
				//3 de abajo para arriba
				if ((y > posiy) && (vuelta >= vecl [2] && vuelta < vecl [3]))

					y -= velocidad;
				else
				//4 de arriba para abajo
				if (y < ((posiy + (desp - 1)) - imageWidth) && (vuelta >= vecl [3] && vuelta < vecl [4]))
					y += velocidad;
				else
				//5 de derecha a izquierda
				if ((x > posix) && (vuelta >= vecl [4] && vuelta < vecl [5]))
					x -= velocidad;
				else
				//6 de abajo hacia arriba
				if ((y > posiy) && (vuelta >= vecl [5] && vuelta < vecl [6]))
					y -= velocidad;

				if (vuelta == vecl [6]) 
				{	 
					vuelta = 0;
					if (velocidad < 6)
						velocidad = velocidad * 2;
					else if (velocidad < 16)
						velocidad = velocidad + 2;
				}
			} 
			else 
			 {
				if ((opcion==2))
				 if (x > (0) && cp == 0)
					x -= (velocidad-4);
				else 
				  if (x < posix) 
				{    
					cp++;	
					x += (velocidad-4);
				    
				} else 
					if (x <= posix)
					cp =0;
			 }

			rectangulo.X = x;
			rectangulo.Y = y;     
			vuelta++;
		}

		public void Draw(SpriteBatch spriteBatch,Color colorm)
		{

		
				   spriteBatch.Draw (textura, rectangulo, colorm);
			  
		}
	}
}