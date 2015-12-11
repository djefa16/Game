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
	public class Colision1
	{


		Texture2D textcut;
		Texture2D texturaplay;
		Texture2D texturaShot,texturaMisil,texturaBala;
		// Un vector para las posiciones X,Y del personaje
		public Color colordis,colorplay;

		// Variable para cálculo de rectángulos de Sprites
		public Rectangle rectanguloplay;
		Rectangle rectanguloShot,rectanguloShotiz,rectanguloShotd;
		bool boss=false,touch=false;
		// Una lista de vector2 para las pocisiones X,Y de las Balas 
		List <Vector2> posicionesShot = new List<Vector2> ();
		List <Vector2> posicionesShotd = new List<Vector2> (); 
		List <Vector2> posicionesShotiz = new List<Vector2> ();
		byte tipcol;// hay 3 tipos de colisiones
		int velocidadshot;
		ulong lado=0;
		float scale=1.0f;
		public int toco1=0,toco2=0;
		int i,x=0,entro=0, numpow=0, vuelta=0;


		public void Initialize (Texture2D texturaplay,Texture2D texturaBala,
		                        Texture2D texturaMisil,Texture2D cutext, byte tipcol, int velshot)
		
		{

			lado = 0;
			this.texturaplay = texturaplay;
			this.texturaBala = texturaBala;
			this.texturaMisil = texturaMisil;
			texturaShot = texturaBala;
			this.tipcol = tipcol;
			textcut = cutext;
			colorplay = Color.Transparent;
			velocidadshot = velshot;

			if (tipcol == 3)
				scale = 0.2f; 
		 

		}

		public void Update (Rectangle rect1,Rectangle rect2,Rectangle rect3, bool act,ref bool powerupt,bool esboss,
		                    SoundEffect impact,  int damage, ref int vida)
		{           


			//rectanguloplay = new Rectangle ((int)rect.X + (texturaplay.Width / 4), (int)rect.Y + (texturaplay.Height / 4), texturaplay.Width / 4, texturaplay.Height / 4); (usarlo para tip 3° player a bos)
         
			// hay 4 tipos de colisiones, 1° player a enemigo o viga,2° player a boss o a viga,3°boss o enemigo a player o a viga, 4°player a powerup,
			colorplay= Color.Transparent;
			if (esboss == true)
				boss = true;

			if (tipcol!=4)
			   if (powerupt==true)
				   texturaShot = texturaMisil;
		         else
				   texturaShot = texturaBala;


			if (tipcol==1 )
			{
				//toco1 = 0;
				//toco2 = 0;
				if (act == true )
				{  

					entro ++;
                   if (entro == 1)
				  {	
					if (lado%2==0)
					{
						posicionesShotiz.Add (new Vector2 ((int)rect1.X+3, (int)rect1.Y));
						lado--;
					}
					else
					{
						posicionesShotd.Add (new Vector2 ((int)rect1.X+40, (int)rect1.Y));
						lado++;
					}
				}
				if (entro == 4)
				   entro = 0;
			   }
			
				// actualizar cada proyectil

				for ( i = 0; i < posicionesShotiz.Count; i++) 
				{ 
					toco1 = 0;
					// actualizo las posiciones de los proyectiles
					posicionesShotiz [i] = new Vector2 (posicionesShotiz [i].X, 
					                                  posicionesShotiz [i].Y- velocidadshot );

					// obtener el rectangulo del proyectil


					rectanguloShotiz = new Rectangle ((int)posicionesShotiz[i].X,
						                                (int)posicionesShotiz [i].Y,
						                                texturaShot.Width, texturaShot.Height);

					// evaluar colisiÃ³n con el rectangulo y actualizamos la vida

					if (rectanguloShotiz.Intersects(rect2)||(rectanguloShotiz.Intersects(rect3)))
					{
						toco1++;

						if ((toco1==1) && (rectanguloShotiz.Intersects(rect2)))
						{ 
							impact.Play ();
							vida -=damage;
						}

					} 
					if (toco1 > 0) 
					{ 
						posicionesShotiz.RemoveAt (i);
						// decrecemos i, por que hay un enemigo menos 
						i--;
					}

				}
			

				for ( i = 0; i < posicionesShotd.Count; i++) 
				{ 
					toco2 = 0;
					// actualizo las posiciones de los proyectiles
					posicionesShotd [i] = new Vector2 (posicionesShotd [i].X, 
					                                  posicionesShotd [i].Y- velocidadshot );

					// obtener el rectangulo del proyectil


					rectanguloShotd = new Rectangle ((int)posicionesShotd[i].X,
					                                (int)posicionesShotd [i].Y,
					                                texturaShot.Width, texturaShot.Height);


					// evaluar colisiÃ³n con el rectangulo y actualizamos la vida

					if (rectanguloShotd.Intersects(rect2)||(rectanguloShotd.Intersects(rect3)))
					{
						toco2++;

						if ((toco2==1) && (rectanguloShotd.Intersects(rect2)))
						{ 
							impact.Play ();
							vida -=damage;
						}

					} 
					if (toco2 > 0) 
					{ 
						posicionesShotd.RemoveAt (i);
						// decrecemos i, por que hay un enemigo menos 
						i--;
					}

				}
			
			
			
			}
			else
				//player a boss
				if (tipcol==2)
				{  
				      
				      if (touch==true) 
				        if (vuelta <50)
				       {     
					       vuelta++; 
						  if (vuelta==50)
					      { 
						     vuelta = 0;  
						    touch = false;
					       }
					}
				     
					if (act == true )
				    {  
					      
					      lado ++;
						   if (lado%2==0)
						   {
							 posicionesShotiz.Add (new Vector2 ((int)rect1.X+3, (int)rect1.Y));
							        
						    }
						     else
						     {
							   posicionesShotd.Add (new Vector2 ((int)rect1.X+40, (int)rect1.Y));
							       
						    }
					      
				     }
					 

				    rectanguloplay = new Rectangle ((int)rect2.X + (texturaplay.Width / 4), (int)rect2.Y + ((3/4)*(texturaplay.Width)), textcut.Width, textcut.Height);

					for ( i = 0; i < posicionesShotiz.Count; i++) 
					{ 
					      toco1 = 0;
						// actualizo las posiciones de los proyectiles
						posicionesShotiz [i] = new Vector2 (posicionesShotiz [i].X, 
						                                  posicionesShotiz [i].Y- velocidadshot );

						// obtener el rectangulo del proyectil
						rectanguloShotiz = new Rectangle ((int)posicionesShotiz[i].X,
						                                (int)posicionesShotiz [i].Y,
						                                texturaShot.Width, texturaShot.Height);

						// evaluar colision con el rectangulo del boss y actualizamos la vida
						// rect3 seria la placa
						if (rectanguloShotiz.Intersects(rectanguloplay)|| rectanguloShotiz.Intersects (rect3))
						{
							toco1++;
						    if (toco1==1 && rectanguloShotiz.Intersects(rectanguloplay))
							{  
								impact.Play ();
								vida-=damage;
							    touch = true;
							}	

						} 
						if (toco1 > 0) 
						{ 
							posicionesShotiz.RemoveAt (i);
							// decrecemos i, por que colisiono la bala y desaparece
							i--;
						 
					   }


					}
				   
				for ( i = 0; i < posicionesShotd.Count; i++) 
				{ 

					toco2 = 0;
					// actualizo las posiciones de los proyectiles
					posicionesShotd [i] = new Vector2 (posicionesShotd [i].X, 
					                                  posicionesShotd [i].Y- velocidadshot );

					// obtener el rectangulo del proyectil
					rectanguloShotd = new Rectangle ((int)posicionesShotd[i].X,
					                                (int)posicionesShotd [i].Y,
					                                texturaShot.Width, texturaShot.Height);

					// evaluar colision con el rectangulo del boss y actualizamos la vida
					// rect3 seria la placa
					 if (rectanguloShotd.Intersects(rectanguloplay)|| rectanguloShotd.Intersects (rect3))
					 {
						toco2++;
						if (toco2==1 && rectanguloShotd.Intersects(rectanguloplay))
						{  
							impact.Play ();
							vida-=damage;
							touch = true;
						}	

					 } 
					 if (toco2 > 0) 
					{ 
						posicionesShotd.RemoveAt (i);
						// decrecemos i, por que colisiono la bala y desaparece
						i--;

					}


				  }

			
			    } 
			    else
				  //boss o enemigo a player y placa
				  if ( tipcol==3 )
				  {
					    if (act == true )
					   {  
						     					 
						     lado++;
					      if (esboss== true)
					      {   
						       if (lado%2==0)
							  
						          posicionesShotiz.Add (new Vector2 ((int)rect1.X+30, (int)rect1.Y));
						       else
							    posicionesShotd.Add (new Vector2 ((int)rect1.X+30, (int)rect1.Y));
					      
					      }
					       else
					       {     
						        if (lado%2==0)

							         posicionesShotiz.Add (new Vector2 ((int)rect1.X+1, (int)rect1.Y));
					          else
						          posicionesShotd.Add (new Vector2 ((int)rect1.X+1, (int)rect1.Y));
					
					        }
					}


						for ( i = 0; i < posicionesShotiz.Count; i++) 
						{ 
					         toco1 = 0;
							// actualizo las posiciones de los proyectiles
							posicionesShotiz [i] = new Vector2 (posicionesShotiz [i].X-1, 
							                                    posicionesShotiz [i].Y+ velocidadshot);

							// obtener el rectangulo del proyectil
							rectanguloShotiz = new Rectangle ((int)posicionesShotiz[i].X,
							                                  (int)posicionesShotiz [i].Y,
							                                  texturaShot.Width, texturaShot.Height);

							// evaluar colision con el rectangulo y actualizamos la vida:

							if (rectanguloShotiz.Intersects(rect2)||(rectanguloShotiz.Intersects(rect3)))
							{
								toco1++;
								if ((toco1==1) && (rectanguloShot.Intersects(rect2)))
								{  
									impact.Play ();
									vida-=damage;
								}	
							} 

							if (toco1 > 0) 
							{ 
								posicionesShotiz.RemoveAt (i);
								// decrecemos i, por que hay un enemigo menos 
								i--;

							}

						}

						for ( i = 0; i < posicionesShotd.Count; i++) 
						{ 
					         toco2 = 0;
							// actualizo las posiciones de los proyectiles
							posicionesShotd [i] = new Vector2 (posicionesShotd [i].X+1, 
							                                   posicionesShotd [i].Y+ velocidadshot);

							// obtener el rectangulo del proyectil
							rectanguloShotd = new Rectangle ((int)posicionesShotd[i].X,
							                                 (int)posicionesShotd [i].Y,
							                                 texturaShot.Width, texturaShot.Height);

							// evaluar colisiÃ³n con el rectangulo y actualizamos la vida

							if (rectanguloShotd.Intersects(rect2)||(rectanguloShotd.Intersects(rect3)))
							{
								toco2++;
								impact.Play ();
								if ((toco2==1) && (rectanguloShotd.Intersects(rect2)))
								{  
									impact.Play ();
									vida-=damage;
								}	
							} 

							if (toco2 > 0) 
							{ 
								 posicionesShotd.RemoveAt (i);
								// decrecemos i, por que hay un enemigo menos 
								i--;
							}

						}

					
			    }
		        else
				  if (tipcol==4)
				  { 	
					  //player a powerup
					  if (act==true && numpow==0)
					  {	
					      numpow++;
					      Random r = new Random(DateTime.Now.Millisecond);
						  x = r.Next (100, 650);
						  posicionesShot.Add (new Vector2 (x,0));
					  }


					for ( i = 0; i < posicionesShot.Count; i++) 
					{ 

						posicionesShot [i] = new Vector2 (posicionesShot [i].X, 
						                                  posicionesShot [i].Y+ 3 );


						rectanguloShot = new Rectangle ((int)posicionesShot[i].X,
						                                (int)posicionesShot [i].Y,
						                                texturaShot.Width,texturaShot.Height);  

						// evaluar colisiÃ³n con el rectangulo y actualizamos el personaje
						//aqui rectanguloshot es el del powerup

						if (rectanguloShot.Intersects(rect1))
						{
							toco1++;
							if ((toco1==1))
							{
								
								powerupt = true;

							}
						}

						if (toco1 > 0 )
						{ 
							posicionesShot.RemoveAt (i);
							// decrecemos i, por que hay un enemigo menos 
							i--;
						}


					} 
				}  

		
		
		} 

		 

		
		public void Draw (SpriteBatch spriteBatch)
		{
			
			//dibujar balas de player
			if (tipcol ==1 )// el player
			{
				 foreach (Vector2 posicionShot in posicionesShotiz) 
			    { 
				    spriteBatch.Draw (texturaShot, posicionShot,Color.White);
			    }
			   foreach (Vector2 posicionShot in posicionesShotd) 
			   { 
				    spriteBatch.Draw (texturaShot, posicionShot,Color.White);
			   }


			}
			  
			 else
				if (tipcol ==2 )// el player
			   {
				foreach (Vector2 posicionShot in posicionesShotiz) 
				{ 
					spriteBatch.Draw (texturaShot, posicionShot,Color.White);
				}
				foreach (Vector2 posicionShot in posicionesShotd) 
				{ 
					spriteBatch.Draw (texturaShot, posicionShot,Color.White);
				}


			 }

			 else
				// Dibujar balas de enemigos
			   if ( tipcol==3)
			  {
				  foreach (Vector2 posicionShot in posicionesShotiz) 
			     { 
				    spriteBatch.Draw (texturaShot, posicionShot,Color.Red);
			     }
				foreach (Vector2 posicionShot in posicionesShotd) 
				{ 
					spriteBatch.Draw (texturaShot, posicionShot,Color.Red);
				}
			 
			}
			  else
			  	// player a powerup
				if ( tipcol==4)
					foreach (Vector2 posicionShot in posicionesShot) 
				  { 
					spriteBatch.Draw (texturaShot, posicionShot,Color.White);
				  }
			 
	     
		
		}


     }

 }


