[web] GET /api/accounting/reports/masters/package  (Cirrus.Api.Controllers.Accounting.Reports.ReportMastersController.GetPackage)  [L72–L76] status=200 [auth=user,admin]
  └─ sends_request ReportMasterPackageQuery [L75]
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.Reports.ReportMasterPackageQueryHandler.Handle [L37–L75]
      └─ maps_to FooterTemplateDto [L65]
        └─ automapper.registration CirrusMappingProfile (FooterTemplate->FooterTemplateDto) [L629]
        └─ converts_to FooterContentDto [L49]
      └─ maps_to HeaderTemplateLightDto [L62]
        └─ automapper.registration CirrusMappingProfile (HeaderTemplate->HeaderTemplateLightDto) [L627]
      └─ maps_to ReportPageLayoutLightDto [L68]
        └─ automapper.registration CirrusMappingProfile (ReportPageLayout->ReportPageLayoutLightDto) [L646]
      └─ uses_service IControlledRepository<FooterTemplate>
        └─ method ReadQuery [L65]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<HeaderTemplate>
        └─ method ReadQuery [L62]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<ReportPageLayout>
        └─ method ReadQuery [L68]
          └─ ... (no implementation details available)
      └─ uses_service IJurisdictionService (AddScoped)
        └─ method GetFirmJurisdictions [L58]
          └─ implementation IJurisdictionService.GetFirmJurisdictions [L17-L17]
          └─ ... (no implementation details available)
      └─ uses_service IMapper
        └─ method ConfigurationProvider [L63]
          └─ ... (no implementation details available)

