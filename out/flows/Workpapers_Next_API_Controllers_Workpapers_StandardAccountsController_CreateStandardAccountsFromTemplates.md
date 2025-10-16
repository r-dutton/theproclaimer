[web] POST /api/standard-accounts/create-from-template  (Workpapers.Next.API.Controllers.Workpapers.StandardAccountsController.CreateStandardAccountsFromTemplates)  [L132–L141] status=200 [auth=AuthorizationPolicies.Administrator]
  └─ sends_request BulkCreateStandardAccountsCommand [L138]
    └─ handled_by Workpapers.Next.ApplicationService.Commands.Workpapers.SourceData.StandardAccounts.BulkCreateStandardAccountsCommandHandler.Handle [L27–L105]
      └─ uses_service IControlledRepository<MasterAccount>
        └─ method WriteQuery [L53]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<StandardAccount>
        └─ method WriteQuery [L41]
          └─ ... (no implementation details available)

