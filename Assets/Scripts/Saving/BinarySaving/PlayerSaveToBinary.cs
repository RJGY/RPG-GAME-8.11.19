﻿using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
public static class PlayerSaveToBinary
{
    public static void SavePlayerData(PlayerHandler player)
    {
        //Reference a Binary Formatter
        BinaryFormatter formatter = new BinaryFormatter();
        //Location to Save
        string path = Application.persistentDataPath + "/" + PlayerDataToSave.saveSlot;
        //Create File at file path
        FileStream stream = new FileStream(path, FileMode.Create);
        //What Data to write to the file
        PlayerDataToSave data = new PlayerDataToSave(player);
        //write it and convert to bytes for writing to binary
        formatter.Serialize(stream, data);
        //and we are done
        stream.Close();
    }
    public static PlayerDataToSave LoadData(PlayerHandler player)
    {
        //Location to Save
        string path = Application.persistentDataPath + "/" + PlayerDataToSave.saveSlot;
        //if we have the file at that path
        if (File.Exists(path))
        {
            //get our binary formatter
            BinaryFormatter formatter = new BinaryFormatter();
            //and read the data from the path
            FileStream stream = new FileStream(path, FileMode.Open);
            //set the data from what it is back to usable variables
            PlayerDataToSave data = formatter.Deserialize(stream) as PlayerDataToSave;
            //we are done
            stream.Close();
            //send usable data back to the PlayerDataToSave Script
            return data;
        }
        else
        {
            return null;
        }
    }
}
