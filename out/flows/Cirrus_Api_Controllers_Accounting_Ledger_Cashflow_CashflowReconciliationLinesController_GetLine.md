[web] GET /api/accounting/ledger/cashflow/reconciliation-lines/{id}  (Cirrus.Api.Controllers.Accounting.Ledger.Cashflow.CashflowReconciliationLinesController.GetLine)  [L42–L51] status=200 [auth=Authentication.UserPolicy]
  └─ maps_to CashflowReconciliationLineDto [L50]
  └─ maps_to CashflowReconciliationLineDto [L45]
    └─ automapper.registration CirrusMappingProfile (CashflowReconciliationLine->CashflowReconciliationLineDto) [L472]
  └─ calls CashflowReconciliationLineRepository.ReadQuery [L45]
  └─ query CashflowReconciliationLine [L45]
    └─ reads_from CashflowReconciliationLines
  └─ uses_service IControlledRepository<CashflowReconciliationLine>
    └─ method ReadQuery [L45]
      └─ ... (no implementation details available)
  └─ uses_service IMapper
    └─ method Map [L50]
      └─ ... (no implementation details available)
  └─ sends_request CanIAccessFileQuery [L48]
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

