[web] GET /api/accounting/reports/pageTypes/admin  (Cirrus.Api.Controllers.Accounting.Reports.ReportPageTypesController.GetAllAdmin)  [L139–L147] status=200 [auth=user,admin]
  └─ maps_to ReportPageTypeLightAdminDto [L143]
    └─ automapper.registration CirrusMappingProfile (ReportPageType->ReportPageTypeLightAdminDto) [L637]
  └─ calls ReportPageTypeRepository.ReadQuery [L143]
  └─ query ReportPageType [L143]
    └─ reads_from ReportPageTypes
  └─ uses_service IControlledRepository<ReportPageType>
    └─ method ReadQuery [L143]
      └─ ... (no implementation details available)
  └─ uses_service IJurisdictionService (AddScoped)
    └─ method GetFirmJurisdictions [L142]
      └─ implementation IJurisdictionService.GetFirmJurisdictions [L17-L17]
      └─ ... (no implementation details available)

