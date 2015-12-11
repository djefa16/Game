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
    /// <summary>
    /// Struck used to contain a Size
    /// </summary>
    struct Size
    {
        public int height { get; private set; }

        public int width { get; private set; } 

        public Size(int width, int height) : this()
        {
            this.height = height;
            this.width = width;

        }
    }

    /// <summary>
    /// This class contains all the functionality of the MainMenu
    /// </summary>
    class MainMenu
    {   
        /// <summary>
        /// Enum used to determine the current GameState
        /// </summary>
		enum GameState { mainMenu,subMenu1,subMenu2,gameover,finish,tran,enterName,inGame,gameover2,finish2,}
		public bool terminado=false , salir=false,agregopan=true;

        /// <summary>
        /// The current state of the game
        /// </summary>
        GameState gameState;

        /// <summary>
        /// The name of the Player
        /// </summary>
        public string name = "";
		string linea="";
		int indcp = 0,i=0,x=100,rep=0;
		public int puntus=0;
        /// <summary>
        /// Determines if the letters should be caps or not
        /// </summary>
		private bool caps, nodir=false,sonopau=false, sonowin=false,sonogameov=false, sonomusic=false, pausa=false , parar=false,noescr=false, esnum=false;
		bool sumo= false;
        /// <summary>
        /// A Collection that hold all GUI elements connected to the menu
        /// </summary>
        private List<List<GUIElement>> menus;

        /// <summary>
        /// Contains the last pressed key
        /// </summary>
        private Keys[] lastPressedKeys = new Keys[1];
		public Archivos archtxts;
        /// <summary>
        /// Spritefont used when  drawing the text entered by the player
        /// </summary>
        SpriteFont sf;
		public Pantallas panux, panux2;


        
        public MainMenu()
        {   
            
			archtxts = new Archivos ();

			archtxts.inicializar ();
			archtxts.Leerinicio ();
			//Adds lists of GUI elements to the menu list
            
			menus = new List<List<GUIElement>>
            {   //MainMenu 0
                (new List<GUIElement> 
                { 
					new GUIElement("Texturas/Menu/menu00"), 
					new GUIElement("Texturas/Menu/Botones/comenzar"),  
					new GUIElement("Texturas/Menu/Botones/tutorial"),
					new GUIElement("Texturas/Menu/Botones/top ranking"),
					new GUIElement("Texturas/Menu/Botones/salir"),

                
				}),
                //Entra al menu del tutorial 1
                (new List<GUIElement> 
                { 
					new GUIElement("Texturas/Menu/menu01"),
					new GUIElement("Texturas/Menu/Botones/atras"),
                }),
				// menu top ranking 2
				(new List<GUIElement> 
				 { 
					new GUIElement("Texturas/Menu/menu02"), 
					new GUIElement("Texturas/Menu/Botones/atras"),
				}),
				// Pausa 3
				(new List<GUIElement> 
				 { 
					new GUIElement("Texturas/Menu/menu03"), 
					new GUIElement("Texturas/Menu/Botones/reanudar"),
					new GUIElement("Texturas/Menu/Botones/salir"),
				}),
			    //Gameover 4
				(new List<GUIElement> 
				 { //gameover
					new GUIElement("Texturas/Menu/menu04"), 
					new GUIElement("Texturas/Menu/Botones/pmenu"),
					new GUIElement("Texturas/Menu/Botones/salir"),
				}),
			    // Finish 5
				(new List<GUIElement> 
				 { //finish
					new GUIElement("Texturas/Menu/menu05"), 
					new GUIElement("Texturas/Menu/Botones/pmenu"),
					new GUIElement("Texturas/Menu/Botones/salir"),
				}),
			     // Enter Name 6
				(new List<GUIElement> 
				 { 
					new GUIElement("Texturas/Menu/menu06"), 
					new GUIElement("Texturas/Menu/menu07"),
					new GUIElement("Texturas/Menu/Botones/continuar"),
					new GUIElement("Texturas/Menu/Botones/atras"),

				}),
				//Trans 7
				(new List<GUIElement> 
				 { 
					new GUIElement("Texturas/Menu/menu02"),
					new GUIElement("Texturas/Menu/Botones/continuar"),

				}),
				//GameOver2 8 
				(new List<GUIElement> 
				 { //gameover2
					new GUIElement("Texturas/Menu/menu04"), 
					new GUIElement("Texturas/Menu/Botones/salir"),
				}),
				//finish2 9
				(new List<GUIElement> 
				 { 
					new GUIElement("Texturas/Menu/menu05"), 
					new GUIElement("Texturas/Menu/Botones/salir"),
				}),
			};
            //Sets the OnClick event on all menu items
            for (int i = 0; i < menus.Count; i++)
            {
                foreach (GUIElement button in menus[i])
                {
                    button.clickEvent += OnClick;
                } 
            }

        }
        /// <summary>
        /// Loads all content in the menu
        /// </summary>
        /// <param name="content">ContentManager</param>
        /// <param name="windowSize">the size of the GameWindow</param>
        public void LoadContent(ContentManager content, Size windowSize)
        {   
            //Loads the content of our spritefont
             sf = content.Load<SpriteFont>("MyFont");
			 
            //Loads the content of all other GUI elements
            for (int i = 0; i < menus.Count; i++)
            {
                foreach (GUIElement button in menus[i])
                {
                    button.LoadContent(content);
                    button.CenterElement(windowSize);
                }
            }
            //Sets offsets of the buttons
			//Menu principal
			menus[0].Find(x => x.ElementName == "Texturas/Menu/Botones/comenzar").MoveElement(0, -100);
			menus[0].Find(x => x.ElementName == "Texturas/Menu/Botones/tutorial").MoveElement(0, -40);
			menus[0].Find(x => x.ElementName == "Texturas/Menu/Botones/top ranking").MoveElement(0, 20);
			menus[0].Find(x => x.ElementName == "Texturas/Menu/Botones/salir").MoveElement(0, 80);

			//tutorial
			menus[1].Find(x => x.ElementName == "Texturas/Menu/Botones/atras").MoveElement(10, -285);
            //top ranking puntuacion
			menus[2].Find(x => x.ElementName == "Texturas/Menu/Botones/atras").MoveElement(220, 240);
			//Pausa
			menus[3].Find(x => x.ElementName == "Texturas/Menu/Botones/reanudar").MoveElement(0,-60);
			menus[3].Find(x => x.ElementName == "Texturas/Menu/Botones/salir").MoveElement(0,0);
            //Game Over
			menus[4].Find(x => x.ElementName == "Texturas/Menu/Botones/pmenu").MoveElement(0,180);
			menus[4].Find(x => x.ElementName == "Texturas/Menu/Botones/salir").MoveElement(0,240);
			//Winner
			menus[5].Find(x => x.ElementName == "Texturas/Menu/Botones/pmenu").MoveElement(0,180);
			menus[5].Find(x => x.ElementName == "Texturas/Menu/Botones/salir").MoveElement(0,240);
			//Enter Name
			menus[6].Find(x => x.ElementName == "Texturas/Menu/Botones/continuar").MoveElement(-45, 90);
			menus[6].Find(x => x.ElementName == "Texturas/Menu/Botones/atras").MoveElement(-45, 140);
           //Transicion entre pantalla y pantalla
			menus[7].Find(x => x.ElementName == "Texturas/Menu/Botones/continuar").MoveElement(-45, 90);
		   // Game Over2
			menus[8].Find(x => x.ElementName == "Texturas/Menu/Botones/salir").MoveElement(0,240);
			//Finish 2
			menus[9].Find(x => x.ElementName == "Texturas/Menu/Botones/salir").MoveElement(0,240);
		}
        /// <summary>
        /// Updates all our GUI element(Mainly checks if any button is pressed)
        /// </summary>


	
		public void Update (ref Pantallas pant,GameTime gameTime ,Song musicmenu,SoundEffect[] somen)// despues colocar como parametros bool gameover, bool finish, bool paused
        {   
            

			//Update each element according to the current gameState
		
			if (agregopan==false)
			{ 
				if (rep < 2)
					pant = panux;
				else
					pant = panux2;

				agregopan = true;
			}

			if (pausa==true)
			{ 
				if (sonopau==false)
				{ 
					somen[0].Play();
					sonopau = true;
				}

			}


			if (sonomusic==false)
			{
				MediaPlayer.Play(musicmenu);
				MediaPlayer.IsRepeating = true;
				MediaPlayer.Volume = 0.50f;
				sonomusic = true;
			}

		
			if (pant.pan[indcp].gameover==true)
			{

				if (sumo==false)
				{
				   rep++;
				   sumo = true;
				}
				if (sonogameov==false)
				{ 
					somen[1].Play();
					sonogameov = true;
				}
				if (noescr==false)
				{			   
					archtxts.EscribiryLeer (name, puntus,rep);
					noescr = true;
				}

				MediaPlayer.Pause ();
				//noescr = false;
				//indcp = 0;
				//sonogameov=false;
				if (rep <= 2 )
				  gameState = GameState.gameover;
				else
					gameState = GameState.gameover2;
				//inda++;

			}
		    else
				if (pant.pan[indcp].finish)
			    { 

				   
				  if (indcp < 3 )
				  { 

					gameState= GameState.tran;
					indcp++;

				 }
				 else
				 {      
					if (sumo==false)
					{
						rep++;
						sumo = true;
					}

					if (sonowin == false)
					{   
						somen [2].Play ();
						sonowin = true;
					}

					if (noescr==false)
					{			   
						archtxts.EscribiryLeer (name, puntus,rep);

					}
					if (rep <= 2 )
						gameState = GameState.finish;
					else
						gameState = GameState.finish2;
					//sonomusic = false;
					//MediaPlayer.Pause ();
					//noescr = false;
					//indcp = 0;
					//sonowin = false;
					//puntus = 0;
				}
			}


			switch (gameState)
            {
                case GameState.mainMenu: 
				    foreach (GUIElement button in menus[0]) //MainMenu
                    {
                        button.Update();
                    }
			       
                 break;
			    case GameState.subMenu1: 
			    foreach (GUIElement button in menus[1]) //Tutorial
				{
					   button.Update();
				}
                  break;
			    case GameState.subMenu2: 
				foreach (GUIElement button in menus[2]) //Ranking
				{
					button.Update();
				}
				break;
			  
			    case GameState.gameover: 
				foreach (GUIElement button in menus[4]) //gameover
				{
					button.Update();
				}

				break;
			    case GameState.finish: 
				foreach (GUIElement button in menus[5]) //Winner
				{
					button.Update();
				}

				break;
			    case GameState.enterName:
				foreach (GUIElement button in menus[6])//EnterName menu
				{
					GetKeys();
					button.Update();
				}
				break;
			    case GameState.tran:
				foreach (GUIElement button in menus[7])//Transicion entre pantalla y pantalla
				{

					button.Update();
				}
				break;
			    case GameState.gameover2:
				foreach (GUIElement button in menus[8])//Gameover2
				{

					button.Update();
				}
				break;
			    case GameState.finish2:
				foreach (GUIElement button in menus[9])//Finish2
				{

					button.Update();
				}
				break;
			   case GameState.inGame:
			   {
					  if (parar==false) 
				      {
					    MediaPlayer.Pause();
						parar = true;
				      }
				       //hacer update de la pantalla
				     
				        pant.pan[indcp].Update(gameTime, ref puntus,ref pausa);

				     if (pausa==true)
					 foreach (GUIElement button in menus[3]) //Pausa
				     {
					    button.Update();
				     }

			     }
				break;

			
			}
          
		
		}

        /// <summary>
        /// Draws all GUI elements
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch</param>
		public void Draw(SpriteBatch spriteBatch, Pantallas pant)
        {   
            //Draws each element according to the current gameState

			switch (gameState)
            {
                case GameState.mainMenu:
			    {     
				    
				    foreach (GUIElement button in menus[0]) //Menu Principal
                    {
                        button.Draw(spriteBatch);
                    }
				 
			    }    
				break;
			    case GameState.subMenu1: 
			
				    foreach (GUIElement button in menus[1]) //Submenu tutorial
				    {
					    button.Draw(spriteBatch);
					    
				    }
			    
				break;

				case GameState.subMenu2: 
				foreach (GUIElement button in menus[2]) //Submenu Ranking
				{
					for (i=0; i < archtxts.cantl; i++)
					{

						linea = archtxts.vecr [i].nombre+ "   "+ Convert.ToString (archtxts.vecr[i].punt);
						spriteBatch.DrawString(sf,linea, new Vector2(x, 50+(18*i)), Color.Green);

					}

					button.Draw(spriteBatch);
				}
				break;
			 
				case GameState.gameover: 
				foreach (GUIElement button in menus[4]) //gameover
				{
					spriteBatch.DrawString(sf, "PUNTUACION:  "+puntus, new Vector2(170, 201), Color.Green);
					spriteBatch.DrawString(sf, "NOMBRE DEL JUGADOR:  "+name, new Vector2(170, 261), Color.Green);
					button.Draw(spriteBatch);
				}

				break;
				case GameState.finish: 
				foreach (GUIElement button in menus[5]) //Winner
				{
					spriteBatch.DrawString(sf, "PUNTUACION:  "+puntus, new Vector2(170, 201), Color.Green);
					spriteBatch.DrawString(sf, "NOMBRE DEL JUGADOR:  "+name, new Vector2(170, 261), Color.Green);
					button.Draw(spriteBatch);
				}
				break;
			    case GameState.enterName:
				foreach (GUIElement button in menus[6])//EnterName menu
				{   
					spriteBatch.DrawString(sf, name, new Vector2(260, 310), Color.Red);
					button.Draw(spriteBatch);
				}
				break;
			    case GameState.tran:
				foreach (GUIElement button in menus[7])//Trans
				{   
					spriteBatch.DrawString(sf, "PUNTUACION:  "+puntus, new Vector2(78, 81), Color.Green);
					spriteBatch.DrawString(sf, "NOMBRE DEL JUGADOR:  "+name, new Vector2(78, 141), Color.Green);
					button.Draw(spriteBatch);
				}
				break;
			    case GameState.gameover2:
				foreach (GUIElement button in menus[8]) //gameover2
				{
					spriteBatch.DrawString(sf, "PUNTUACION:  "+puntus, new Vector2(170, 201), Color.Green);
					spriteBatch.DrawString(sf, "NOMBRE DEL JUGADOR:  "+name, new Vector2(170, 261), Color.Green);
					button.Draw(spriteBatch);
				}
				break;
			    case GameState.finish2:
				foreach (GUIElement button in menus[9]) //Finish2
				{
					spriteBatch.DrawString(sf, "PUNTUACION:  "+puntus, new Vector2(170, 201), Color.Green);
					spriteBatch.DrawString(sf, "NOMBRE DEL JUGADOR:  "+name, new Vector2(170, 261), Color.Green);
					button.Draw(spriteBatch);
				}
				break;
			   
			   case GameState.inGame:
			   {    //hacer el Draw de la pantalla
					
				       pant.pan [indcp].Draw (spriteBatch);
				       if (pausa==true)
				 	   foreach (GUIElement button in menus[3]) //Pausa
				       {
					         button.Draw(spriteBatch);
				       }
			    
			    }
				break; 
             
			}


        }

        /// <summary>
        /// Method called every time a GUI element is clicked
        /// </summary>
        /// <param name="element"></param>
        public void OnClick(string element)
        {
			if (element == "Texturas/Menu/Botones/comenzar")//Menu principal
            {
                gameState = GameState.enterName;
            }
			if (element == "Texturas/Menu/Botones/tutorial")//tutorial
            {
				gameState = GameState.subMenu1;

            }
          
			if (element == "Texturas/Menu/Botones/atras")//vuelve atras al menu principal
			{

				gameState = GameState.mainMenu;

			}

			if (element == "Texturas/Menu/Botones/pmenu")//vuelve al principio del juego al menu principal
			{
				MediaPlayer.Pause ();
				sonomusic = false;
				noescr = false;
				indcp = 0;
				puntus = 0;
				sonowin = false;
				sonogameov=false;
				agregopan = false;
				gameState = GameState.mainMenu;
				sumo = false;
			}


			if (element == "Texturas/Menu/Botones/top ranking")//Puntuaciones
            {
               gameState= GameState.subMenu2;

            }
		   
			if (element == "Texturas/Menu/Botones/reanudar")//tutorial
			{
				pausa = false;
				sonopau = false; 
				gameState= GameState.inGame;

			}


			if (element == "Texturas/Menu/Botones/continuar")//En el menu Enter Name o para continuar otra pantalla
			{

			
				gameState = GameState.inGame;

			}


			if (element == "Texturas/Menu/Botones/salir")//tutorial
            {
                 salir=true;
				 
            }
		
		}

        private void GetKeys()
        {
            KeyboardState kbState = Keyboard.GetState();
            Keys[] pressedKeys = kbState.GetPressedKeys();

            //check if any of the previous update's keys are no longer pressed
            foreach (Keys key in lastPressedKeys)
            {
                if (!pressedKeys.Contains(key))
                    OnKeyUp(key);
            }

            //check if the currently pressed keys were already pressed
            foreach (Keys key in pressedKeys)
            {
                if (!lastPressedKeys.Contains(key))
                    OnKeyDown(key);
            }
            //save the currently pressed keys so we can compare on the next update
            lastPressedKeys = pressedKeys;
        }

        private void OnKeyDown(Keys key)
        {
			switch (key)
			{
			    case Keys.NumPad0:
			    {
				   name += "0";
					esnum = true;
			     }
				break;
				case Keys.NumPad1:
			     {
				    name += "1";
				    esnum = true;
			    }
				break;
				case Keys.NumPad2:
			    {
				   name += "2";
				   esnum = true;
			    }
				break;
				case Keys.NumPad3:
			    {
				   name += "3";
				   esnum = true;
			    }
				break;
				case Keys.NumPad4:
			   {
				   name += "4";
				   esnum = true;
			   }
				break;
				case Keys.NumPad5:
			   {
				   name += "5";
				   esnum = true;
			   }
				break;
				case Keys.NumPad6:
			   {
				   name += "6";
				   esnum = true;
			    }
				break;
				case Keys.NumPad7:
			    {
				    name += "7";
				    esnum = true;
			    }
				break;
				case Keys.NumPad8:
			    {
				   name += "8";
				   esnum = true;
			    }
				break;
				case Keys.NumPad9:
			   {
				   name += "9";
				   esnum = true;
			   }
				break;
			  
			}


			switch (key)
			{
			case Keys.D0:
			{
				name += "0";
				esnum = true;
			}
				break;
				case Keys.D1:
			{
				name += "1";
				esnum = true;
			}
				break;
				case Keys.D2:
			{
				name += "2";
				esnum = true;
			}
				break;
				case Keys.D3:
			{
				name += "3";
				esnum = true;
			}
				break;
				case Keys.D4:
			{
				name += "4";
				esnum = true;
			}
				break;
				case Keys.D5:
			{
				name += "5";
				esnum = true;
			}
				break;
				case Keys.D6:
			{
				name += "6";
				esnum = true;
			}
				break;
				case Keys.D7:
			{
				name += "7";
				esnum = true;
			}
				break;
				case Keys.D8:
			{
				name += "8";
				esnum = true;
			}
				break;
				case Keys.D9:
			{
				name += "9";
				esnum = true;
			}
				break;
			
			}

			if (key != Keys.Left && key != Keys.Right && key != Keys.Down && key != Keys.Up && key != Keys.Enter && key != Keys.Delete)	
				nodir = false;
			else
				nodir = true;

			if (key != Keys.NumPad0 && key != Keys.D0 && key != Keys.NumPad1 && key != Keys.D1 && key != Keys.NumPad2 && key != Keys.D2 && key != Keys.NumPad3 && key != Keys.D3 && key != Keys.NumPad4 && key != Keys.D4 && key != Keys.NumPad5 && key != Keys.D5 && key != Keys.NumPad6 && key != Keys.D6 && key != Keys.NumPad7 && key != Keys.D7 && key != Keys.NumPad8 && key != Keys.D8 && key != Keys.NumPad9 && key != Keys.D9)
				esnum = false;
			//do stuff
            if (key == Keys.Back && name.Length > 0) //Removes a letter from the name if there is a letter to remove
            {
                name = name.Remove(name.Length - 1);
            }
            else 
				if (key == Keys.LeftShift || key == Keys.RightShift)//Sets caps to true if a shift key is pressed
            {
                caps = true;
            }
            else 
				if (!caps && name.Length < 16 && !esnum && !nodir) //If the name isn't too long, and !caps the letter will be added without caps
            {
                if (key == Keys.Space)
                {
                    name += " ";
                }
                else
                {
                    name += key.ToString().ToLower();
                }
            }
			else  
			   
				if (name.Length < 16 && !esnum && !nodir) //Adds the letter to the name in CAPS
            {  
	              			   
					   name += key.ToString();
            }
        }

        private void OnKeyUp(Keys key)
        {
            //Sets caps to false if one of the shift keys goes up
            if (key == Keys.LeftShift || key == Keys.RightShift)
            {
                caps = false;
            }
        }
    }
}
