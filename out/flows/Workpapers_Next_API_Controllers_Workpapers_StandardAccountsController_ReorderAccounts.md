[web] PUT /api/standard-accounts/{id:int}/reorder  (Workpapers.Next.API.Controllers.Workpapers.StandardAccountsController.ReorderAccounts)  [L195–L200] status=200
  └─ sends_request ReorderStandardAccountsCommand [L198]
    └─ handled_by Cirrus.ApplicationService.Accounting.Commands.Ledger.ReorderStandardAccountsCommandHandler.Handle [L32–L55]
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L48]
          └─ implementation IRequestProcessor.ProcessAsync [L9-L9]
          └─ ... (no implementation details available)

