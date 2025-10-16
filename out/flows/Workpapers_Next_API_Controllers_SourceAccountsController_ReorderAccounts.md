[web] PUT /api/source-accounts/header/{id:Guid}/reorder  (Workpapers.Next.API.Controllers.SourceAccountsController.ReorderAccounts)  [L277–L282] status=200 [auth=AuthorizationPolicies.User]
  └─ sends_request ReorderAccountsCommand -> ReorderAccountsCommandHandler [L280]
    └─ handled_by Cirrus.ApplicationService.Accounting.Commands.Ledger.ReorderAccountsCommandHandler.Handle [L53–L122]
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L69]
          └─ implementation DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync [L7-L35]
            └─ ... (no dispatches detected)
  └─ impact_summary
    └─ requests 1
      └─ ReorderAccountsCommand
    └─ handlers 1
      └─ ReorderAccountsCommandHandler

