[web] POST /api/reportsettings/reporttemplates/  (Workpapers.Next.API.Controllers.Reportance.ReportTemplatesController.AddReportTemplate)  [L64–L75] status=201 [auth=AuthorizationPolicies.Administrator]
  └─ uses_service UnitOfWork
    └─ method Add [L72]
      └─ ... (no implementation details available)
  └─ uses_service UserService
    └─ method IsSuperUser [L68]
      └─ implementation Workpapers.Next.ApplicationService.Services.UserService.IsSuperUser [L20-L295]

