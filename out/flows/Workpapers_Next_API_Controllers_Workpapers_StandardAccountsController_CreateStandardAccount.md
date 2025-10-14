[web] POST /api/standard-accounts  (Workpapers.Next.API.Controllers.Workpapers.StandardAccountsController.CreateStandardAccount)  [L115–L124] [auth=AuthorizationPolicies.Administrator]
  └─ sends_request CreateStandardAccountCommand [L121]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Commands.Workpapers.SourceData.StandardAccounts.CreateStandardAccountCommandHandler.Handle [L26–L71]
      └─ uses_service IControlledRepository<MasterAccount>
        └─ method WriteQuery [L57]
      └─ uses_service IControlledRepository<StandardAccount>
        └─ method Add [L43]

