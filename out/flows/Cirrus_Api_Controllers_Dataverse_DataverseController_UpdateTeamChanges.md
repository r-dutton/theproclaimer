[web] PUT /api/dataverse/teams  (Cirrus.Api.Controllers.Dataverse.DataverseController.UpdateTeamChanges)  [L58–L63] status=200 [auth=Authentication.RequireTenantIdPolicy]
  └─ uses_service ITenantService (AddScoped)
    └─ method GetCurrentTenant [L61]
      └─ implementation ITenantService.GetCurrentTenant [L14-L14]
      └─ ... (no implementation details available)
  └─ sends_request ProcessDataverseTeamCommand [L62]
    └─ handled_by Cirrus.Central.Dataverse.Commands.ProcessDataverseTeamCommandHandler.Handle [L23–L64]
      └─ uses_service IControlledRepository<Team>
        └─ method WriteQuery [L37]
          └─ ... (no implementation details available)

