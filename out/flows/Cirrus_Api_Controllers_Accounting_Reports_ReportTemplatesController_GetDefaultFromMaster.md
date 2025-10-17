[web] GET /api/accounting/reports/templates/{fileId:Guid}/default/{masterId:int}  (Cirrus.Api.Controllers.Accounting.Reports.ReportTemplatesController.GetDefaultFromMaster)  [L134–L138] status=200 [auth=user]
  └─ sends_request ReportTemplateFromMasterQuery -> ReportTemplateFromMasterQueryHandler [L137]
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.Reports.ReportTemplateFromMasterQueryHandler.Handle [L45–L267]
      └─ maps_to HeaderAccountMatchDto [L67]
      └─ uses_service IControlledRepository<File> (Scoped (inferred))
        └─ method ReadQuery [L77]
          └─ implementation Cirrus.Data.Repository.Accounting.FileRepository.ReadQuery
      └─ uses_service IControlledRepository<Account> (Scoped (inferred))
        └─ method ReadQuery [L67]
          └─ implementation Cirrus.Data.Repository.Accounting.Ledger.AccountRepository.ReadQuery
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L65]
          └─ implementation DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync [L7-L35]
            └─ ... (no dispatches detected)
  └─ impact_summary
    └─ requests 1
      └─ ReportTemplateFromMasterQuery
    └─ handlers 1
      └─ ReportTemplateFromMasterQueryHandler
    └─ mappings 1
      └─ HeaderAccountMatchDto

