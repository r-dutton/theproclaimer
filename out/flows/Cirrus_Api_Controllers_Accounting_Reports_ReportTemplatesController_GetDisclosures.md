[web] GET /api/accounting/reports/templates/{id}/disclosures  (Cirrus.Api.Controllers.Accounting.Reports.ReportTemplatesController.GetDisclosures)  [L124–L128] [auth=user]
  └─ sends_request GetDefaultDisclosuresForExistingReportQuery [L127]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.Reports.Notes.GetDefaultDisclosuresForExistingReportQueryHandler.Handle [L42–L104]
      └─ calls ReportContentRepository.LoadReadProperties [L70]
      └─ maps_to DisclosureModel [L72]
      └─ uses_service IControlledRepository<ReportTemplate>
        └─ method ReadQuery [L62]
      └─ uses_service IMapper
        └─ method Map [L72]
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L85]

