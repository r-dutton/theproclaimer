[web] PUT /api/source-accounts/header/{id:Guid}/reorder  (Workpapers.Next.API.Controllers.SourceAccountsController.ReorderAccounts)  [L277–L282] status=200 [auth=AuthorizationPolicies.User]
  └─ sends_request ReorderAccountsCommand [L280]
    └─ handled_by Cirrus.ApplicationService.Accounting.Commands.Ledger.ReorderAccountsCommandHandler.Handle [L53–L122]
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L69]
          └─ implementation IRequestProcessor.ProcessAsync [L9-L9]
          └─ ... (no implementation details available)

