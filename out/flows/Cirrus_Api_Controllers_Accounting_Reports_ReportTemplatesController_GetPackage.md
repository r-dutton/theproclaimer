[web] GET /api/accounting/reports/templates/package  (Cirrus.Api.Controllers.Accounting.Reports.ReportTemplatesController.GetPackage)  [L144–L148] [auth=user]
  └─ sends_request ReportTemplatePackageQuery [L147]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.Reports.ReportTemplatePackageQueryHandler.Handle [L50–L129]
      └─ maps_to DatasetLightDto [L101]
        └─ automapper.registration CirrusMappingProfile (Dataset->DatasetLightDto) [L204]
      └─ maps_to FooterTemplateDto [L116]
        └─ automapper.registration CirrusMappingProfile (FooterTemplate->FooterTemplateDto) [L629]
        └─ converts_to FooterContentDto [L49]
      └─ maps_to HeaderTemplateLightDto [L113]
        └─ automapper.registration CirrusMappingProfile (HeaderTemplate->HeaderTemplateLightDto) [L627]
      └─ maps_to ReportPageLayoutLightDto [L119]
        └─ automapper.registration CirrusMappingProfile (ReportPageLayout->ReportPageLayoutLightDto) [L646]
      └─ maps_to ReportingSuiteUltraLightDto [L123]
        └─ automapper.registration CirrusMappingProfile (ReportingSuite->ReportingSuiteUltraLightDto) [L766]
      └─ maps_to ReportSettingsLightDto [L83]
        └─ automapper.registration CirrusMappingProfile (ReportSettings->ReportSettingsLightDto) [L532]
      └─ maps_to ReportWatermarkDto [L87]
        └─ automapper.registration CirrusMappingProfile (ReportWatermark->ReportWatermarkDto) [L605]
      └─ maps_to DivisionLightDto [L105]
        └─ automapper.registration CirrusMappingProfile (Division->DivisionLightDto) [L226]
      └─ maps_to TradingAccountLightDto [L109]
        └─ automapper.registration CirrusMappingProfile (TradingAccount->TradingAccountLightDto) [L228]
      └─ uses_service IControlledRepository<Dataset>
        └─ method ReadQuery [L101]
      └─ uses_service IControlledRepository<Division>
        └─ method ReadQuery [L105]
      └─ uses_service IControlledRepository<FooterTemplate>
        └─ method ReadQuery [L116]
      └─ uses_service IControlledRepository<HeaderTemplate>
        └─ method ReadQuery [L113]
      └─ uses_service IControlledRepository<ReportingSuite>
        └─ method ReadQuery [L123]
      └─ uses_service IControlledRepository<ReportPageLayout>
        └─ method ReadQuery [L119]
      └─ uses_service IControlledRepository<ReportSettings>
        └─ method ReadQuery [L83]
      └─ uses_service IControlledRepository<ReportWatermark>
        └─ method ReadQuery [L87]
      └─ uses_service IControlledRepository<TradingAccount>
        └─ method ReadQuery [L109]
      └─ uses_service IJurisdictionService (AddScoped)
        └─ method GetUserJurisdictions [L97]
      └─ uses_service IMapper
        └─ method ConfigurationProvider [L84]

