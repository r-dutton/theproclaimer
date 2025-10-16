[web] GET /api/reportsettings/reporttemplates/  (Workpapers.Next.API.Controllers.Reportance.ReportTemplatesController.Get)  [L52–L62] status=200
  └─ maps_to ReportTemplate [L56]
    └─ converts_to ReportTemplateDto [L534]
    └─ converts_to ReportTemplateModifiedInfoDto [L542]
    └─ converts_to ReportTemplateLightDto [L546]
    └─ converts_to ReportTemplate [L23]
  └─ uses_service UnitOfWork
    └─ method Table [L56]
      └─ implementation UnitOfWork.Table
  └─ uses_service UserService
    └─ method GetFirmId [L55]
      └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetFirmId [L20-L295]
        └─ uses_service Firm>
          └─ method GetFirmId [L139]
            └─ ... (no implementation details available)
        └─ uses_cache IMemoryCache.GetOrCreate [read] [L280]
  └─ impact_summary
    └─ services 2
      └─ UnitOfWork
      └─ UserService
    └─ caches 1
      └─ IMemoryCache
    └─ mappings 1
      └─ ReportTemplate

