[web] PUT /api/accounting/ledger/accounts/merge/bulk  (Cirrus.Api.Controllers.Accounting.Ledger.AccountsController.BulkMerge)  [L504–L510] status=200 [auth=user]
  └─ sends_request BulkMergeAccountsCommand [L508]
    └─ handled_by Cirrus.ApplicationService.Accounting.Commands.Ledger.BulkMergeAccountsCommandHandler.Handle [L33–L111]
      └─ uses_service ILogger
        └─ method LogWarning [L87]
          └─ ... (no implementation details available)
      └─ logs ILogger [Warning] [L87]
  └─ sends_request CanIAccessFileQuery [L507]
    └─ handled_by Cirrus.ApplicationService.Firm.Queries.CanIAccessFileQueryHandler.Handle [L43–L101]
      └─ uses_service IRequestInfoService (AddScoped)
        └─ method IsValidServiceAccountRequest [L66]
          └─ implementation IRequestInfoService.IsValidServiceAccountRequest [L20-L20]
          └─ ... (no implementation details available)
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L90]
          └─ implementation IRequestProcessor.ProcessAsync [L9-L9]
          └─ ... (no implementation details available)
      └─ uses_service ITenantService (AddScoped)
        └─ method GetCurrentTenant [L68]
          └─ implementation ITenantService.GetCurrentTenant [L14-L14]
          └─ ... (no implementation details available)
      └─ uses_service IUserService (InstancePerLifetimeScope)
        └─ method GetUserId [L68]
          └─ implementation IUserService.GetUserId [L18-L18]
          └─ ... (no implementation details available)
      └─ uses_cache IDistributedCache.SetRecordAsync [write] [L79]
      └─ uses_cache IDistributedCache.DoesRecordExistAsync [access] [L71]
      └─ uses_cache IDistributedCache.CreateAccessKey [write] [L68]

