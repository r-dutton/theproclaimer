[web] DELETE /api/standard-accounts/{id:guid}  (Workpapers.Next.API.Controllers.Workpapers.StandardAccountsController.Delete)  [L207–L216] [auth=AuthorizationPolicies.Administrator]
  └─ sends_request DeleteStandardAccountCommand [L213]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Commands.Workpapers.SourceData.StandardAccounts.DeleteStandardAccountCommandHandler.Handle [L25–L51]
      └─ uses_service IControlledRepository<StandardAccount>
        └─ method WriteQuery [L41]

