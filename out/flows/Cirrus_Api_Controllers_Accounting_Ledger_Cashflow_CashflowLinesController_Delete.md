[web] DELETE /api/accounting/ledger/cashflow/lines/{id}  (Cirrus.Api.Controllers.Accounting.Ledger.Cashflow.CashflowLinesController.Delete)  [L116–L122] [auth=Authentication.UserPolicy]
  └─ calls CashflowLineRepository.Remove [L121]
  └─ calls CashflowLineRepository.WriteQuery [L119]
  └─ writes_to CashflowLine [L121]
    └─ reads_from CashflowLines
  └─ writes_to CashflowLine [L119]
    └─ reads_from CashflowLines
  └─ uses_service IControlledRepository<CashflowLine>
    └─ method WriteQuery [L119]
  └─ sends_request CanIAccessFileQuery [L120]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.ApplicationService.Firm.Queries.CanIAccessFileQueryHandler.Handle [L43–L101]
      └─ uses_service IRequestInfoService (AddScoped)
        └─ method IsValidServiceAccountRequest [L66]
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L90]
      └─ uses_service ITenantService (AddScoped)
        └─ method GetCurrentTenant [L68]
      └─ uses_service IUserService (InstancePerLifetimeScope)
        └─ method GetUserId [L68]
      └─ uses_cache IDistributedCache [L79]
        └─ method SetRecordAsync [write] [L79]
      └─ uses_cache IDistributedCache [L71]
        └─ method DoesRecordExistAsync [access] [L71]
      └─ uses_cache IDistributedCache [L68]
        └─ method CreateAccessKey [write] [L68]

