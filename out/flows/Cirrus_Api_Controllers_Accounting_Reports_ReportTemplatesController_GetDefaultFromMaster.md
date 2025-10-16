[web] GET /api/accounting/reports/templates/{fileId:Guid}/default/{masterId:int}  (Cirrus.Api.Controllers.Accounting.Reports.ReportTemplatesController.GetDefaultFromMaster)  [L134–L138] status=200 [auth=user]
  └─ sends_request ReportTemplateFromMasterQuery [L137]
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.Reports.ReportTemplateFromMasterQueryHandler.Handle [L45–L267]
      └─ maps_to HeaderAccountMatchDto [L67]
      └─ uses_service IControlledRepository<Account>
        └─ method ReadQuery [L67]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<File>
        └─ method ReadQuery [L77]
          └─ ... (no implementation details available)
      └─ uses_service IMapper
        └─ method ConfigurationProvider [L71]
          └─ ... (no implementation details available)
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L65]
          └─ implementation IRequestProcessor.ProcessAsync [L9-L9]
          └─ ... (no implementation details available)

