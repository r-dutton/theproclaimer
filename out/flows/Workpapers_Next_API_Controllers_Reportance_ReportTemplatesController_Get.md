[web] GET /api/reportsettings/reporttemplates/  (Workpapers.Next.API.Controllers.Reportance.ReportTemplatesController.Get)  [L52–L62]
  └─ maps_to ReportTemplate [L56]
    └─ converts_to ReportTemplateDto [L534]
    └─ converts_to ReportTemplateLightDto [L546]
    └─ converts_to ReportTemplateModifiedInfoDto [L542]
    └─ converts_to ReportTemplate [L23]
  └─ uses_service UnitOfWork
    └─ method Table [L56]
  └─ uses_service UserService
    └─ method GetFirmId [L55]

