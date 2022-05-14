using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Notus_File_Uploader_Test
{
    class Uploader
    {
        public void Send(string fileName, byte[] file, long size, int chunkSize = 2048)
        {
            int chunkLength = (int)Math.Ceiling(Convert.ToDouble(file.Length / chunkSize));
            Notus.Core.Variable.FileTransferStruct tmpFileStruct = Notus.Core.MergeRawData.FileUpload(
                new Notus.Core.Variable.FileTransferStruct()
                {
                    FileName = fileName,
                    ChunkSize = chunkSize,
                    ChunkCount = chunkLength,
                    FileSize = size,
                    FileHash = new Notus.HashLib.SHA256().Calculate(file),
                    PublicKey = "4a0656f1184f9682bbbe36405c49794054c228d977b3d112740ab9b4795b6acea41ba30b1245cf91627a6e8094342d8aeacb2ddbe18e2b6dff03f384f8a52232",
                    Sign = "",
                }
            );

            string responseData = Notus.Core.Function.FindAvailableNode(
                "storage/file/new",
                new Dictionary<string, string>()
                {
                    {
                        "data",
                        JsonSerializer.Serialize(tmpFileStruct)
                    }
                },
                Notus.Core.Variable.NetworkType.MainNet,
                Notus.Core.Variable.NetworkLayer.Layer2
            ).GetAwaiter().GetResult();
            Notus.Core.Variable.BlockResponse tmpStartObj = JsonSerializer.Deserialize<Notus.Core.Variable.BlockResponse>(responseData);

            int chunk = 0;
            for (int i = 0; i < chunkLength; i++)
            {
                byte[] tmpArray = new byte[chunkSize];
                Array.Copy(file, chunk, tmpArray, 0, tmpArray.Length);
                string sendDataStr = JsonSerializer.Serialize(
                    new Notus.Core.Variable.FileChunkStruct()
                    {
                        Count = i,
                        Data = System.Convert.ToBase64String(tmpArray),
                        UID = tmpStartObj.UID
                    }
                );
                bool innerLoopExit = false;
                while (innerLoopExit == false)
                {
                    string responseChunk = Notus.Core.Function.FindAvailableNode(
                        "storage/file/update",
                        new Dictionary<string, string>() {
                        {
                            "data", sendDataStr
                        }
                        },
                        Notus.Core.Variable.NetworkType.MainNet,
                        Notus.Core.Variable.NetworkLayer.Layer2
                    ).GetAwaiter().GetResult();
                    Console.WriteLine(responseChunk);
                    Notus.Core.Variable.BlockResponse tmpChunkObj = JsonSerializer.Deserialize<Notus.Core.Variable.BlockResponse>(responseChunk);
                    if (tmpChunkObj.Result == Notus.Core.Variable.BlockStatusCode.AddedToQueue)
                    {
                        innerLoopExit = true;
                    }
                    else
                    {
                        Thread.Sleep(2500);
                    }
                }
                chunk += chunkSize;
            }
        }
    }
}


/*
{
	"CurveName":"prime256v1",
	"Words":[
		"broom","opera","virtual","office","reason","wasp","gift","loan",
		"soup","transfer","improve","news","garage","left","engine","talent"
	],
	"PrivateKey":"960fa1a1848dc27193f8fb6979a4d026ce92c5891ba7916a91f4100321187279",
	"PublicKey":"4a0656f1184f9682bbbe36405c49794054c228d977b3d112740ab9b4795b6acea41ba30b1245cf91627a6e8094342d8aeacb2ddbe18e2b6dff03f384f8a52232",
	"WalletKey":"NREfW5vVHfvNs3LYieAM6JT9LPwVjcy2ujEjkk"
}
*/