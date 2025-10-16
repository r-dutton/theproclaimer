[web] DELETE /api/accounting/ledger/cashflow/reconciliation-lines/{id}  (Cirrus.Api.Controllers.Accounting.Ledger.Cashflow.CashflowReconciliationLinesController.Delete)  [L112–L118] status=200 [auth=Authentication.UserPolicy]
  └─ calls CashflowReconciliationLineRepository.Remove [L117]
  └─ calls CashflowReconciliationLineRepository.WriteQuery [L115]
  └─ delete CashflowReconciliationLine [L117]
    └─ reads_from CashflowReconciliationLines
  └─ write CashflowReconciliationLine [L115]
    └─ reads_from CashflowReconciliationLines
  └─ uses_service IControlledRepository<CashflowReconciliationLine>
    └─ method WriteQuery [L115]
      └─ ... (no implementation details available)
  └─ sends_request CanIAccessFileQuery [L116]
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

