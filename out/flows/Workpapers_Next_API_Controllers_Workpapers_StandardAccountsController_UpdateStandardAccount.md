[web] PUT /api/standard-accounts/{id:guid}  (Workpapers.Next.API.Controllers.Workpapers.StandardAccountsController.UpdateStandardAccount)  [L150–L157] [auth=AuthorizationPolicies.Administrator]
  └─ sends_request UpdateStandardAccountCommand [L154]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Commands.Workpapers.SourceData.StandardAccounts.UpdateStandardAccountCommandHandler.Handle [L27–L49]
      └─ uses_service IControlledRepository<MasterAccount>
        └─ method ReadQuery [L43]
      └─ uses_service IControlledRepository<StandardAccount>
        └─ method WriteQuery [L40]

