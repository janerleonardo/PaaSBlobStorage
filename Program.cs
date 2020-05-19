using System.Reflection;
using System.IO;
using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.Configuration.FileExtensions;
using Microsoft.Azure;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;

namespace BlobConsole
{
    class Program
    {
        static void Main(string[] args)
        {
           var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange:true);

            var configuration = builder.Build();

            IConfiguration config = new  ConfigurationBuilder()
            .AddJsonFile("appsettings.json",true, true)             
            .Build();
            string getConnstring1 = configuration["configurations:ConnectionString"];
            string getConnstring = config["configurations:ConnectionString"];
            Console.WriteLine(getConnstring1);
            Console.WriteLine(getConnstring);
             Console.WriteLine("Janer");

            CloudStorageAccount cuentaAlmacenamiento = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=pruebaspaas;AccountKey=In3IMDD0juKNMGcjVAIVmSCZfK4OjWz7XAbDufJOq3SJikCAfXQZVuvjdMEn515/8Xb4deshqghD+i1m7m1Teg==;EndpointSuffix=core.windows.net");
            CloudBlobClient clienteBlob = cuentaAlmacenamiento.CreateCloudBlobClient();
            CloudBlobContainer contenedor = clienteBlob.GetContainerReference("janer");
            contenedor.CreateIfNotExists();
            contenedor.SetPermissions(new BlobContainerPermissions 
                                                {PublicAccess = BlobContainerPublicAccessType.Blob });

            CloudBlockBlob miBlob = contenedor.GetBlockBlobReference("foto.jpg");
            using (var fileStrem = System.IO.File.OpenRead(@"rayo.jpg"))
            {
                miBlob.UploadFromStream(fileStrem);
            }
            System.Console.WriteLine("Tu Archivo ya esta en la nube");
            Console.ReadLine();
            

        }
    }
}
