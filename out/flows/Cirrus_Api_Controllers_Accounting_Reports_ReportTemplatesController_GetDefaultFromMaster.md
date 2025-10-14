[web] GET /api/accounting/reports/templates/{fileId:Guid}/default/{masterId:int}  (Cirrus.Api.Controllers.Accounting.Reports.ReportTemplatesController.GetDefaultFromMaster)  [L134–L138] [auth=user]
  └─ sends_request ReportTemplateFromMasterQuery [L137]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.Reports.ReportTemplateFromMasterQueryHandler.Handle [L45–L267]
      └─ maps_to HeaderAccountMatchDto [L67]
      └─ uses_service IControlledRepository<Account>
        └─ method ReadQuery [L67]
      └─ uses_service IControlledRepository<File>
        └─ method ReadQuery [L77]
      └─ uses_service IMapper
        └─ method ConfigurationProvider [L71]
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L65]

