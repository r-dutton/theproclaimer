[web] GET /api/accounting/reports/pageTypes/admin  (Cirrus.Api.Controllers.Accounting.Reports.ReportPageTypesController.GetAllAdmin)  [L139–L147] [auth=user,admin]
  └─ maps_to ReportPageTypeLightAdminDto [L143]
    └─ automapper.registration CirrusMappingProfile (ReportPageType->ReportPageTypeLightAdminDto) [L637]
  └─ calls ReportPageTypeRepository.ReadQuery [L143]
  └─ queries ReportPageType [L143]
    └─ reads_from ReportPageTypes
  └─ uses_service IControlledRepository<ReportPageType>
    └─ method ReadQuery [L143]
  └─ uses_service IJurisdictionService (AddScoped)
    └─ method GetFirmJurisdictions [L142]

