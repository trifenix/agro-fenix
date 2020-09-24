using Cosmonaut;
using Moq;
using System;
using trifenix.agro.db;
using trifenix.agro.db.interfaces;
using trifenix.agro.db.interfaces.agro.common;
using trifenix.agro.db.interfaces.common;
using trifenix.agro.external.interfaces;
using trifenix.agro.microsoftgraph.interfaces;
using trifenix.connect.agro_model;
using trifenix.connect.mdm.ts_model;
using Xunit;
using trifenix.agro.external.operations;
using trifenix.agro.email.interfaces;
using trifenix.agro.weather.interfaces;
using trifenix.agro.search.interfaces;
using trifenix.agro.storage.interfaces;
using trifenix.connect.agro_model_input;
using trifenix.agro.external.operations.tests.data;
using System.Linq;

namespace trifenix.agro.external.operations.tests
{
    public class AgraManagerTests
    {

        private IAgroManager<GeoPointTs> agroManager;

        public AgraManagerTests()
        {
            var mockEmail = new Mock<IEmail>();

            var mockWeather = new Mock<IWeatherApi>();

            var mockUploadImage = new Mock<IUploadImage>();
            var mockSearch = new Mock<IAgroSearch<GeoPointTs>>();

            agroManager = new AgroManager<GeoPointTs>(new MockConnect(), mockEmail.Object, mockUploadImage.Object, mockWeather.Object, mockSearch.Object, string.Empty, false);

        }


        [Theory]
        [InlineData("9aebaf15-eb85-49d7-acca-643329d4078b")]
        [InlineData("7990893f-74e1-45d6-8f3d-af1c9896842c")]
        public void CreandoProductoSinDosis(string id) 
        {
            // assign
            var productInput = AgroData.Products.First(p => p.Id.Equals(id));
            // eliminando id para crear un producto (si tiene id es editar).
            productInput.Id = string.Empty;






            //action
            agroManager.Product.SaveInput(productInput, false);



            //assert

        }


    }



    public class MockConnect : IDbConnect
    {

        // no se usa en la mayor�a de las operaciones
        public ICosmosStore<EntityContainer> BatchStore { 
            get {

                return null;
            }    
        }


        public ICommonQueries CommonQueries {  
            get {
                var mock = new Mock<ICommonQueries>();
                // definici�n de m�todos.


                
                return mock.Object;
            }
        }

        public IGraphApi GraphApi
        {
            get
            {
                var mock = new Mock<IGraphApi>();
                // definici�n de m�todos.



                return mock.Object;
            }
        }

        public IExistElement ExistsElements(bool isBatch)
        {
            var mock = new Mock<IExistElement>();
            // definici�n de m�todos.
            return mock.Object;
        }


        public ICommonDbOperations<T> GetCommonDbOp<T>() where T : DocumentBase
        {
            var mock = new Mock<ICommonDbOperations<T>>();
            // definici�n de m�todos.


            return mock.Object;
        }

        public IMainGenericDb<T> GetMainDb<T>() where T : DocumentBase
        {
            var mock = new Mock<IMainGenericDb<T>>();
            // definici�n de m�todos.


            return mock.Object;
        }
    }
}