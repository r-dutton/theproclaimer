[web] PUT /api/dataverse/teams  (Cirrus.Api.Controllers.Dataverse.DataverseController.UpdateTeamChanges)  [L58–L63] [auth=Authentication.RequireTenantIdPolicy]
  └─ uses_service ITenantService (AddScoped)
    └─ method GetCurrentTenant [L61]
  └─ sends_request ProcessDataverseTeamCommand [L62]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.Central.Dataverse.Commands.ProcessDataverseTeamCommandHandler.Handle [L23–L64]
      └─ uses_service IControlledRepository<Team>
        └─ method WriteQuery [L37]

