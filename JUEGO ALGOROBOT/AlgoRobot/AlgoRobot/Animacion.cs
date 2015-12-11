#region Using Statements
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

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
	
	public class Animacion
	{
		// La imagen animada representada por un grupo de imágenes
		Texture2D spriteStrip;
		// Valor para escalar el sprite
		float scale;
		// Tiempo desde la ultima vez que se actualizo la imagen
		public int elapsedTime;
		// Tiempo de despliegue por imagen
		int frameTime;
		// Numero de imágenes que conforman la animación
		int frameCount;
		// Indice de la imagen actual
		int currentFrame;
		// Color de la imagen a desplegar
		Color color;
		// El área de la imagen que vamos a desplegar
		public Rectangle sourceRect = new Rectangle();
		// El área donde queremos desplegar la imagen
		Rectangle destinationRect = new Rectangle();
		// Ancho de la una imagen
		public int FrameWidth;
		// Alto de una imagen
		public int FrameHeight;
		// Estado de la animacion
		public bool Active;
		// Repetir animación
		public bool Looping;
		bool mov;
		// Posición del sprite
		public Vector2 Position;

		public void Initialize(Texture2D texture, Vector2 position, int frameWidth,
		                       int frameHeight, int frameCount, int frametime, Color color,
		                       float scale, bool looping, bool mov)
		{
			// Mantener copias locales de los valores pasados
			this.mov = mov;
			this.color = color;
			this.FrameWidth = frameWidth;
			this.FrameHeight = frameHeight;
			this.frameCount = frameCount;
			this.frameTime = frametime;
			this.scale = scale;

			Looping = looping;
			Position = position;
			spriteStrip = texture;

			// Hacer reset a los tiempos
			elapsedTime = 0;
		    currentFrame = 0;
			// Activar la animación por defecto
			Active = true;
		}


		public void Update(GameTime gameTime, Rectangle recta)
		{
			// No actualizar si la imagen esta desactivada
			if (!Active) 
			{
			
				return;
			}
				// Actualizar tiempo transcurrido
			elapsedTime += (int)gameTime.ElapsedGameTime.TotalMilliseconds;
			// Si elapsedTime es mayor que frame time debemos cambiar de imagen
			if (elapsedTime > frameTime)
			{
				// Movemos a la siguiente imagen
				currentFrame++;
				// Si currentFrame es igual al frameCount hacemos reset currentFrame a cero
				if (currentFrame == frameCount)
				{
					currentFrame = 0;
					// Si no queremos repetir la animacion asignamos Active a falso
					if (!Looping) 
						Active = false;
				}
				// Reiniciamos elapsedTime a cero


				elapsedTime = 0;
			}
			// Tomamos la imagen correcta multiplicando el currentFrame  por el ancho de la imagen
			sourceRect = new Rectangle(currentFrame * FrameWidth, 0, FrameWidth, FrameHeight);

			// Actualizamos la posicion de la imagen en caso que esta se desplace por la pantalla

			if (mov==false)
			{
				destinationRect =	new Rectangle((int)Position.X - (int)(FrameWidth * scale) / 2,
				(int)Position.Y - (int)(FrameHeight * scale) / 2,
				(int)(FrameWidth * scale),
				(int)(FrameHeight * scale));
			}
			if ((mov==true) && currentFrame < frameCount)
			{ 
				destinationRect = new Rectangle((int)recta.X - (int)(FrameWidth * scale)/5,
				                                (int)recta.Y - (int)(FrameHeight * scale)/5,
				                                (int)(FrameWidth ),
				                                (int)(FrameHeight));
		  
	        }
	    }

		public void Draw(SpriteBatch spriteBatch)
		{
			if (Active) 
			{
				spriteBatch.Draw (spriteStrip, destinationRect, sourceRect, color);
			} 
	   
		}
	
	}
}		
