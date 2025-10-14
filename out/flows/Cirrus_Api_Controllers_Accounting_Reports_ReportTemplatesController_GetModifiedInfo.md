[web] GET /api/accounting/reports/templates/{id}/modified-info  (Cirrus.Api.Controllers.Accounting.Reports.ReportTemplatesController.GetModifiedInfo)  [L80–L88] [auth=user]
  └─ maps_to ReportTemplateModifiedInfoDto [L83]
    └─ automapper.registration CirrusMappingProfile (ReportTemplate->ReportTemplateModifiedInfoDto) [L542]
  └─ maps_to UserUltraLightDto [L85]
    └─ automapper.registration DataverseMappingProfile (User->UserUltraLightDto) [L85]
    └─ automapper.registration WorkpapersMappingProfile (User->UserUltraLightDto) [L96]
    └─ automapper.registration CirrusMappingProfile (User->UserUltraLightDto) [L103]
  └─ calls ReportTemplateRepository.ReadQuery [L83]
  └─ calls UserRepository.ReadQuery [L85]
  └─ queries User [L85]
  └─ queries ReportTemplate [L83]
  └─ uses_service IControlledRepository<ReportTemplate>
    └─ method ReadQuery [L83]
  └─ uses_service IControlledRepository<User>
    └─ method ReadQuery [L85]

