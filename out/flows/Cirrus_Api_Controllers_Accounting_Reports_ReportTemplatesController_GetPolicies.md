[web] GET /api/accounting/reports/templates/{id}/policies  (Cirrus.Api.Controllers.Accounting.Reports.ReportTemplatesController.GetPolicies)  [L104–L108] status=200 [auth=user]
  └─ sends_request GetDefaultPoliciesForExistingReportQuery [L107]
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.Reports.Notes.GetDefaultPoliciesForExistingReportQueryHandler.Handle [L44–L105]
      └─ calls ReportContentRepository.LoadReadProperties [L72]
      └─ maps_to PolicySelectorModel [L74]
      └─ uses_service IControlledRepository<ReportTemplate>
        └─ method ReadQuery [L64]
          └─ ... (no implementation details available)
      └─ uses_service IMapper
        └─ method Map [L74]
          └─ ... (no implementation details available)
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L87]
          └─ implementation IRequestProcessor.ProcessAsync [L9-L9]
          └─ ... (no implementation details available)

