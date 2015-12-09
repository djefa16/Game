#region Using Statements

using System;
using System.Collections.Generic; 
using System.Linq; 
using Microsoft.Xna.Framework; 
using Microsoft.Xna.Framework.Audio; 
using Microsoft.Xna.Framework.Content; 
using Microsoft.Xna.Framework.GamerServices; 
using Microsoft.Xna.Framework.Graphics; 
using Microsoft.Xna.Framework.Input; 
using Microsoft.Xna.Framework.Media; 
using Microsoft.Xna.Framework.Net; 
using Microsoft.Xna.Framework.Storage; 

#endregion

namespace AlgoRobot
{
	public class Boss
	{

		Texture2D spriteboss; 
		Vector2 posicionInicial = Vector2.Zero; 
		Vector2 posicion = Vector2.Zero;
		public Colision1 chocobosspv;
		int ind=0,limit=0,pri=0;
		float i=0.0f;
		float angulo = 50.0f,r; 
		public Color colorboss;
		public int bdamage, vitalidad,valor;
		int calc=0;
		public bool atacar,activado,muerto,entro,nosono=false;
		public Rectangle rectaboss;
		SoundEffect shotboss,soundexp;
		Animacion explosion;

		public void Initialize(Texture2D texturaboss,Texture2D cutext, Texture2D texshot, 
		                       int vitalidad, int bdamage, SoundEffect shotboss, SoundEffect soexp,Texture2D texp) 
		{ 
			this.shotboss = shotboss;
			atacar = false;
			chocobosspv = new Colision1 ();
			explosion = new Animacion ();
			activado = false;
			muerto = false;
			soundexp = soexp;
			spriteboss = texturaboss;
			posicionInicial = new Vector2(340,150);
			colorboss = Color.TransparentWhite;
			this.bdamage = bdamage;
			this.vitalidad = vitalidad;
			valor = vitalidad;
			chocobosspv.Initialize (spriteboss,texshot, texshot, cutext,3,9);
			explosion.Initialize (texp, posicion, 187, 190, 12, 100, Color.White, 1.0f, false,true);

		} 

	
		public void Update(GameTime gameTime, ref bool finishp)
		{ 

			activado = true;

			if (pri == 0)
				entro = true;
			else
				entro = false;

			if (muerto ==false)
			{
			     r = 300; 

			   if (angulo == 360) 
				  angulo = 0; 
			    else 
			   {
				   calc++;
				   angulo+=i; 
					if (calc % 15 == 0)
					{
					    shotboss.Play ();
						atacar = true;
					    
					}
					else
						atacar = false;
				}

			   rectaboss = new Rectangle ( Convert.ToInt16(r * Math.Cos(angulo) + posicionInicial.X), Convert.ToInt16((r/4.2f) * Math.Sin(angulo) + posicionInicial.Y),spriteboss.Width, spriteboss.Height); 

			   if ((ind%20==0)&& limit < 18)
			  {
				  limit++;
				  i += 0.0010f;
			   } 
			//choco.Update (gameTime, rectaboss, posicionInicial);
			//explosion.Update (gameTime,rectaboss);
				ind++;
				pri++;
		  }
		  else
		  { 
				  if (nosono==false)
				{    
					soundexp.Play ();
					nosono = true;
				}

				explosion.Update (gameTime, rectaboss);
				if (explosion.Active == false)
					finishp = true;
		  }
			
			if (vitalidad <= 0)
				muerto = true;
			
	    } 

		public void Draw(SpriteBatch spriteBatch) 
		{ 

			if (muerto == false) 
			{     
				spriteBatch.Draw (spriteboss, rectaboss, Color.White);
				chocobosspv.Draw (spriteBatch);

		     } 
			else
				explosion.Draw (spriteBatch);
		
		} 


	}
}

