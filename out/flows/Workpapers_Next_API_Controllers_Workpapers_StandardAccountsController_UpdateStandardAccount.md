[web] PUT /api/standard-accounts/{id:guid}  (Workpapers.Next.API.Controllers.Workpapers.StandardAccountsController.UpdateStandardAccount)  [L150–L157] status=200 [auth=AuthorizationPolicies.Administrator]
  └─ sends_request UpdateStandardAccountCommand [L154]
    └─ handled_by Workpapers.Next.ApplicationService.Commands.Workpapers.SourceData.StandardAccounts.UpdateStandardAccountCommandHandler.Handle [L27–L49]
      └─ uses_service IControlledRepository<MasterAccount>
        └─ method ReadQuery [L43]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<StandardAccount>
        └─ method WriteQuery [L40]
          └─ ... (no implementation details available)

