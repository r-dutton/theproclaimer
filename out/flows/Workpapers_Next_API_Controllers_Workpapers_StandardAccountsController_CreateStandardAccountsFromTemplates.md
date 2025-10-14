[web] POST /api/standard-accounts/create-from-template  (Workpapers.Next.API.Controllers.Workpapers.StandardAccountsController.CreateStandardAccountsFromTemplates)  [L132–L141] [auth=AuthorizationPolicies.Administrator]
  └─ sends_request BulkCreateStandardAccountsCommand [L138]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Commands.Workpapers.SourceData.StandardAccounts.BulkCreateStandardAccountsCommandHandler.Handle [L27–L105]
      └─ uses_service IControlledRepository<MasterAccount>
        └─ method WriteQuery [L53]
      └─ uses_service IControlledRepository<StandardAccount>
        └─ method WriteQuery [L41]

