[web] DELETE /api/standard-accounts/{id:guid}  (Workpapers.Next.API.Controllers.Workpapers.StandardAccountsController.Delete)  [L207–L216] status=204 [auth=AuthorizationPolicies.Administrator]
  └─ sends_request DeleteStandardAccountCommand [L213]
    └─ handled_by Workpapers.Next.ApplicationService.Commands.Workpapers.SourceData.StandardAccounts.DeleteStandardAccountCommandHandler.Handle [L25–L51]
      └─ uses_service IControlledRepository<StandardAccount>
        └─ method WriteQuery [L41]
          └─ ... (no implementation details available)

