[web] PUT /api/accounting/ledger/cashflow/reconciliation-lines/{id}  (Cirrus.Api.Controllers.Accounting.Ledger.Cashflow.CashflowReconciliationLinesController.Update)  [L79–L85] [auth=Authentication.UserPolicy]
  └─ calls CashflowReconciliationLineRepository.WriteQuery [L82]
  └─ writes_to CashflowReconciliationLine [L82]
    └─ reads_from CashflowReconciliationLines
  └─ uses_service IControlledRepository<CashflowReconciliationLine>
    └─ method WriteQuery [L82]
  └─ sends_request CanIAccessFileQuery [L83]
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

