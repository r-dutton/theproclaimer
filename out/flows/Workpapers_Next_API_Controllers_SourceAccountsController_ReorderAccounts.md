[web] PUT /api/source-accounts/header/{id:Guid}/reorder  (Workpapers.Next.API.Controllers.SourceAccountsController.ReorderAccounts)  [L277–L282] [auth=AuthorizationPolicies.User]
  └─ sends_request ReorderAccountsCommand [L280]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.ApplicationService.Accounting.Commands.Ledger.ReorderAccountsCommandHandler.Handle [L53–L122]
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L69]

