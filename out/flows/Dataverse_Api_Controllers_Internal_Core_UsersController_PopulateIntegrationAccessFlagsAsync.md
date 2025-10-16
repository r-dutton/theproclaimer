[web] GET /api/internal/users  (Dataverse.Api.Controllers.Internal.Core.UsersController.PopulateIntegrationAccessFlagsAsync)  [L205–L218] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ uses_service IDatagetImanageService (AddTransient)
    └─ method TestConnection [L211]
      └─ implementation Dataverse.Connections.DataGet.Services.DataGetImanageService.TestConnection [L19-L225]

