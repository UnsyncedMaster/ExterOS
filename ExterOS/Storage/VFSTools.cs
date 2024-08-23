﻿/* 
 * Name: VFSTools
 * Desc: Macros for VFS management
 * Cont: tunnull2
 * 
 */

using Sys = Cosmos.System;
using Cosmos.System.FileSystem.VFS;
using Cosmos.System.FileSystem;
using System.Threading.Tasks;
using System;
using System.Text;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;

/// <summary>
/// Wrapper for Cosmos VFS interaction. Contains mostly statics instead of Voids like referenced code. 
/// </summary>
public class VFSTools
{
    /// <summary>
    /// "Wrapper" for CosmosVFS initialization.
    /// Initializes VFS. Use the result of this as a global.
    /// </summary>
    public static bool Initialize()
    {
        CosmosVFS CFileSystem = new CosmosVFS();
        //This should make the file system usable within ExterOS
        VFSManager.RegisterVFS(CFileSystem);
        //Returns true 100% for now, unsure if failure prone.
        return true;
    }
    public static int GetFreeSpace(ByteMeasurement ConversionMeasurement)
    {
        var ReturnedFreeSpace = 0;
        //Only one drive I presume, there's pretty bad docs for Cosmos.
        var FreeSpace = VFSManager.GetAvailableFreeSpace("0:\\");
        switch(ConversionMeasurement)
        {
            //I'm lazy.
            case ByteMeasurement.Bytes:
                ReturnedFreeSpace = (int)FreeSpace;
                return ReturnedFreeSpace;
            case ByteMeasurement.Kilobytes:
                ReturnedFreeSpace = (int)FreeSpace / 1024;
                return ReturnedFreeSpace;
            case ByteMeasurement.Megabytes:
                ReturnedFreeSpace = (int)FreeSpace / 1024 / 1024;
                return ReturnedFreeSpace;
            case ByteMeasurement.Gigabytes:
                ReturnedFreeSpace = (int)FreeSpace / 1024 / 1024 / 1024;
                return ReturnedFreeSpace;
            default:
                return ReturnedFreeSpace = (int)FreeSpace;
        }
    }
    public static List<Sys.FileSystem.Listing.DirectoryEntry> List(string Directory)
    {
        //This wasn't copied from the documentation because I'm tired at all.
        var directoryList = VFSManager.GetDirectoryListing(Directory);
        return directoryList;
    }
    public static string Read(string File)
    {
        var FileToRead = VFSManager.GetFile(File);
        //Why must I use FS aaaaaaaaaaaaaaaaaaa;
        var ReadFileStream = FileToRead.GetFileStream();
        //If I can use file... use it!
        if(ReadFileStream.CanRead)
        {
            //I am the one who copies from the Wiki.
            byte[] textToRead = new byte[ReadFileStream.Length];
            ReadFileStream.Read(textToRead, 0, (int)ReadFileStream.Length);
            return Encoding.Default.GetString(textToRead);
        }
        return "";
        //Fuck yo errors! Make sure it's not corrupt, dummy.
    }
    //Basic Imp of File Writing
    public static bool Write(string File, string Input)
    {
        //I haaaaaaaaaaaaaaaaaaaaaaaaaaate file streams.
        var FileToWrite = VFSManager.GetFile(File);
        var FileWriteStream = FileToWrite.GetFileStream();

        if (FileWriteStream.CanWrite)
        {
            byte[] textToWrite = Encoding.ASCII.GetBytes(Input);
            FileWriteStream.Write(textToWrite, 0, textToWrite.Length);
        }
        return true;
        //I once again have no sympathy for you if you decide to write oogum boogum into the input argument.
    }

    public static bool FileExistanceCheck(string File)
    {
        var directoryList = VFSManager.GetDirectoryListing("0:\\");
        //I cannot use directorylist.
        List<string> FileList = new List<string>();
        //Blatant with it fr.
        foreach (var directoryEntry in directoryList)
        {
            var entryType = directoryEntry.mEntryType;
            if (entryType == Sys.FileSystem.Listing.DirectoryEntryTypeEnum.File)
            {
                FileList.Add(directoryEntry.mName);
            }
        }
        if(!FileList.Contains(File)) 
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    public static bool CreateFile(string File)
    {
        VFSManager.CreateFile(File);
        return true;
    }

}