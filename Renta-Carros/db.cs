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
              Precio = precio,
              EnRenta = false
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

        public List<Carros> ObtenerCarrosDisponibles()
        {
            var client = new MongoClient(MONGODB_URI);
            var database = client.GetDatabase("Carros");

            try
            {
                var collection = database.GetCollection<BsonDocument>("carros");

                // Filter query to find documents where "enRenta" is false
                var filter = new BsonDocument("enRenta", false);

                var bsonDocuments = collection.Find(filter).ToList();

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


        public bool ExisteCarroPorPlaca(string placa)
        {
            var client = new MongoClient(MONGODB_URI);
            var database = client.GetDatabase("Carros");

            try
            {
                var collection = database.GetCollection<Carros>("carros");

                // Construye un filtro para buscar documentos con la placa específica
                var filter = Builders<Carros>.Filter.Eq(carro => carro.Placas, placa);

                // Realiza la búsqueda utilizando el filtro y verifica si hay al menos un documento encontrado
                var carroEncontrado = collection.Find(filter).Any();

                return carroEncontrado;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }

        public bool RentarCarro(string placas)
        {
            var client = new MongoClient(MONGODB_URI);
            var database = client.GetDatabase("Carros");

            try
            {
                var collection = database.GetCollection<BsonDocument>("carros");

                // Filter to find the car document with the specified license plate
                var filter = new BsonDocument("placas", placas);

                // Update operation to set "enRenta" to true
                var update = Builders<BsonDocument>.Update.Set("enRenta", true);

                var updateResult = collection.UpdateOne(filter, update);

                return updateResult.IsModifiedCountAvailable && updateResult.ModifiedCount == 1;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }


    }
}
