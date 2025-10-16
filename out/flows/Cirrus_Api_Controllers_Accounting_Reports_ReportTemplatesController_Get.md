[web] GET /api/accounting/reports/templates/{id}  (Cirrus.Api.Controllers.Accounting.Reports.ReportTemplatesController.Get)  [L70–L74] status=200 [auth=user]
  └─ sends_request GetReportTemplateQuery [L73]
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.Reports.GetReportTemplateQueryHandler.Handle [L39–L150]
      └─ calls ReportContentRepository.LoadReadProperties [L126]
      └─ maps_to ReportElementDto [L135]
      └─ maps_to ReportTemplateDto [L60]
        └─ automapper.registration CirrusMappingProfile (ReportTemplate->ReportTemplateDto) [L534]
        └─ converts_to ReportTemplateCreateModel [L552]
      └─ maps_to ReportTemplateColumnDto [L89]
        └─ converts_to ReportTemplateColumnCreateModel [L555]
      └─ maps_to ReportTemplateFinancialPageDetailDto [L84]
        └─ converts_to ReportTemplateFinancialPageModel [L559]
      └─ maps_to ReportTemplateNotePageDetailDto [L128]
        └─ converts_to ReportTemplateNotePageModel [L558]
      └─ maps_to ReportTemplatePageDto [L67]
        └─ converts_to ReportTemplatePageCreateModel [L556]
        └─ converts_to ReportTemplatePageCreateUpdateModel [L557]
      └─ uses_service IControlledRepository<ReportTemplate>
        └─ method ReadQuery [L60]
          └─ ... (no implementation details available)
      └─ uses_service IMapper
        └─ method ConfigurationProvider [L70]
          └─ ... (no implementation details available)
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L64]
          └─ implementation IRequestProcessor.ProcessAsync [L9-L9]
          └─ ... (no implementation details available)

