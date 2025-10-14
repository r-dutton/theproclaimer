[web] GET /api/accounting/reports/pageTypes/  (Cirrus.Api.Controllers.Accounting.Reports.ReportPageTypesController.GetAll)  [L83–L95] [auth=user]
  └─ maps_to ReportPageTypeLightWithContentFieldsDto [L90]
    └─ automapper.registration CirrusMappingProfile (ReportPageType->ReportPageTypeLightWithContentFieldsDto) [L635]
  └─ calls ReportPageTypeRepository.ReadQuery [L90]
  └─ queries ReportPageType [L90]
    └─ reads_from ReportPageTypes
  └─ uses_service IControlledRepository<ReportPageType>
    └─ method ReadQuery [L90]
  └─ uses_service IJurisdictionService (AddScoped)
    └─ method GetUserJurisdictions [L88]

