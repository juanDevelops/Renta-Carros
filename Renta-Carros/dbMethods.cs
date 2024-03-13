using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using Org.Apache.Http.Authentication;

namespace Renta_Carros
{
    class dbMethods
    {
        static string ipv4C = "";

        public dbMethods(String ipv4)
        {
            ipv4C = ipv4;
        }

        public dbMethods()
        {

        }

        string MONGODB_URI = $"mongodb://{ipv4C}:27017";
        public string errorMessage = "";

        public void InsertarCarro(string filePath, string marca, string modelo, string año, string color, string placas, string precio)
        {
            if (ipv4C == "")
            {
                return;
            }
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

        public List<Carros> ObtenerCarrosDisponibles()
        {
            if (ipv4C == "")
            {
                return null;
            }
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

        public async Task<List<Carros>> ObtenerAutos()
        {
            var client = new MongoClient(MONGODB_URI);
            var database = client.GetDatabase("Carros");
            var collection = database.GetCollection<Carros>("carros");

            var filter = Builders<Carros>.Filter.Eq("enRenta", false);
            var autos = await collection.Find(filter).ToListAsync();
            return autos;
        }


        public bool ExisteCarroPorPlaca(string placa)
        {
            if (ipv4C == "")
            {
                return false;
            }
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
            if (ipv4C == "")
            {
                return false;
            }
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

        public bool ModificarCarroPorPlaca(byte[] imagen, string nuevaMarca, string nuevoModelo, string nuevoAño, string nuevoColor, string nuevasPlacas, string nuevoPrecio)
        {
            if (ipv4C == "")
            {
                return false;
            }
            bool success = false;

            try
            {
                var client = new MongoClient(MONGODB_URI);
                var database = client.GetDatabase("Carros");
                var collection = database.GetCollection<BsonDocument>("carros");
                
                var filter = Builders<BsonDocument>.Filter.Eq("placas", (nuevasPlacas));
                var update = Builders<BsonDocument>.Update
                    .Set("imagen", imagen)
                    .Set("marca", nuevaMarca)
                    .Set("modelo", nuevoModelo)
                    .Set("año", nuevoAño)
                    .Set("color", nuevoColor)
                    .Set("placas", nuevasPlacas)
                    .Set("precio", nuevoPrecio);

                collection.UpdateOne(filter, update);
                return success = true;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return success;
            }

        }

    }
}
