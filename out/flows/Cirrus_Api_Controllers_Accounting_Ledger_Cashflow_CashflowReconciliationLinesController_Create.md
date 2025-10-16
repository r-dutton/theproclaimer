[web] POST /api/accounting/ledger/cashflow/reconciliation-lines/  (Cirrus.Api.Controllers.Accounting.Ledger.Cashflow.CashflowReconciliationLinesController.Create)  [L65–L77] status=201 [auth=Authentication.UserPolicy]
  └─ calls CashflowReconciliationLineRepository.Add [L76]
  └─ insert CashflowReconciliationLine [L76]
    └─ reads_from CashflowReconciliationLines
  └─ uses_service IControlledRepository<CashflowReconciliationLine>
    └─ method Add [L76]
      └─ ... (no implementation details available)
  └─ uses_service IReadRepository (InstancePerLifetimeScope)
    └─ method Table [L70]
      └─ ... (no implementation details available)
  └─ sends_request CanIAccessFileQuery [L68]
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

