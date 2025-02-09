using System;
using System.IO;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;

public class Serializer
{
	public static T Load<T>(string filename) where T: class
	{
		if (File.Exists("saves/" + filename))
		{
			try
			{
				using (Stream stream = File.OpenRead("saves/" + filename))
				{
					BinaryFormatter formatter = new BinaryFormatter();
                    
					return formatter.Deserialize(stream) as T;
				}
			}
			catch (Exception e)
			{
				Debug.Log(e.Message);
			}
		}
        else{
            Debug.Log($"File {"saves/" + filename} does not exist");
        }
		return default(T);
	}
	
	public static void Save<T>(string filename, T data) where T: class
	{
		if(!Directory.Exists("/saves"))
		{
			Directory.CreateDirectory("/saves");
		}
		using (Stream stream = File.OpenWrite("saves/" + filename))
		{
			BinaryFormatter formatter = new BinaryFormatter();
            
            Debug.Log("saves/" + filename + " was created");
			formatter.Serialize(stream, data);
		}
	}

    public static void Delete(){
		if(Directory.Exists(Directory.GetCurrentDirectory() + "/saves"))
		{
			Directory.Delete(Directory.GetCurrentDirectory() + "/saves", true);
		}
		Directory.CreateDirectory(Directory.GetCurrentDirectory() + "/saves");
    }

    public static bool CheckFileExistance(string filename){
        return File.Exists("saves/" + filename);
    }
}