using Reqnroll;

namespace Api.Test.Integration.Steps
{
    [Binding]
    public class Hooks
    {
        private readonly ScenarioContext _context;

        public Hooks(ScenarioContext context)
        {
            _context = context;
        }

        [BeforeScenario]
        public void SetupInMemoryDbName()
        {
            var dbName = Guid.NewGuid().ToString();
            _context.Set(dbName, "InMemoryDbName");
        }
    }
}
