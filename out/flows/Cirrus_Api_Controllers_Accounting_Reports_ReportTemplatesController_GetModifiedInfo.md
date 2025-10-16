[web] GET /api/accounting/reports/templates/{id}/modified-info  (Cirrus.Api.Controllers.Accounting.Reports.ReportTemplatesController.GetModifiedInfo)  [L80–L88] status=200 [auth=user]
  └─ maps_to UserUltraLightDto [L85]
    └─ automapper.registration CirrusMappingProfile (User->UserUltraLightDto) [L103]
  └─ maps_to ReportTemplateModifiedInfoDto [L83]
    └─ automapper.registration CirrusMappingProfile (ReportTemplate->ReportTemplateModifiedInfoDto) [L542]
  └─ calls UserRepository.ReadQuery [L85]
  └─ calls ReportTemplateRepository.ReadQuery [L83]
  └─ query User [L85]
  └─ query ReportTemplate [L83]
  └─ impact_summary
    └─ entities 2 (writes=0, reads=2)
      └─ ReportTemplate writes=0 reads=1
      └─ User writes=0 reads=1
    └─ mappings 2
      └─ ReportTemplateModifiedInfoDto
      └─ UserUltraLightDto

