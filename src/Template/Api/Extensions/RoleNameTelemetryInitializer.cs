using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.Extensibility;

namespace Template.Api.Extensions
{
    public class RoleNameTelemetryInitializer : ITelemetryInitializer
    {
        private readonly string _roleName;

        public RoleNameTelemetryInitializer(string roleName)
        {
            _roleName = roleName;
        }

        public void Initialize(ITelemetry telemetry)
        {
            telemetry.Context.Cloud.RoleName = _roleName;
        }
    }
}

