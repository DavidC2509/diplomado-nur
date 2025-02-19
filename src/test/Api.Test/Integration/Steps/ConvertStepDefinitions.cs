using Microsoft.EntityFrameworkCore;
using TechTalk.SpecFlow;
using Template.Command.Database;

namespace Api.Test.Integration.Steps
{
    class ConvertStepDefinitions
    {
        private readonly ScenarioContext _context;

        public ConvertStepDefinitions(ScenarioContext scenarioContext)
        {
            _context = scenarioContext;
        }

        [Given(@"la siguiente entidad ""(.*)"" registrada")]
        public void GivenLaSiguienteEntidadRegistrada(string entityName, string data)
        {
            var options = new DbContextOptionsBuilder<DataBaseContext>()
                .UseInMemoryDatabase(_context.Get<string>("DB_Key"))
                .Options;

            using (var context = new DataBaseContext(options, null))
            {
                switch (entityName)
                {
                    case "Client":
                        StoreClient(context, data);
                        break;

                }
            }
        }


        private void StoreClient(DataBaseContext db, string data)
        {
            //var parse = Newtonsoft.Json.JsonConvert.DeserializeObject<Client>(data);

            //db.Cl.Add(parse);
            //db.SaveChanges();

            _context.Set(true, "RecordId");
        }


    }
}
