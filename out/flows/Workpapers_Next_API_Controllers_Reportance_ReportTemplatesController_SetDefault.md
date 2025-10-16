[web] PUT /api/reportsettings/reporttemplates/setdefault/{id:Guid}  (Workpapers.Next.API.Controllers.Reportance.ReportTemplatesController.SetDefault)  [L94–L109] status=200 [auth=AuthorizationPolicies.Administrator]
  └─ uses_service UnitOfWork
    └─ method Table [L101]
      └─ ... (no implementation details available)
  └─ uses_service UserService
    └─ method IsSuperUser [L98]
      └─ implementation Workpapers.Next.ApplicationService.Services.UserService.IsSuperUser [L20-L295]
  └─ sends_request GetFirmReportSettingQuery [L99]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.ReportanceDesktop.GetFirmReportSettingQueryHandler.Handle [L10–L49]
      └─ uses_service UnitOfWork
        └─ method Table [L24]
          └─ ... (no implementation details available)

