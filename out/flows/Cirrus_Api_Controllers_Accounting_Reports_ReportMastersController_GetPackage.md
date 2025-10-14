[web] GET /api/accounting/reports/masters/package  (Cirrus.Api.Controllers.Accounting.Reports.ReportMastersController.GetPackage)  [L72–L76] [auth=user,admin]
  └─ sends_request ReportMasterPackageQuery [L75]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
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
      └─ uses_service IControlledRepository<HeaderTemplate>
        └─ method ReadQuery [L62]
      └─ uses_service IControlledRepository<ReportPageLayout>
        └─ method ReadQuery [L68]
      └─ uses_service IJurisdictionService (AddScoped)
        └─ method GetFirmJurisdictions [L58]
      └─ uses_service IMapper
        └─ method ConfigurationProvider [L63]

