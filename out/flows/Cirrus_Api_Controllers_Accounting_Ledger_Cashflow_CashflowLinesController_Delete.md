[web] DELETE /api/accounting/ledger/cashflow/lines/{id}  (Cirrus.Api.Controllers.Accounting.Ledger.Cashflow.CashflowLinesController.Delete)  [L116–L122] status=200 [auth=Authentication.UserPolicy]
  └─ calls CashflowLineRepository.Remove [L121]
  └─ calls CashflowLineRepository.WriteQuery [L119]
  └─ delete CashflowLine [L121]
    └─ reads_from CashflowLines
  └─ write CashflowLine [L119]
    └─ reads_from CashflowLines
  └─ uses_service IControlledRepository<CashflowLine>
    └─ method WriteQuery [L119]
      └─ ... (no implementation details available)
  └─ sends_request CanIAccessFileQuery [L120]
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

