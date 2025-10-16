[web] GET /api/accounting/reports/templates/{id}/disclosures  (Cirrus.Api.Controllers.Accounting.Reports.ReportTemplatesController.GetDisclosures)  [L124–L128] status=200 [auth=user]
  └─ sends_request GetDefaultDisclosuresForExistingReportQuery [L127]
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.Reports.Notes.GetDefaultDisclosuresForExistingReportQueryHandler.Handle [L42–L104]
      └─ calls ReportContentRepository.LoadReadProperties [L70]
      └─ maps_to DisclosureModel [L72]
      └─ uses_service IControlledRepository<ReportTemplate>
        └─ method ReadQuery [L62]
          └─ ... (no implementation details available)
      └─ uses_service IMapper
        └─ method Map [L72]
          └─ ... (no implementation details available)
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L85]
          └─ implementation IRequestProcessor.ProcessAsync [L9-L9]
          └─ ... (no implementation details available)

