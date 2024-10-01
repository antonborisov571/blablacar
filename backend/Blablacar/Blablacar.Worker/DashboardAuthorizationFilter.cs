using Hangfire.Dashboard;

namespace Blablacar.Worker;

/// <inheritdoc />
public class DashboardAuthorizationFilter : IDashboardAuthorizationFilter
{
    /// <inheritdoc />
    public bool Authorize(DashboardContext context) => true;
}