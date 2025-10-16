[web] GET /api/accounting/reports/pageTypes/{id}/light  (Cirrus.Api.Controllers.Accounting.Reports.ReportPageTypesController.GetLight)  [L72–L78] status=200 [auth=user]
  └─ maps_to ReportPageTypeLightWithContentFieldsDto [L75]
    └─ automapper.registration CirrusMappingProfile (ReportPageType->ReportPageTypeLightWithContentFieldsDto) [L635]
  └─ calls ReportPageTypeRepository.ReadQuery [L75]
  └─ query ReportPageType [L75]
    └─ reads_from ReportPageTypes
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ ReportPageType writes=0 reads=1
    └─ mappings 1
      └─ ReportPageTypeLightWithContentFieldsDto

