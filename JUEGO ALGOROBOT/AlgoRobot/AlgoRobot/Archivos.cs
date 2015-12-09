using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace AlgoRobot
{

	public struct regjug
	{
		public string nombre;
		public int punt;
	}


	public class Archivos
	{

		public regjug [] vecr= new regjug[20];
		regjug [] vecaux= new regjug[3];
		regjug regijug, regaux;
		FileStream stream = new FileStream("../../datos.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite);
		StreamReader sr ;
		StreamWriter sw ;
		//StreamWriter archrank;
		List<regjug> listar = new List<regjug>();
		string line;
		public int cantl = 0, i=0,j=0, pos=1;
		int indaux=0;


		public void inicializar ()
		{
			sr = new StreamReader (stream);
			sw = new StreamWriter (stream);
		}
		public void Leerinicio ( )
		{


			while((line = sr.ReadLine()) != null)
			{
				pos = line.IndexOf (" ");
				regaux.nombre = line.Substring (0, pos);
				pos += 3;
				line = line.Remove (0, pos);
				regaux.punt = int.Parse (line);
				listar.Add (regaux);
				cantl++;
			}


			//Metodo de ordenamiento por burbujeo
			for ( i = listar.Count - 1; i > 0; i--)
			{

				//las iteraciones son siempre desde el principio hasta el límite puesto arriba
				for (j = 0; j < i; j++)
				{

					//si el número es más pequeño que el próximo, se cambian de lugar
					if (listar[j].punt < listar[j + 1].punt)
					{
						regaux = listar[j];
						listar[j] = listar[j + 1];
						listar[j + 1] = regaux;
					}
				}
			}


			if (cantl < 20)
			{


				for (i =0; i  < listar.Count; i++)
				{
					vecr [i] = listar [i];

				}


			}
			else
			{
				cantl = 20;
				for (i=0 ; i <  cantl ; i++)
				{

					vecr[i]= listar[i]; 

				}

			}



		}
		public void EscribiryLeer (string nombre, int punt, int cantlin)
		{
			// Esto va en el escribir
	
			indaux = cantlin;

			regijug.nombre = nombre;
			regijug.punt = punt;
			vecaux [indaux-1] = regijug;
			listar.Add (regijug);
			cantl++;


			//Metodo de ordenamiento por burbujeo
			for ( i = listar.Count - 1; i > 0; i--)
			{

				//las iteraciones son siempre desde el principio hasta el límite puesto arriba
				for (j = 0; j < i; j++)
				{

					//si el número es más pequeño que el próximo, se cambian de lugar
					if (listar[j].punt < listar[j + 1].punt)
					{
						regaux = listar[j];
						listar[j] = listar[j + 1];
						listar[j + 1] = regaux;
					}
				}
			}


			if (cantl < 20)
			{


				for (i =0; i  < listar.Count; i++)
				{
					vecr [i] = listar [i];

				}



			}
			else
			{
				cantl = 20;
				for (i=0 ; i <  cantl ; i++)
				{

					vecr[i]= listar[i]; 

				}

			}

			//archrank = new StreamWriter ("../../topranking.txt", false);

			//for (i=0 ;i < cantl; i++)
			//{  

				//line = vecr [i].nombre;
				//line = line + "   ";
				//line = line + Convert.ToString (vecr [i].punt);
				//archrank.WriteLine (line);

			//}

		}

		public void Cerrar ()
		{
			//archrank.Close ();

			for (i= 0 ; i < indaux; i++)
			{  
				line = vecaux [i].nombre+"   "+  Convert.ToString (vecaux [i].punt);
				sw.WriteLine (line); 
			}
			sw.Close ();
			sr.Close ();


		}
	}
}

