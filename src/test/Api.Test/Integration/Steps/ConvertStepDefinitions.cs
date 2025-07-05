using Microsoft.EntityFrameworkCore;
using Reqnroll;
using Template.Command.Database;
using Template.Domain.ClientAggregate;

namespace Api.Test.Integration.Steps
{
    [Binding]
    class ConvertStepDefinitions
    {
        private readonly ScenarioContext _context;

        public ConvertStepDefinitions(ScenarioContext scenarioContext)
        {
            _context = scenarioContext;
        }

        [Given(@"la siguiente entidad ""(.*)"" registrada")]
        public void GivenLaSiguienteEntidadRegistrada(string entityName)
        {

            var dbName = _context.Get<string>("InMemoryDbName");

            var options = new DbContextOptionsBuilder<DataBaseContext>()
                .UseInMemoryDatabase(dbName)
                .Options;

            using (var context = new DataBaseContext(options, null))
            {
                switch (entityName)
                {
                    case "client":
                        StoreClient(context);
                        break;

                }
            }
        }


        private void StoreClient(DataBaseContext db)
        {
            var parse = Client.CreateClient("Test", "75621921", "papapote3@gmail.com", Guid.NewGuid());

            db.Add(parse);
            db.SaveChanges();

            _context.Set(parse.Id.ToString(), "RecordId");
        }


    }
}