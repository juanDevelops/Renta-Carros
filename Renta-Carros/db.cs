using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Renta_Carros
{
    class db
    {
        string MONGODB_URI = "mongodb://192.168.1.69:27017";
        public string errorMessage = "";
        //string connectionString = Environment.GetEnvironmentVariable("MONGODB_URI");

        //public void Conectar()
        //{
        //    if (connectionString == null)
        //    {
        //        Console.WriteLine("You must set your 'MONGODB_URI' environment variable.");
        //        Environment.Exit(0);
        //    }

        //    var client = new MongoClient(connectionString);
        //    var collection = client.GetDatabase("posyowe1").GetCollection<BsonDocument>("Ratas");
        //    var filter = Builders<BsonDocument>.Filter.Eq("title", "Back to the Future");
        //    var document = collection.Find(filter).First();
        //    Console.WriteLine(document);
        //}

        public String AbrirConexion()
        {
            var client = new MongoClient(MONGODB_URI);
            string bd = "";

            try
            {
                List<String> nombreDB = client.ListDatabaseNames().ToList();
                foreach (var db in nombreDB)
                {
                    bd = db.ToString();
                }
            }
            catch (Exception)
            {

                throw;
            }

            return bd;
        }

        public void InsertarCarro(string filePath, string marca, string modelo, string año, string color, string placas, string precio)
        {
            var client = new MongoClient(MONGODB_URI);
            var database = client.GetDatabase("Carros");
            var carrosDB = database.GetCollection<Carros>("carros");

            byte[] imagen = Enumerable.Repeat((byte)0x20, 1).ToArray();

            if (filePath != "")
            {
                imagen = File.ReadAllBytes(filePath);
            }
            else
            {

            }
            

            var Carro = new Carros() 
            { 
              Imagen = new BsonBinaryData(imagen),
              Marca = marca, 
              Modelo = modelo, 
              Año = año,
              Color = color, 
              Placas = placas, 
              Precio = precio 
            };

            carrosDB.InsertOne(Carro);
        }

        public List<BsonDocument> GetAllDocumentsFromCollection()
        {
            var client = new MongoClient(MONGODB_URI);
            var database = client.GetDatabase("Carros");

            try
            {
                var collection = database.GetCollection<BsonDocument>("carros");
                return collection.Find(new BsonDocument()).ToList();
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return null;
            }
        }

        public List<Carros> ObtenerCarros()
        {
            var client = new MongoClient(MONGODB_URI);
            var database = client.GetDatabase("Carros");

            try
            {
                var collection = database.GetCollection<BsonDocument>("carros");
                var bsonDocuments = collection.Find(new BsonDocument()).ToList();

                // Transformar los BsonDocuments a objetos de la clase Carros
                var carros = bsonDocuments.Select(bsonDoc => BsonSerializer.Deserialize<Carros>(bsonDoc)).ToList();

                return carros;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return null;
            }
        }

    }
}
