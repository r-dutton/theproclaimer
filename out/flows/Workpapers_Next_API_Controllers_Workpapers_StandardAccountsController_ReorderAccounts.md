[web] PUT /api/standard-accounts/{id:int}/reorder  (Workpapers.Next.API.Controllers.Workpapers.StandardAccountsController.ReorderAccounts)  [L195–L200]
  └─ sends_request ReorderStandardAccountsCommand [L198]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.ApplicationService.Accounting.Commands.Ledger.ReorderStandardAccountsCommandHandler.Handle [L32–L55]
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L48]

